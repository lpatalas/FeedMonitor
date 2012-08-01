using System;
using System.Collections.Generic;

namespace FeedMonitor.Models
{
	public interface ISubscriptions
	{
		IList<Feed> Feeds { get; }

		void Add(string url);
		void Remove(Feed feed);
	}
}
