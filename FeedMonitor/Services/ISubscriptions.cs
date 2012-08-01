using System;
using System.Collections.Generic;
using FeedMonitor.Models;

namespace FeedMonitor.Services
{
	public interface ISubscriptions
	{
		IList<Feed> Feeds { get; }

		void Add(string url);
		void Remove(Feed feed);
	}
}
