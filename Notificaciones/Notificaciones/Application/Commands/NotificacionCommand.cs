using MediatR;
using System.Threading.Tasks;

namespace Notificaciones.Application.Commands
{
    public class NotificacionCommand : IRequest<bool>
    {
        public string email { get; set; }
    }
}
