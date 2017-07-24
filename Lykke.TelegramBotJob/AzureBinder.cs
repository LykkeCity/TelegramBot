using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.ResolveAnything;
using AzureRepositories.Infrastructure;
using AzureRepositories.Log;
using AzureRepositories.Messages;
using AzureRepositories.Settings;
using AzureRepositories.Telegram;
using AzureStorage.Blob;
using AzureStorage.Tables;
using Common.IocContainer;
using Common.Log;
using Core.Infrastructure;
using Core.Messages;
using Core.Prices;
using Core.Settings;
using Core.Telegram;
using LkeServices.Messages;
using LkeServices.Messages.UpdatesHandler;
using LkeServices.Messages.UpdatesHandler.Commands;
using LkeServices.Messages.UpdatesHandler.Commands.Rates;
using LkeServices.Prices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Lykke.TelegramBotJob
{
    public class AzureBinder : IDependencyBinder
    {
        public const string DefaultConnectionString = "UseDevelopmentStorage=true";


        public ContainerBuilder Bind(IConfigurationRoot configuration, ContainerBuilder builder = null)
        {
            var connectionString = configuration.GetConnectionString("Main");

            var serviceCollection = new ServiceCollection();

            ConfigureServices(connectionString, serviceCollection);

            var ioc = new ContainerBuilder();
            ioc.Populate(serviceCollection);            

            ioc.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            return ioc;
        }

        public void ConfigureServices(string connectionString, IServiceCollection services)
        {
#if DEBUG
            var settings = GeneralSettingsReader.ReadGeneralSettingsLocal<AppStaticSettings>(connectionString).TelegramBot;
#else
            var settings = GeneralSettingsReader.ReadGeneralSettings<AppStaticSettings>(connectionString).TelegramBot;
#endif
            services.AddMemoryCache();
            services.AddSingleton(settings);

            var telegramBot = new TelegramBotClient(settings.Token);
            telegramBot.SetWebhookAsync(string.Empty).Wait();
            services.AddSingleton(telegramBot);

            var sp = services.BuildServiceProvider();
            var memCache = sp.GetService<IMemoryCache>();

            services.AddSingleton<IMessagesTemplatesRepository>(
                new MessagesTemplatesRepository(new AzureBlobStorage(settings.Db.TemplatesConnString), memCache));

            var log = new LogToTable(new AzureTableStorage<LogEntity>(settings.Db.LogsConnString,
                "TgLogTelegramBot", null));

            services.AddSingleton<ILog>(log);

            services.AddSingleton<IHandledMessagesRepository>(
                new HandledMessagesRepository(new AzureTableStorageWithCache<HandledMessageRecord>(settings.Db.DataConnString,
                    "TgHandledMessages", log)));


            services.AddSingleton<IUsersOnChannelRepository>(
                new UsersOnChannelRepository(new AzureTableStorage<UserOnChannelRecord>(settings.Db.DataConnString,
                    "TgUsersOnChannel", log)));

            services.AddSingleton<IOffsetRepository>(
                new OffsetRepository(new AzureTableStorage<OffsetRecord>(settings.Db.DataConnString,
                    "TgUpdatesOffset", log)));

            services.AddSingleton<IServiceMonitoringRepository>(
                new ServiceMonitoringRepository(new AzureTableStorage<MonitoringRecordEntity>(settings.Db.SharedConnString,
                    "Monitoring", log)));

            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<ILykkePriceService, LykkePriceService>();
            services.AddTransient<IUpdatesHandlerService, UpdateHandlerService>();
            services.AddTransient<ILykkeApiClient, LykkeApiClient>();
            
            services.AddTransient<BotCommandsFactory>();
            services.AddTransient<IBotCommand, AndroidAppCommand>();
            services.AddTransient<IBotCommand, IosAppCommand>();
            services.AddTransient<IBotCommand, LkkPriceCommand>();
            services.AddTransient<IBotCommand, StartCommand>();
            services.AddTransient<IBotCommand, SupportMailCommand>();
            services.AddTransient<IBotCommand, UserJoinedCommand>();
            services.AddTransient<IBotCommand, UserLeftCommand>();
            services.AddTransient<IBotCommand, FaqCommand>();
            services.AddTransient<IBotCommand, GetAppCommand>();
            services.AddTransient<IBotCommand, ExchangeRatesCommand>();
            services.AddTransient<IBotCommand, BtcUsdRatesCommand>();
            services.AddTransient<IBotCommand, EthUsdRatesCommand>();
            services.AddTransient<IBotCommand, EthBtcRatesCommand>();
            services.AddTransient<IBotCommand, Lkk1YBtcRatesCommand>();
            services.AddTransient<IBotCommand, LkkBtcRatesCommand>();
            services.AddTransient<IBotCommand, SlrBtcRatesCommand>();
            services.AddTransient<IBotCommand, TimeBtcRatesCommand>();

            services.AddTransient<IUpdatesHandlerService, UpdateHandlerService>();            
        }
    }
}
