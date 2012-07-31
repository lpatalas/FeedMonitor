using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FeedMonitor.ViewModels.Designer
{
	public class DesignerMessageBoxViewModel : MessageBoxViewModel
	{
		public DesignerMessageBoxViewModel()
			: base(designerMessage, designerTitle, designerButtons)
		{
		}

		private static readonly MessageBoxButtonMapping[] designerButtons =
		{
			new MessageBoxButtonMapping("Delete", MessageBoxResult.Yes),
			new MessageBoxButtonMapping("Cancel", MessageBoxResult.No)
		};

		private const string designerMessage = "Do you really want to remove specified item?";
		private const string designerTitle = "Confirm removal";
	}
}
