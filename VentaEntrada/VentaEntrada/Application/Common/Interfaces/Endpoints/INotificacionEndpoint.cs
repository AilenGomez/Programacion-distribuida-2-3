using System.Threading.Tasks;

namespace Application.Common.Interfaces.Endpoints
{
    public interface INotificacionEndpoint
    {
        Task<bool> SendNotification(string email);
    }
}
