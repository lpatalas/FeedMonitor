using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Services;

namespace FeedMonitor.Models
{
	public class SubscriptionFactory : ISubscriptionFactory
	{
		private readonly IFeedProvider feedProvider;

		public SubscriptionFactory()
			: this(new FeedProvider())
		{
		}

		public SubscriptionFactory(IFeedProvider feedProvider)
		{
			this.feedProvider = feedProvider;
		}

		public Subscription Create(string url)
		{
			return new Subscription(url, feedProvider);
		}
	}
}
