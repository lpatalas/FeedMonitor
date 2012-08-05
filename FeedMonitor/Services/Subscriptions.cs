using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.Services
{
	public class Subscriptions : ISubscriptions
	{
		private readonly IFeedFactory feedFactory;
		private readonly IObservableCollection<Feed> feeds = new BindableCollection<Feed>();

		public IObservableCollection<Feed> Feeds
		{
			get { return feeds; }
		}

		public Subscriptions(IFeedFactory feedFactory)
		{
			Contract.Requires(feedFactory != null);

			this.feedFactory = feedFactory;
		}

		public void Add(string url)
		{
			Contract.Requires(!string.IsNullOrEmpty(url));

			if (feeds.Any(feed => feed.Url.Equals(url, StringComparison.Ordinal)))
				throw new InvalidOperationException("URL \"" + url + "\" was already added to the list of feeds.");

			var newFeed = feedFactory.Create(url);
			feeds.Add(newFeed);
			newFeed.Update();
		}

		public void Remove(Feed feed)
		{
			Contract.Requires(feed != null);

			feeds.Remove(feed);
		}
	}
}
