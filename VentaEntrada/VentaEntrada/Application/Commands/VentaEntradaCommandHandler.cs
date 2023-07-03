using Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace VentaEntrada.Application.Commands
{
    public class VentaEntradaCommandHandler : IRequestHandler<VentaEntradaCommand, string>
    {
        private readonly ILogger<VentaEntradaCommandHandler> _logger;
        private readonly IEntradaService _entradaService;
        public VentaEntradaCommandHandler(ILogger<VentaEntradaCommandHandler> logger, IEntradaService entradaService)
        {
            _logger = logger;
            _entradaService = entradaService;
        }

        public async Task<string> Handle(VentaEntradaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Inicia proceso de venta de asiento {request.row}-{request.column}");
            var transaction = await _entradaService.seatSale(request, cancellationToken);

            return transaction;
        }
    }
}
