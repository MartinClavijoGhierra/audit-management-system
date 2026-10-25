using CasosDeUso.DTOs.EquipoDTOs;
using CasosDeUso.DTOs.PagoDTOs;
using CasosDeUso.InterfacesCU.CUEquipo;
using CasosDeUso.InterfacesCU.CUPago;
using ExcepcionesPropias.ExcepcionesEntidades;
using ExcepcionesPropias.ExcepcionesGenericas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoWebApiController : ControllerBase
    {
        public ICUObtenerPagos CUObtenerPagos { get; set; }
        public ICUAltaPagoUnico CUAltaPagoUnico { get; set; }
        public ICUAltaPagoRecurrente CUAltaPagoRecurrente { get; set; }
        public ICUBuscarPago CUBuscarPago { get; set; }
        public ICUEquiposMonto CUObtenerEquipos { get; set; }

        public PagoWebApiController(
            ICUObtenerPagos cUObtenerPagos,
            ICUAltaPagoUnico cUAltaPagoUnico,
            ICUAltaPagoRecurrente cUAltaPagoRecurrente,
            ICUBuscarPago cUBuscarPago,
            ICUEquiposMonto cUEquiposMonto)
        {
            CUObtenerPagos = cUObtenerPagos;
            CUAltaPagoUnico = cUAltaPagoUnico;
            CUAltaPagoRecurrente = cUAltaPagoRecurrente;
            CUBuscarPago = cUBuscarPago;
            CUObtenerEquipos = cUEquiposMonto;
        }

        // ================================
        // GET PAGOS DEL USUARIO
        // ================================
        /// <summary>
        /// Obtiene todos los pagos del usuario autenticado.
        /// </summary>
        /// <remarks>
        /// Recupera los pagos asociados al usuario cuyo ID está incluido en el token.
        /// </remarks>
        /// <returns>Lista de pagos del usuario autenticado.</returns>
        /// <response code="200">Pagos obtenidos exitosamente</response>
        /// <response code="400">El ID del usuario no es válido</response>
        /// <response code="401">El token no contiene un ID</response>
        /// <response code="404">No se encontraron pagos asociados al usuario</response>
        /// <response code="409">Conflicto al procesar la solicitud</response>
        /// <response code="500">Error inesperado del servidor</response>
        [HttpGet(Name = "FindById")]
        [Authorize(Roles = "Gerente,Empleado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                Claim claimId = User.FindFirst(ClaimTypes.NameIdentifier);

                if (claimId == null)
                    return Unauthorized("El token no contiene un ID de usuario.");

                int id = int.Parse(claimId.Value);

                if (id <= 0)
                {
                    return BadRequest("El id no es correcto");
                }

                IEnumerable<ListadoPagoDTO> pagos = CUObtenerPagos.Ejecutar(id);

                if (pagos != null)
                    return Ok(pagos);

                return NotFound($"No se encontró ningún pago con id {id}");
            }
            catch (PagoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // ================================
        // GET EQUIPOS POR MONTO
        // ================================
        /// <summary>
        /// Obtiene equipos cuyo costo supera un monto especificado.
        /// </summary>
        /// <param name="monto">Monto mínimo requerido</param>
        /// <returns>Listado de equipos con pagos superiores al monto indicado</returns>
        /// <response code="200">Equipos encontrados</response>
        /// <response code="400">Monto inválido (debe ser mayor a 0)</response>
        /// <response code="404">No se encontraron equipos con dicho monto</response>
        /// <response code="500">Error inesperado del servidor</response>
        [HttpGet("EncontrarEquipo/{monto}")]
        [Authorize(Roles = "Gerente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(decimal monto)
        {
            try
            {
                if (monto <= 0)
                {
                    return BadRequest("Debe seleccionar un monto mayor a 0");
                }

                IEnumerable<ListadoEquipoDTO> equipos = CUObtenerEquipos.Ejecutar(monto);

                if (equipos != null)
                {
                    return Ok(equipos);
                }

                return NotFound($"No se encontró ningún pago con pagos superiores a {monto}");
            }
            catch (PagoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // ================================
        // POST PAGO ÚNICO
        // ================================
        /// <summary>
        /// Registra un pago único.
        /// </summary>
        /// <param name="pagoUnicoDTO">Datos del pago único</param>
        /// <returns>Pago registrado con su nuevo ID</returns>
        /// <response code="201">Pago único creado correctamente</response>
        /// <response code="400">Datos incorrectos</response>
        /// <response code="409">Conflicto al registrar el pago</response>
        /// <response code="500">Error inesperado del servidor</response>
        [HttpPost("unico")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] PagoUnicoDTO pagoUnicoDTO)
        {
            try
            {
                if (pagoUnicoDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }

                int id = CUAltaPagoUnico.Ejecutar(pagoUnicoDTO);
                pagoUnicoDTO.Id = id;

                return CreatedAtRoute("FindById", new { Id = id }, pagoUnicoDTO);
            }
            catch (PagoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }

        // ================================
        // POST PAGO RECURRENTE
        // ================================
        /// <summary>
        /// Registra un pago recurrente.
        /// </summary>
        /// <param name="pagoRecurrenteDTO">Datos del pago recurrente</param>
        /// <returns>Pago registrado con su nuevo ID</returns>
        /// <response code="201">Pago recurrente creado correctamente</response>
        /// <response code="400">Datos incorrectos</response>
        /// <response code="409">Conflicto al registrar el pago</response>
        /// <response code="500">Error inesperado del servidor</response>
        [HttpPost("recurrente")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] PagoRecurrenteDTO pagoRecurrenteDTO)
        {
            try
            {
                if (pagoRecurrenteDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }

                int id = CUAltaPagoRecurrente.Ejecutar(pagoRecurrenteDTO);
                pagoRecurrenteDTO.Id = id;

                return CreatedAtRoute("FindById", new { Id = id }, pagoRecurrenteDTO);
            }
            catch (PagoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }
    }
}
