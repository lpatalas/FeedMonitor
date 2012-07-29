using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.ViewModels
{
	public class SubscriptionsViewModel : PropertyChangedBase, ISubscriptionsViewModel
	{
		private readonly IMessageBoxService messageBoxService;
		private readonly BindableCollection<Subscription> subscriptions = new BindableCollection<Subscription>();

		public IEnumerable<Subscription> Subscriptions
		{
			get { return subscriptions; }
		}

		public SubscriptionsViewModel()
			: this(new MessageBoxService())
		{
		}

		public SubscriptionsViewModel(IMessageBoxService messageBoxService)
		{
			this.messageBoxService = messageBoxService;
		}

		public void AddSubscription(string sourceUrl)
		{
			var alreadyExists = subscriptions.Any(item => item.Url.Equals(sourceUrl, StringComparison.Ordinal));
			if (!alreadyExists)
				subscriptions.Add(new Subscription(sourceUrl));
		}

		public void RemoveSubscription(Subscription subscription)
		{
			var userConfirmed = messageBoxService.ShowYesNoDialog(
				"Delete subscription?",
				"Do you really want to delete specified subscription?");

			if (userConfirmed)
				subscriptions.Remove(subscription);
		}
	}
}
