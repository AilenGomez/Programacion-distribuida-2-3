using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VentaEntrada.Application.Common.Utils;

namespace VentaEntrada.Infrastructure.HealthChecks
{
    public class NotificacionHC : IHealthCheck
    {
        private HttpClient client;
        private IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public NotificacionHC(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            client = _httpClientFactory.CreateClient();
        }


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response = await client.GetAsync(VariablesUtil.GetValue("NotificacionApiUrl", _configuration) + "/ping");
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy();
            }
            return HealthCheckResult.Unhealthy();
        }
    }
}
