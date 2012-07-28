using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;

namespace FeedMonitor.UnitTests.Fakes
{
	public class FakeFeedSource : IFeedSource
	{
		private readonly IEnumerable<FeedItem> items;
		private readonly string sourceUrl;

		public IEnumerable<FeedItem> AllItems
		{
			get { return items; }
		}

		public string SourceUrl
		{
			get { return sourceUrl; }
		}

		public FakeFeedSource()
			: this(Enumerable.Empty<FeedItem>())
		{
		}

		public FakeFeedSource(string url)
			: this(url, Enumerable.Empty<FeedItem>())
		{
		}

		public FakeFeedSource(IEnumerable<FeedItem> items)
			: this(string.Empty, items)
		{
			Contract.Requires(items != null);
			this.items = items;
		}

		public FakeFeedSource(string url, IEnumerable<FeedItem> items)
		{
			this.items = items;
			this.sourceUrl = url;
		}
	}
}
