using System;
using System.Windows;
using System.Windows.Controls;
using FeedMonitor.Services;

namespace FeedMonitor.ViewModels
{
	public class ShellViewModel
	{
		public object CurrentViewModel { get; private set; }

		public ShellViewModel()
		{
			this.CurrentViewModel = new SubscriptionsViewModel();
		}
	}
}
