using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using FeedMonitor.Services;

namespace FeedMonitor.ViewModels
{
	public class ShellViewModel : Conductor<object>.Collection.OneActive
	{
		private readonly ISubscriptionsViewModel subscriptionsViewModel;

		public ShellViewModel(ISubscriptionsViewModel subscriptionsViewModel)
		{
			Contract.Requires(subscriptionsViewModel != null);
			this.subscriptionsViewModel = subscriptionsViewModel;

			ActivateItem(this.subscriptionsViewModel);
		}
	}
}
