using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FeedMonitor.Services
{
	public class MessageBoxService : IMessageBoxService
	{
		public bool ShowYesNoDialog(string title, string message)
		{
			var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
			return result == MessageBoxResult.Yes;
		}
	}
}
