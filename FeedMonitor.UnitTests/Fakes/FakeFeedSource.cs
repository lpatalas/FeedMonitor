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

		public IEnumerable<FeedItem> AllItems
		{
			get { return items; }
		}

		public FakeFeedSource(IEnumerable<FeedItem> items)
		{
			Contract.Requires(items != null);
			this.items = items;
		}
	}
}
