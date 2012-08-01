using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.UnitTests.Fakes
{
	public class FakeFeedFactory : IFeedFactory
	{
		private readonly IFeedDownloader feedDownloader;

		public FakeFeedFactory(IFeedDownloader feedDownloader)
		{
			this.feedDownloader = feedDownloader;
		}

		public virtual Feed Create(string url)
		{
			return new Feed(feedDownloader, url);
		}
	}
}
