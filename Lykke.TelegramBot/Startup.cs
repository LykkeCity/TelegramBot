using AzureRepositories.Log;
using AzureRepositories.Messages;
using AzureRepositories.Settings;
using AzureRepositories.Telegram;
using AzureStorage.Blob;
using AzureStorage.Tables;
using Common.Log;
using Core.Messages;
using Core.Prices;
using Core.Settings;
using Core.Telegram;
using LkeServices.Messages;
using LkeServices.Messages.UpdatesHandler.Commands;
using LkeServices.Prices;
using Lykke.TelegramBot.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Telegram.Bot;

namespace Lykke.TelegramBot
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            var settings = GeneralSettingsReader.ReadGeneralSettingsLocal<AppStaticSettings>(Configuration.GetConnectionString("Main")).TelegramBot;
#else
            var settings = GeneralSettingsReader.ReadGeneralSettings<AppStaticSettings>(Configuration.GetConnectionString("Main")).TelegramBot;
#endif
            services.AddMemoryCache();
            services.AddSingleton(settings);

            var telegramBot = new TelegramBotClient(settings.Token);
            telegramBot.SetWebhookAsync($"{settings.BaseUrl}/api/TelegramUpdates").Wait();
            services.AddSingleton(telegramBot);

            var sp = services.BuildServiceProvider();
            var memCache = sp.GetService<IMemoryCache>();

            services.AddSingleton<IMessagesTemplatesRepository>(
                new MessagesTemplatesRepository(new AzureBlobStorage(settings.Db.TemplatesConnString), memCache));

            var log = new LogToTable(new AzureTableStorageWithCache<LogEntity>(settings.Db.LogsConnString,
                "TgLogTelegramBot", null));

            services.AddSingleton<ILog>(log);

            services.AddSingleton<IHandledMessagesRepository>(
                new HandledMessagesRepository(new AzureTableStorage<HandledMessageRecord>(settings.Db.DataConnString,
                    "TgHandledMessages", log)));


            services.AddSingleton<IUsersOnChannelRepository>(
                new UsersOnChannelRepository(new AzureTableStorage<UserOnChannelRecord>(settings.Db.DataConnString,
                    "TgUsersOnChannel", log)));

            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<ILykkePriceService, LykkePriceService>();
            services.AddTransient<IUpdatesHandlerService, UpdateHandlerService>();

            services.AddTransient<BotCommandsFactory>();
            services.AddTransient<AndroidAppCommand>();
            services.AddTransient<IosAppCommand>();
            services.AddTransient<LkkPriceCommand>();
            services.AddTransient<StartCommand>();
            services.AddTransient<SupportMailCommand>();
            services.AddTransient<UserJoinedCommand>();
            services.AddTransient<UserLeftCommand>();
            services.AddTransient<FaqCommand>();

            services.AddTransient<IUpdatesHandlerService, UpdateHandlerService>();

            // Add framework services.
            services.AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "TelegramUpdateHandler"
                });
                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
                        IApplicationLifetime appLifetime)
        {
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
