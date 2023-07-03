using MediatR;
using Microsoft.Extensions.Logging;
using Notificaciones.Application.Services;
using Notificaciones.Application.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Notificaciones.Application.Commands
{
    public class NotificacionCommandHandler : IRequestHandler<NotificacionCommand, bool>
    {
        private readonly ILogger<NotificacionCommandHandler> _logger;
        public readonly IMailService _mailService;
        public NotificacionCommandHandler(ILogger<NotificacionCommandHandler> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }
        public async Task<bool> Handle(NotificacionCommand request, CancellationToken cancellationToken)
        {
            return await _mailService.SendEmail(request.email);
   
        }

    }
}
