using Application.DTOs;
using PuertaDeEntrada.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPuertaQueueService
    {
        Task<TransactionResponse> AddQueue(PuertaEntradaCommand entity, CancellationToken cancellationToken);
    }
}
