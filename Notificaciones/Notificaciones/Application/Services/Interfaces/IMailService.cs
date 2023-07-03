using System.Threading.Tasks;

namespace Notificaciones.Application.Services.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendEmail(string email);

    }
}