using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using FeedMonitor.ViewModels;
using Ninject;
using Ninject.Extensions.Conventions;

namespace FeedMonitor
{
	public class AppBootstrapper : Bootstrapper<ShellViewModel>
	{
		private IKernel kernel;

		protected override void Configure()
		{
			kernel = new StandardKernel();

			kernel.Bind<IWindowManager>().To<WindowManager>();
			kernel.Bind<IEventAggregator>().To<EventAggregator>();

			kernel.Bind(x => x
				.FromThisAssembly()
				.SelectAllClasses()
				.InNamespaces("FeedMonitor.Services")
				.Where(type => type.GetInterface("I" + type.Name) != null)
				.BindAllInterfaces()
				.Configure(cfg => cfg.InSingletonScope()));

			kernel.Bind(x => x
				.FromThisAssembly()
				.SelectAllClasses()
				.InNamespaces("FeedMonitor.ViewModels")
				.NotInNamespaces("FeedMonitor.ViewModels.Designer")
				.BindAllInterfaces()
				.Configure(cfg => cfg.InTransientScope()));
		}

		protected override object GetInstance(Type serviceType, string key)
		{
			return kernel.Get(serviceType);
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}

		protected override void BuildUp(object instance)
		{
			kernel.Inject(instance);
		}
	}
}
