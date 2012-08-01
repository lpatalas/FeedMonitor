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
		IEnumerable<Feed> Feeds { get; }

		void Subscribe(string url);
		void Unsubscribe(Feed feed);
	}
}
