using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PuertaDeEntrada.Application.Common.Interfaces;
using PuertaDeEntrada.Application.Common.Interfaces.Services;
using PuertaDeEntrada.Application.Common.Utils;
using PuertaDeEntrada.Infrastructure.Persistence;
using PuertaDeEntrada.Infrastructure.Services;

namespace PuertaDeEntrada.Infrastructure
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

            return services;
        }
    }
}
