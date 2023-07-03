using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notificaciones.Application.Commands;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Notificaciones.WebUI.Controllers
{
    public class NotificacionController : ApiController
    {
        private readonly ILogger<NotificacionController> _logger;
        public NotificacionController(ILogger<NotificacionController> logger)
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
        public async Task<IActionResult> User([FromBody] string email)
        {
            try
            {
                return Ok(await Mediator.Send(new NotificacionCommand { email = email }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
