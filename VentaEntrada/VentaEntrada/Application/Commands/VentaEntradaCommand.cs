using MediatR;

namespace VentaEntrada.Application.Commands
{
    public class VentaEntradaCommand : IRequest<string>
    {
        public string transaction { get; set; }
        public int row { get; set; }
        public int column { get; set; }
    }
}
