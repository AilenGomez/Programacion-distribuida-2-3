using Application.Common.Interfaces.Endpoints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PuertaDeEntrada.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VentaEntrada.Application.Commands;
using VentaEntrada.Application.Common.Interfaces;
using VentaEntrada.Application.Common.Utils;
using VentaEntrada.Application.Repositories.Interfaces;

namespace Application.Services
{
    public class EntradaService : IEntradaService
    {
        private readonly ILogger<EntradaService> _logger;
        private readonly IGenericRepository<Transaction> _genericRepositoryTransaction;
        private readonly IGenericRepository<Seat> _genericRepositorySeat;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private static readonly object lockObject = new object();
        private readonly INotificacionEndpoint _notificacionEndpoint;
        public EntradaService(
            ILogger<EntradaService> logger, IGenericRepository<Seat> genericRepositorySeat, IUnitOfWork unitOfWork, IConfiguration configuration, IGenericRepository<Transaction> genericRepositoryTransaction, INotificacionEndpoint notificacionEndpoint)
        {
            _logger = logger;
            _genericRepositorySeat = genericRepositorySeat;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _genericRepositoryTransaction = genericRepositoryTransaction;
            _notificacionEndpoint = notificacionEndpoint;
        }

        public async Task<string> seatSale(VentaEntradaCommand request, CancellationToken cancellationToken)
        {
            var N = VariablesUtil.GetValue("N", _configuration);
            var M = VariablesUtil.GetValue("M", _configuration);
            var message = "";

            var transactiondb = _genericRepositoryTransaction.GetByCondition(x => x.idTransaction == request.transaction);
            if (transactiondb == null)
            {
                throw new Exception($"El numero de transaccion no es valido - {request.transaction}");
            }
            var dateTime2 = DateTimeOffset.FromUnixTimeSeconds((long)transactiondb.timeSpan).LocalDateTime;
            var tiempoTranscurrido = DateTime.Now.Subtract(dateTime2).TotalMinutes;
            if (tiempoTranscurrido > Double.Parse(M))
            {
                await UpdateTransactions(transactiondb);
                return $"¡La ventana de compra ha expirado para {transactiondb.email}!";
            }
            if (request.row < Int32.Parse(N) && request.column < Int32.Parse(N))
            {
                lock (lockObject)
                {
                    var seats = _genericRepositorySeat.GetAll();
                    if (!seats.Any(x => x.Column == request.column && x.Row == request.row))
                    {
                        _genericRepositorySeat.Add(new Seat { Id = 0, Column = request.column, Row = request.row, isFree = false });
                        _unitOfWork.Commit();

                        _logger.LogInformation($"Enviando notificacion - transaccion {transactiondb.idTransaction}");
                        _notificacionEndpoint.SendNotification(transactiondb.email);

                        message = "Se concreto la venta con exito";
                    }
                    else
                    {
                        throw new Exception($"El asiento {request.row} - {request.column} ya esta vendido");
                    }

                }
                await UpdateTransactions(transactiondb);
                return message;
            }
            else
            {
                throw new Exception($"No es un asiento valido {request.row} - {request.column}");
            }

        }

        public async Task UpdateTransactions(Transaction transaction)
        {
            _genericRepositoryTransaction.Remove(transaction);
            _unitOfWork.Commit();
            var transactions = await _genericRepositoryTransaction.GetAllAsync();
            foreach (Transaction item in transactions)
            {
                if (item.posicion == 1)
                {
                    item.timeSpan = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    item.posicion = null;
                    item.idTransaction = Guid.NewGuid().ToString();
                    _genericRepositoryTransaction.Update(item);
                    _unitOfWork.Commit();
                }
                else
                {
                    item.posicion -= 1;
                    _genericRepositoryTransaction.Update(item);
                    _unitOfWork.Commit();
                }
            }

        }
    }
}
