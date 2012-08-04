using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace FeedMonitor.ViewModels.Designer
{
	public class DesignerShellViewModel : Conductor<object>.Collection.OneActive, IShellViewModel
	{
		public DesignerShellViewModel()
		{
			Items.Add(new EmptyNamedViewModel("items"));
			Items.Add(new EmptyNamedViewModel("feeds"));
			Items.Add(new EmptyNamedViewModel("settings"));
			ActivateItem(Items[0]);
		}
	}
}
