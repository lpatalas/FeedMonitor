using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.ViewModels.Designer
{
	class DesignerMessageBoxViewModel : MessageBoxViewModel
	{
		public DesignerMessageBoxViewModel()
			: base(designerMessage, designerTitle)
		{
		}

		private const string designerMessage = "Do you really want to remove specified item?";
		private const string designerTitle = "Confirm removal";
	}
}
