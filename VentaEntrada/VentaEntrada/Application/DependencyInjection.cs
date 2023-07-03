using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PuertaDeEntrada.Domain.Entities;
using System.Reflection;
using VentaEntrada.Application.Common.Behaviours;
using VentaEntrada.Application.Repositories.Interfaces;
using VentaEntrada.Application.Repositories;
using Application.Services;

namespace VentaEntrada.Application
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
            services.AddTransient<IEntradaService, EntradaService>();
            return services;
        }
    }
}
