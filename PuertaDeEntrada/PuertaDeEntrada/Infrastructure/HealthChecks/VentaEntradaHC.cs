using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PuertaDeEntrada.Application.Common.Utils;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PuertaDeEntrada.Infrastructure.HealthChecks
{
    public class VentaEntradaHC : IHealthCheck
    {
        private HttpClient client;
        private IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public VentaEntradaHC(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            client = _httpClientFactory.CreateClient();
        }


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response = await client.GetAsync(VariablesUtil.GetValue("VentaEntradaApiUrl", _configuration) + "/ping");
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy();
            }
            return HealthCheckResult.Unhealthy();
        }
    }
}
