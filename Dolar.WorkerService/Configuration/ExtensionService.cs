using Dolar.WorkerService.Domain.Interfaces;
using Dolar.WorkerService.Domain.Models.ConfigureDependency;
using Dolar.WorkerService.Infra.Data.ApiClient;
using Dolar.WorkerService.Infra.Data.Notification.Email;
using Dolar.WorkerService.Service.Services.EconomiaAwesome;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dolar.WorkerService.Configuration
{
    public static class ExtensionService
    {
        public static IServiceCollection ConfigureServiceDependence(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureHttpClientsFactory(configuration);
            services.ConfigureOptions(configuration);
            services.ConfigureInfra();
            services.ConfigureServices();
            return services;
        }

        private static IServiceCollection ConfigureHttpClientsFactory(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("economiaAwesome", c =>
            {
                c.BaseAddress = new System.Uri(configuration["EconomiaAwesomeApi:BaseUri"]);
            });
            return services;
        }

        private static IServiceCollection ConfigureInfra(this IServiceCollection services)
        {
            services.AddSingleton<EconomiaAwesomeClient>();
            services.AddTransient<IEmailSend, EmailSend>();
            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<EconomiaAwesomeService>();
            return services;
        }

        private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            return services;
        }
    }
}
