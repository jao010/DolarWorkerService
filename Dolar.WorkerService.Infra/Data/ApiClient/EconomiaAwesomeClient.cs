using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dolar.WorkerService.Domain.Models.Clients.EconomiaAwesome;

namespace Dolar.WorkerService.Infra.Data.ApiClient
{
    public class EconomiaAwesomeClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public EconomiaAwesomeClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
        }

        public async Task<EconomiaAwesomeResponse> GetDolar()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _configuration["EconomiaAwesomeApi:TodasMoedas"]);

            var client = this._httpClientFactory.CreateClient("economiaAwesome");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EconomiaAwesomeResponse>(responseStream);
            }
            else
            {
                return null;
            }
        }
    }
}
