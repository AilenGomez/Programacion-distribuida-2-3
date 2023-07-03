using Application.Common.Interfaces.Endpoints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VentaEntrada.Application.Common.Utils;

namespace Infrastructure.Endpoints
{
    public class NotificacionEndpoint : INotificacionEndpoint
    {
        private HttpClient client;
        private IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NotificacionEndpoint> _logger;
        public NotificacionEndpoint(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<NotificacionEndpoint> logger
            )
        {
            _httpClientFactory = httpClientFactory;
            client = _httpClientFactory.CreateClient();
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<bool> SendNotification(string email)
        {
            var data = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(VariablesUtil.GetValue("NotificacionApiUrl", _configuration), data);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
