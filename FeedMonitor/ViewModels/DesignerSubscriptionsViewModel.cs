using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;

namespace FeedMonitor.ViewModels
{
	public class DesignerSubscriptionsViewModel : ISubscriptionsViewModel
	{
		private readonly IList<Subscription> subscriptions = new List<Subscription>()
		{
			new Subscription("Fabulous Adventures In Coding", @"http://blogs.msdn.com/b/ericlippert/rss.aspx"),
			new Subscription("The Old New Thing", @"http://blogs.msdn.com/b/oldnewthing/rss.aspx"),
			new Subscription("Scott Hanselman", @"http://feeds.feedburner.com/ScottHanselman")
		};

		public IEnumerable<Subscription> Subscriptions
		{
			get { return subscriptions; }
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
