using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.ViewModels.Designer
{
	public class DesignerSubscriptionsViewModel : ISubscriptionsViewModel
	{
		private readonly IFeedDownloader feedDownloader = new NullFeedDownloader();
		private readonly IList<Feed> feeds = new List<Feed>();

		public IEnumerable<Feed> Feeds
		{
			get { return feeds; }
		}

		public DesignerSubscriptionsViewModel()
		{
			feeds.Add(new Feed(feedDownloader, @"http://blogs.msdn.com/b/ericlippert/rss.aspx"));
			feeds.Add(new Feed(feedDownloader, @"http://blogs.msdn.com/b/oldnewthing/rss.aspx"));
			feeds.Add(new Feed(feedDownloader, @"http://feeds.feedburner.com/ScottHanselman"));
		}

		public void Subscribe(string url)
		{
			throw new NotImplementedException();
		}

		public void Unsubscribe(Feed feed)
		{
			throw new NotImplementedException();
		}
	}
}
