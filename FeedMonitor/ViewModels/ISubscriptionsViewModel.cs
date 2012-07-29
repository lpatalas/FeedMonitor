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
		IEnumerable<Subscription> Subscriptions { get; }

		void AddSubscription(string url);
		void RemoveSubscription(Subscription subscription);
	}
}
