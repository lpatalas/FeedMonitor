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
		private readonly IList<Feed> subscriptions = new List<Feed>();

		public IEnumerable<Feed> Subscriptions
		{
			get { return subscriptions; }
		}

		public DesignerSubscriptionsViewModel()
		{
			subscriptions.Add(new Feed(feedDownloader, @"http://blogs.msdn.com/b/ericlippert/rss.aspx"));
			subscriptions.Add(new Feed(feedDownloader, @"http://blogs.msdn.com/b/oldnewthing/rss.aspx"));
			subscriptions.Add(new Feed(feedDownloader, @"http://feeds.feedburner.com/ScottHanselman"));
		}

		public void AddSubscription(string url)
		{
			throw new NotImplementedException();
		}

		public void RemoveSubscription(Feed subscription)
		{
			throw new NotImplementedException();
		}
	}
}
