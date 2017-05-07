using Autofac.Extensions.DependencyInjection;
using Common.IocContainer;
using Lykke.JobTriggers.Triggers;
using Microsoft.Extensions.Configuration;

namespace Lykke.TelegramBotJob
{
    public class AppHost
    {
	    private readonly IDependencyBinder _binder;
	    private readonly IConfigurationRoot _configurationRoot;

	    public AppHost(IDependencyBinder binder, IConfigurationRoot configurationRoot)
	    {
		    _binder = binder;
		    _configurationRoot = configurationRoot;
	    }

	    public void Run()
	    {			
			var containerBuilder = _binder.Bind(_configurationRoot);
			var ioc = containerBuilder.Build();
            
			var triggerHost = new TriggerHost(new AutofacServiceProvider(ioc));
			triggerHost.Start().Wait();
		}
	}
}
