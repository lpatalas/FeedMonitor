using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Services;

namespace FeedMonitor.Models
{
	public class Feed
	{
		private readonly IFeedProvider feedProvider;
		private readonly IList<FeedItem> items = new List<FeedItem>();
		private readonly string url;

		public IList<FeedItem> Items
		{
			get { return items; }
		}

		public Feed(IFeedProvider feedProvider, string url)
		{
			Contract.Requires(feedProvider != null);
			Contract.Requires(!string.IsNullOrEmpty(url));

			this.feedProvider = feedProvider;
			this.url = url;
		}
	}
}
