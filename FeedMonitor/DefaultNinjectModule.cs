using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace FeedMonitor
{ 
	public class DefaultNinjectModule : NinjectModule
	{
		public override void Load()
		{
			BindExternalServices();
			BindServices();
			BindViewModels();
		}

		private void BindExternalServices()
		{
			Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
			Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
		}

		private void BindServices()
		{
			this.Bind(x => x
				.FromThisAssembly()
				.SelectAllClasses()
				.InNamespaces("FeedMonitor.Services")
				.Where(TypeIsDefaultInterfaceImplementation)
				.BindDefaultInterface()
				.Configure(cfg => cfg.InSingletonScope()));
		}

		private void BindViewModels()
		{
			this.Bind(x => x
				.FromThisAssembly()
				.SelectAllClasses()
				.InNamespaces("FeedMonitor.ViewModels")
				.Where(TypeIsDefaultInterfaceImplementation)
				.BindDefaultInterface()
				.Configure(cfg => cfg.InTransientScope()));
		}

		private static bool TypeIsDefaultInterfaceImplementation(Type type)
		{
			var interfaceName = "I" + type.Name;
			return type.GetInterface(interfaceName) != null;
		}
	}
}
