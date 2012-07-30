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
