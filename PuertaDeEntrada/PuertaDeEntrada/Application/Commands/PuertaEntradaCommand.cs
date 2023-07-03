using Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PuertaDeEntrada.Application.Commands
{
    public class PuertaEntradaCommand : IRequest<TransactionResponse>
    {
        [EmailAddress]
        public string email { get; set; }
    }
}
