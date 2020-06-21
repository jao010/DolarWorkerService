using Microsoft.Extensions.Configuration;

namespace Dolar.WorkerService.Configuration
{
    public static class ConfigurationStart
    {
        public static IConfiguration Config(string environment)
        {
            return new ConfigurationBuilder()
                                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables()
                                .Build();
        }
    }
}
