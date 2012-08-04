using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace FeedMonitor.ViewModels.Designer
{
	public class EmptyNamedViewModel : IHaveDisplayName
	{
		public string DisplayName { get; set; }

		public EmptyNamedViewModel(string displayName)
		{
			Contract.Requires(!string.IsNullOrEmpty(displayName));
			this.DisplayName = displayName;
		}
	}
}
