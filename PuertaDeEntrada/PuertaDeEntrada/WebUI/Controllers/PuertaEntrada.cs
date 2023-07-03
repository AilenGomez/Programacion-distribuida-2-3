using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PuertaDeEntrada.Application.Commands;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PuertaDeEntrada.WebUI.Controllers
{
    public class PuertaEntradaController : ApiController
    {
        private readonly ILogger<PuertaEntradaController> _logger;
        public PuertaEntradaController(ILogger<PuertaEntradaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> User([FromQuery] string email)
        {
            try
            {
                return Ok(await Mediator.Send(new PuertaEntradaCommand { email = email }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
