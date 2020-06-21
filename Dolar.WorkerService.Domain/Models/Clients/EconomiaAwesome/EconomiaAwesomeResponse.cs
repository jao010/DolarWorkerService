using System.Text.Json.Serialization;

namespace Dolar.WorkerService.Domain.Models.Clients.EconomiaAwesome
{
    public class EconomiaAwesomeResponse
    {
        [JsonPropertyName("USD")]
        public Usd Usd { get; set; }
    }
}
