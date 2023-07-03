using System.Threading;
using System.Threading.Tasks;
using VentaEntrada.Application.Commands;

namespace Application.Services
{
    public interface IEntradaService
    {
        Task<string> seatSale(VentaEntradaCommand entity, CancellationToken cancellationToken);
    }
}
