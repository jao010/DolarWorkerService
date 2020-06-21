using Dolar.WorkerService.Domain.Interfaces;
using Dolar.WorkerService.Infra.Data.ApiClient;
using System;
using System.Threading.Tasks;

namespace Dolar.WorkerService.Service.Services.EconomiaAwesome
{
    public class EconomiaAwesomeService
    {
        private readonly EconomiaAwesomeClient _economiaAwesomeClient;
        private readonly IEmailSend _emailSend;
        public EconomiaAwesomeService(EconomiaAwesomeClient economiaAwesomeClient,
            IEmailSend emailSend)
        {
            this._economiaAwesomeClient = economiaAwesomeClient;
            this._emailSend = emailSend;
        }

        public async Task<decimal> Executar(decimal dolarVlrAtual)
        {
            var dolar = await this._economiaAwesomeClient.GetDolar();

            var dolarVlrNovo = decimal.Round(Convert.ToDecimal(dolar.Usd.Bid.Replace(".", ",")), 2);

            if (dolarVlrAtual != dolarVlrNovo)
            {
                await this._emailSend.SendEmailAsync(dolarVlrAtual, dolarVlrNovo);
            }

            return dolarVlrNovo;
        }

        public async Task<decimal> Executar()
        {
            var dolar = await this._economiaAwesomeClient.GetDolar();

            return decimal.Round(Convert.ToDecimal(dolar.Usd.Bid.Replace(".", ",")), 2);
        }
    }
}
