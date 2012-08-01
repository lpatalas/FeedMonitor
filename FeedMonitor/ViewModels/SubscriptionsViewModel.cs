using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
		private readonly BindableCollection<Feed> feeds = new BindableCollection<Feed>();
		private readonly IMessageBoxService messageBoxService;
		private readonly ISubscriptions subscriptions;

		public IEnumerable<Feed> Feeds
		{
			get { return subscriptions.Feeds; }
		}

		public SubscriptionsViewModel(
			IFeedFactory feedFactory,
			IMessageBoxService messageBoxService,
			ISubscriptions subscriptions)
		{
			Contract.Requires(feedFactory != null);
			Contract.Requires(messageBoxService != null);
			Contract.Requires(subscriptions != null);

			this.feedFactory = feedFactory;
			this.messageBoxService = messageBoxService;
			this.subscriptions = subscriptions;
		}

		public void Subscribe(string sourceUrl)
		{
			if (string.IsNullOrEmpty(sourceUrl))
				return;

			var alreadyExists = subscriptions.Feeds.Any(item => item.Url.Equals(sourceUrl, StringComparison.Ordinal));
			if (!alreadyExists)
				subscriptions.Add(sourceUrl);
		}

		public void Unsubscribe(Feed feed)
		{
			var userConfirmed = messageBoxService.ShowYesNoDialog("Do you really want to unsubscribe specified feed?", "Unsubscribe feed?");

			if (userConfirmed)
				subscriptions.Remove(feed);
		}
	}
}
