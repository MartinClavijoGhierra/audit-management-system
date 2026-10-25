using CasosDeUso.InterfacesCU.CUTipoGastos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoGastoWebApiController : ControllerBase
    {
        public ICUListadoTipoGastos CUListadoTipoGastos { get; set; }

        public TipoGastoWebApiController(ICUListadoTipoGastos cUListadoTipoGastos)
        {
            CUListadoTipoGastos = cUListadoTipoGastos;
        }

        /// <summary>
        /// Obtiene el listado completo de tipos de gasto.
        /// </summary>
        /// <remarks>
        /// Permite obtener todos los tipos de gasto almacenados en el sistema,
        /// incluyendo activos e inactivos.
        /// </remarks>
        /// <returns>Listado de tipos de gasto.</returns>
        /// <response code="200">Listado devuelto correctamente</response>
        /// <response code="500">Error inesperado en el servidor</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                return Ok(CUListadoTipoGastos.Ejecutar());
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }
    }
}