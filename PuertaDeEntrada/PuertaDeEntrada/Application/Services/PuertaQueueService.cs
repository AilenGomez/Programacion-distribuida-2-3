using Application.DTOs;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PuertaDeEntrada.Application.Commands;
using PuertaDeEntrada.Application.Common.Interfaces;
using PuertaDeEntrada.Application.Common.Utils;
using PuertaDeEntrada.Application.Repositories.Interfaces;
using PuertaDeEntrada.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PuertaQueueService : IPuertaQueueService
    {
        private readonly ILogger<PuertaQueueService> _logger;
        private readonly IGenericRepository<Transaction> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public PuertaQueueService(
            ILogger<PuertaQueueService> logger, IGenericRepository<Transaction> genericRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _logger = logger;
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<TransactionResponse> AddQueue(PuertaEntradaCommand request, CancellationToken cancellationToken)
        {
            var P = VariablesUtil.GetValue("P",_configuration);
            var transaction = new TransactionResponse { };
            var transactions = _genericRepository.GetAll();
            try
            {
                if (transactions.Any(x => x.email == request.email && x.idTransaction == null))
                {
                    transaction.posicion = transactions.Where(x => x.email == request.email).Select(x => x.posicion).FirstOrDefault();
                    _logger.LogInformation($"El usuario {request.email} se encuentra en la posicion {transaction.posicion}");
                    return transaction;
                }
                if (transactions.Count() < Int32.Parse(P))
                {
                    var transactioncreated = new Transaction { idTransaction = Guid.NewGuid().ToString(), email = request.email, timeSpan = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds, posicion = null };
                    await _genericRepository.AddAsync(transactioncreated);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    transaction.transaction = transactioncreated.idTransaction;
                    _logger.LogInformation($"El usuario {request.email} obtuvo el numero de transaccion {transactioncreated.idTransaction}");

                }
                else
                {
                    var transactioncreated = new Transaction { idTransaction = null, email = request.email, timeSpan = null, posicion = transactions.Count() + 1 - Int32.Parse(P) };
                    await _genericRepository.AddAsync(transactioncreated);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    transaction.posicion = transactioncreated.posicion;
                    _logger.LogInformation($"El usuario {request.email} se encuentra en la posicion {transaction.posicion}");
                }

                return transaction;
            }
            catch(Exception ex)
            {
                throw new Exception("Ese mail ya tiene una transaccion asignada");
            }
            

        }
    }
}
