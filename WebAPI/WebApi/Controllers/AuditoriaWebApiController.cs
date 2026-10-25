using CasosDeUso.DTOs.AuditoriaDTOs;
using CasosDeUso.InterfacesCU.CUAuditoria;
using ExcepcionesPropias.ExcepcionesEntidades;
using ExcepcionesPropias.ExcepcionesGenericas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaWebApiController : ControllerBase
    {
        public ICUListadoAuditoriaDTO CUListadoAuditoriaDTO { get; set; }

        public AuditoriaWebApiController(ICUListadoAuditoriaDTO cUListadoAuditoriaDTO)
        {
            CUListadoAuditoriaDTO = cUListadoAuditoriaDTO;
        }

        /// <summary>
        /// Obtiene el listado completo de auditorías de un tipo de gasto.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite obtener todas las auditorías registradas en el sistema para un tipo de gasto.
        /// </remarks>
        /// <returns>Lista de auditorías</returns>
        /// <response code="200">Listado obtenido correctamente</response>
        /// <response code="500">Error inesperado del servidor</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<ListadoAuditoriaDTO> auditorias = CUListadoAuditoriaDTO.Ejecutar(0);
                return Ok(auditorias);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }

        /// <summary>
        /// Permite obtener un conjunto de auditorías asociadas a un ID.
        /// </summary>
        /// <param name="id">Identificador de la auditoría</param>
        /// <returns>Listado de auditorías encontradas</returns>
        /// <response code="200">Auditorías encontradas correctamente</response>
        /// <response code="400">No se encontró auditorias</response>
        /// <response code="404">No se encontró la auditoría solicitada</response>
        /// <response code="409">Conflicto al procesar la solicitud</response>
        /// <response code="500">Error inesperado del servidor</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id no encontrado");
                }

                IEnumerable<ListadoAuditoriaDTO> auditorias = CUListadoAuditoriaDTO.Ejecutar(id);
                return Ok(auditorias);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuditoriaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }
    }
}