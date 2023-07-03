using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PuertaDeEntrada.Application.Common.Behaviours;
using PuertaDeEntrada.Application.Repositories;
using PuertaDeEntrada.Application.Repositories.Interfaces;
using PuertaDeEntrada.Domain.Entities;
using System.Reflection;

namespace PuertaDeEntrada.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<IGenericRepository<Seat>, GenericRepository<Seat>>();
            services.AddTransient<IGenericRepository<Transaction>, GenericRepository<Transaction>>();
            services.AddTransient<IPuertaQueueService, PuertaQueueService>();
            return services;
        }
    }
}
