using System.IO;
using Common.IocContainer;
using Microsoft.Extensions.Configuration;

namespace Lykke.TelegramBotJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            IDependencyBinder binder = new AzureBinder();

            AppHost host = new AppHost(binder, configuration);
            host.Run();
        }
    }
}