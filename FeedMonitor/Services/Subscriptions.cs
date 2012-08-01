﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.Services
{
	public class Subscriptions : ISubscriptions
	{
		private readonly IFeedFactory feedFactory;
		private readonly IList<Feed> feeds = new BindableCollection<Feed>();

		public IList<Feed> Feeds
		{
			get { return feeds; }
		}

		public Subscriptions(IFeedFactory feedFactory)
		{
			Contract.Requires(feedFactory != null);

			this.feedFactory = feedFactory;
		}

		public void Add(string url)
		{
			Contract.Requires(!string.IsNullOrEmpty(url));

			if (feeds.Any(feed => feed.Url.Equals(url, StringComparison.Ordinal)))
				throw new InvalidOperationException("URL \"" + url + "\" was already added to the list of feeds.");

			feeds.Add(feedFactory.Create(url));
		}

		public void Remove(Feed feed)
		{
			Contract.Requires(feed != null);

			feeds.Remove(feed);
		}
	}
}
