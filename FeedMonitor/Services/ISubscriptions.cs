using System;
using System.Collections.Generic;
using Caliburn.Micro;
using FeedMonitor.Models;

namespace FeedMonitor.Services
{
	public interface ISubscriptions
	{
		IObservableCollection<Feed> Feeds { get; }

		void Add(string url);
		void Remove(Feed feed);
	}
}
