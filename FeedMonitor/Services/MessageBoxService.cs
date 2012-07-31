using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using FeedMonitor.ViewModels;

namespace FeedMonitor.Services
{
	public class MessageBoxService : IMessageBoxService
	{
		private readonly IWindowManager windowManager;

		public MessageBoxService(IWindowManager windowManager)
		{
			Contract.Requires(windowManager != null);
			this.windowManager = windowManager;
		}

		public bool ShowYesNoDialog(string message, string title)
		{
			var viewModel = new MessageBoxViewModel(message, title, yesNoButtons);
			windowManager.ShowDialog(viewModel);
			return viewModel.Result == MessageBoxResult.Yes;
		}

		private static readonly MessageBoxButtonMapping[] yesNoButtons =
		{
			new MessageBoxButtonMapping("Yes", MessageBoxResult.Yes),
			new MessageBoxButtonMapping("No", MessageBoxResult.No)
		};
	}
}
