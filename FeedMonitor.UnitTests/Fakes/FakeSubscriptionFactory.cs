using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.UnitTests.Fakes
{
	public class FakeSubscriptionFactory : ISubscriptionFactory
	{
		private readonly IFeedProvider feedProvider;

		public FakeSubscriptionFactory(IFeedProvider feedProvider)
		{
			this.feedProvider = feedProvider;
		}

		public virtual Subscription Create(string url)
		{
			return new Subscription(url, feedProvider);
		}
	}
}
