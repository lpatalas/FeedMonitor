using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using FeedMonitor.Services;

namespace FeedMonitor.ViewModels
{
	public class ShellViewModel
	{
		private readonly ISubscriptionsViewModel subscriptionsViewModel;

		public object CurrentViewModel { get; private set; }

		public ShellViewModel(ISubscriptionsViewModel subscriptionsViewModel)
		{
			Contract.Requires(subscriptionsViewModel != null);

			this.subscriptionsViewModel = subscriptionsViewModel;
			this.CurrentViewModel = subscriptionsViewModel;
		}
	}
}
