using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Notificaciones.Application.Common.Behaviours;
using Notificaciones.Application.Services.Interfaces;
using Notificaciones.Application.Common.Utils;

namespace Notificaciones.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient<IMailService, EmailSender>();
            services.AddTransient<IMailService, EmailSender>();
            return services;
        }
    }
}
