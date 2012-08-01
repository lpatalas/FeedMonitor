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
		private readonly BindableCollection<Feed> feeds = new BindableCollection<Feed>();
		private readonly IMessageBoxService messageBoxService;

		public IEnumerable<Feed> Feeds
		{
			get { return feeds; }
		}

		public SubscriptionsViewModel(IFeedFactory feedFactory, IMessageBoxService messageBoxService)
		{
			this.feedFactory = feedFactory;
			this.messageBoxService = messageBoxService;
		}

		public void Subscribe(string sourceUrl)
		{
			var alreadyExists = feeds.Any(item => item.Url.Equals(sourceUrl, StringComparison.Ordinal));
			if (!alreadyExists)
			{
				var newSubscription = feedFactory.Create(sourceUrl);
				feeds.Add(newSubscription);
			}
		}

		public void Unsubscribe(Feed feed)
		{
			var userConfirmed = messageBoxService.ShowYesNoDialog("Do you really want to unsubscribe specified feed?", "Unsubscribe feed?");

			if (userConfirmed)
				feeds.Remove(feed);
		}
	}
}
