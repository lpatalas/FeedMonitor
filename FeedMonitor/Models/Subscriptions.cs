using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Services;

namespace FeedMonitor.Models
{
	public class Subscriptions
	{
		private readonly IFeedFactory feedFactory;
		private readonly IList<Feed> feeds = new List<Feed>();

		public IList<Feed> Feeds
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
			feeds.Add(feedFactory.Create(url));
		}

		public void Remove(Feed feed)
		{
			feeds.Remove(feed);
		}
	}
}
