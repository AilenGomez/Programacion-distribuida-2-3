using Application.DTOs;
using Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PuertaDeEntrada.Application.Commands
{
    public class PuertaEntradaCommandHandler : IRequestHandler<PuertaEntradaCommand, TransactionResponse>
    {
        private readonly ILogger<PuertaEntradaCommandHandler> _logger;
        private readonly IPuertaQueueService _puertaQueueService;
        public PuertaEntradaCommandHandler(ILogger<PuertaEntradaCommandHandler> logger, IPuertaQueueService puertaQueueService)
        {
            _logger = logger;
            _puertaQueueService = puertaQueueService;
        }

        public async Task<TransactionResponse> Handle(PuertaEntradaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inicia pedido de numero de transaccion o numero de posicion en la fila en caso de estar ocupado el servicio");
            var transaction = await _puertaQueueService.AddQueue(request, cancellationToken);

            return transaction;
        }
    }
}
