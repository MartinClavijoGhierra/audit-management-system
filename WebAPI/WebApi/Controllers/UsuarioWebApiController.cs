using CasosDeUso.DTOs.UsuarioDTOs;
using CasosDeUso.InterfacesCU.CUUsuario;
using ExcepcionesPropias.ExcepcionesEntidades;
using ExcepcionesPropias.ExcepcionesGenericas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Autenticacion;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioWebApiController : ControllerBase
    {
        public ICULoguinUsuario CULoguinUsuario { get; set; }
        public ICUModificarContrasenia CUModificarContrasenia { get; set; }

        public UsuarioWebApiController(
            ICULoguinUsuario cULoguinUsuario,
            ICUModificarContrasenia cUModificarContrasenia)
        {
            CULoguinUsuario = cULoguinUsuario;
            CUModificarContrasenia = cUModificarContrasenia;
        }

        /// <summary>
        /// Permite iniciar sesión en el sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint recibe las credenciales del usuario y devuelve un token JWT válido
        /// junto con los datos del usuario logueado.
        /// </remarks>
        /// <param name="loguinDTO">Credenciales del usuario (email y contraseña).</param>
        /// <returns>Usuario autenticado con token generado.</returns>
        /// <response code="200">Login exitoso</response>
        /// <response code="400">Datos incorrectos o credenciales inválidas</response>
        /// <response code="409">Error por conflicto de datos</response>
        /// <response code="500">Error inesperado del servidor</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] LoguinDTO loguinDTO)
        {
            try
            {
                if (loguinDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }

                UsuarioLogueadoDTO userLog = CULoguinUsuario.Ejecutar(loguinDTO);

                if (userLog != null)
                {
                    userLog.Token = ManejadorToken.CrearToken(userLog);
                }

                return Ok(userLog);
            }
            catch (UsuarioException ex)
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

        /// <summary>
        /// Permite restablecer la contraseña de un usuario mediante su email.
        /// </summary>
        /// <remarks>
        /// Solo el rol <b>Administrador</b> puede ejecutar esta acción.
        /// El endpoint genera una nueva contraseña aleatoria y la devuelve en la respuesta.
        /// </remarks>
        /// <param name="email">Email del usuario al que se le restablecerá la contraseña.</param>
        /// <returns>Nueva contraseña generada para el usuario.</returns>
        /// <response code="200">Contraseña restablecida correctamente</response>
        /// <response code="400">El email proporcionado es inválido</response>
        /// <response code="409">Conflicto al procesar la solicitud</response>
        /// <response code="500">Error inesperado del servidor</response>
        [HttpPut("{email}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ModificarContrasenia(string email)
        {
            try
            {
                if (email == null)
                {
                    return BadRequest("Debe indicar un email válido.");
                }

                string nuevaContrasenia = CUModificarContrasenia.Ejecutar(email);

                return Ok($"Contraseña restablecida correctamente. Nueva contraseña: {nuevaContrasenia}");
            }
            catch (UsuarioException ex)
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