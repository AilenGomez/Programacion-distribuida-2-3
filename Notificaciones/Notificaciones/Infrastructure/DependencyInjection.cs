using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Notificaciones.Application.Common.Interfaces;
using Notificaciones.Application.Common.Interfaces.Services;
using Notificaciones.Application.Common.Utils;
using Notificaciones.Infrastructure.Persistence;
using Notificaciones.Infrastructure.Services;

namespace Notificaciones.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = VariablesUtil.GetConnectionString(configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
