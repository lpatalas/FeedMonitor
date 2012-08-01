using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;

namespace FeedMonitor.ViewModels
{
	public interface ISubscriptionsViewModel
	{
		IEnumerable<Feed> Subscriptions { get; }

		void AddSubscription(string url);
		void RemoveSubscription(Feed subscription);
	}
}
