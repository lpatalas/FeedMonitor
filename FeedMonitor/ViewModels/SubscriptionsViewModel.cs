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
		private readonly IFeedFactory feedFactory;
		private readonly IMessageBoxService messageBoxService;
		private readonly BindableCollection<Feed> subscriptions = new BindableCollection<Feed>();

		public IEnumerable<Feed> Subscriptions
		{
			get { return subscriptions; }
		}

		public SubscriptionsViewModel(IFeedFactory feedFactory, IMessageBoxService messageBoxService)
		{
			this.feedFactory = feedFactory;
			this.messageBoxService = messageBoxService;
		}

		public void AddSubscription(string sourceUrl)
		{
			var alreadyExists = subscriptions.Any(item => item.Url.Equals(sourceUrl, StringComparison.Ordinal));
			if (!alreadyExists)
			{
				var newSubscription = feedFactory.Create(sourceUrl);
				subscriptions.Add(newSubscription);
			}
		}

		public void RemoveSubscription(Feed subscription)
		{
			var userConfirmed = messageBoxService.ShowYesNoDialog("Do you really want to delete specified subscription?", "Delete subscription?");

			if (userConfirmed)
				subscriptions.Remove(subscription);
		}
	}
}
