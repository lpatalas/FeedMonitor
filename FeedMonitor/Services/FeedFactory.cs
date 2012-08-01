using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;

namespace FeedMonitor.Services
{
	public class FeedFactory : IFeedFactory
	{
		private readonly IFeedDownloader feedDownloader;

		public FeedFactory()
			: this(new FeedDownloader())
		{
		}

		public FeedFactory(IFeedDownloader feedDownloader)
		{
			this.feedDownloader = feedDownloader;
		}

		public Feed Create(string url)
		{
			return new Feed(feedDownloader, url);
		}
	}
}
