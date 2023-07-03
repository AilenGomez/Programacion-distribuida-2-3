using Application.Common.Interfaces.Endpoints;
using Infrastructure.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using VentaEntrada.Application.Common.Interfaces;
using VentaEntrada.Application.Common.Interfaces.Services;
using VentaEntrada.Application.Common.Utils;
using VentaEntrada.Infrastructure.Persistence;
using VentaEntrada.Infrastructure.Services;

namespace VentaEntrada.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = VariablesUtil.GetConnectionString(configuration);

            services.AddDbContext<ApplicationDbContext>(options =>

                options.UseSqlServer(connection,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INotificacionEndpoint, NotificacionEndpoint>();

            return services;
        }
    }
}
