using Dolar.WorkerService.Service.Services.EconomiaAwesome;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dolar.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly EconomiaAwesomeService _service;

        public Worker(ILogger<Worker> logger,
            EconomiaAwesomeService service
            )
        {
            _logger = logger;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            decimal dolarAtual = await this._service.Executar();
            while (!stoppingToken.IsCancellationRequested)
            {
                var dolarNovo = await this._service.Executar(dolarAtual);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation($"Valor do dolar: {dolarNovo}");
                dolarAtual = dolarNovo;
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
