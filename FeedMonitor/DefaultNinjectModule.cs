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
			this.Bind<IWindowManager>().To<WindowManager>();
			this.Bind<IEventAggregator>().To<EventAggregator>();
			
			this.Bind(x => x
				.FromThisAssembly()
				.SelectAllClasses()
				.InNamespaces("FeedMonitor.Services")
				.Where(type => type.GetInterface("I" + type.Name) != null)
				.BindAllInterfaces()
				.Configure(cfg => cfg.InSingletonScope()));

			this.Bind(x => x
				.FromThisAssembly()
				.SelectAllClasses()
				.InNamespaces("FeedMonitor.ViewModels")
				.NotInNamespaces("FeedMonitor.ViewModels.Designer")
				.BindAllInterfaces()
				.Configure(cfg => cfg.InTransientScope()));
		}
	}
}
