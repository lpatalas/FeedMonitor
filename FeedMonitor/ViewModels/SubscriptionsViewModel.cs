using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using FeedMonitor.Models;

namespace FeedMonitor.ViewModels
{
	public class SubscriptionsViewModel : PropertyChangedBase
	{
		private readonly BindableCollection<Subscription> subscriptions = new BindableCollection<Subscription>();

		public IEnumerable<Subscription> Subscriptions
		{
			get { return subscriptions; }
		}

		public void AddSubscription(string sourceUrl)
		{
			subscriptions.Add(new Subscription(sourceUrl));
		}

		public void RemoveSubscription(Subscription subscription)
		{
			subscriptions.Remove(subscription);
		}
	}
}
