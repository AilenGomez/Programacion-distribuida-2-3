using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using VentaEntrada.Application.Commands;
using VentaEntrada.WebUI.Controllers;

namespace PuertaDeEntrada.WebUI.Controllers
{
    public class VentaEntradaController : ApiController
    {
        private readonly ILogger<VentaEntradaController> _logger;
        public VentaEntradaController(ILogger<VentaEntradaController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Post a new user
        /// </summary>
        /// <response code="200">Devuelve el objeto userResponse</response>
        /// <response code="400">Error inesperado</response> 
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> User([FromQuery] VentaEntradaCommand ventaEntrada)
        {
            try
            {
                return Ok(await Mediator.Send(ventaEntrada));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
