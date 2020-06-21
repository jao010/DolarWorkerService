using Dolar.WorkerService.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Dolar.WorkerService
{
    public class Program
    {
        protected Program()
        {
        }
        private static string _environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        private static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            Configuration = ConfigurationStart.Config(_environment);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigureServiceDependence(Configuration);
                    services.AddHostedService<Worker>();
                });
    }
}
