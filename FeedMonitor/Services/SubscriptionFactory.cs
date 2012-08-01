using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;

namespace FeedMonitor.Services
{
	public class SubscriptionFactory : ISubscriptionFactory
	{
		private readonly IFeedDownloader feedDownloader;

		public SubscriptionFactory()
			: this(new FeedDownloader())
		{
		}

		public SubscriptionFactory(IFeedDownloader feedDownloader)
		{
			this.feedDownloader = feedDownloader;
		}

		public Subscription Create(string url)
		{
			return new Subscription(url, feedDownloader);
		}
	}
}
