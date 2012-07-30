using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.ViewModels
{
	public class DesignerSubscriptionsViewModel : ISubscriptionsViewModel
	{
		private readonly IFeedProvider feedProvider = new NullFeedProvider();
		private readonly IList<Subscription> subscriptions = new List<Subscription>();

		public IEnumerable<Subscription> Subscriptions
		{
			get { return subscriptions; }
		}

		public DesignerSubscriptionsViewModel()
		{
			subscriptions.Add(new Subscription("Fabulous Adventures In Coding", @"http://blogs.msdn.com/b/ericlippert/rss.aspx", feedProvider));
			subscriptions.Add(new Subscription("The Old New Thing", @"http://blogs.msdn.com/b/oldnewthing/rss.aspx", feedProvider));
			subscriptions.Add(new Subscription("Scott Hanselman", @"http://feeds.feedburner.com/ScottHanselman", feedProvider));
		}

		public void AddSubscription(string url)
		{
			throw new NotImplementedException();
		}

		public void RemoveSubscription(Models.Subscription subscription)
		{
			throw new NotImplementedException();
		}
	}
}
