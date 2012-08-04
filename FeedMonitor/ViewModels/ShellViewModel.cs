using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using FeedMonitor.Services;

namespace FeedMonitor.ViewModels
{
	public class ShellViewModel : Conductor<object>.Collection.OneActive, IShellViewModel
	{
		public object SelectedScreen
		{
			get { return ActiveItem; }
			set { ActivateItem(value); }
		}

		public ShellViewModel(
			IItemsViewModel itemsViewModel,
			ISubscriptionsViewModel subscriptionsViewModel)
		{
			Contract.Requires(itemsViewModel != null);
			Contract.Requires(subscriptionsViewModel != null);

			Items.Add(itemsViewModel);
			Items.Add(subscriptionsViewModel);

			ActivateItem(itemsViewModel);
		}

		public override void ActivateItem(object item)
		{
			base.ActivateItem(item);
		}
	}
}
