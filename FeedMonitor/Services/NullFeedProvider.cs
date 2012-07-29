using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Services
{
	public class NullFeedProvider : IFeedProvider
	{
		public SyndicationFeed GetFeed(string url)
		{
			var feed = new SyndicationFeed();
			feed.BaseUri = new Uri(url);
			feed.Title = new TextSyndicationContent("No Title");
			return feed;
		}
	}
}
