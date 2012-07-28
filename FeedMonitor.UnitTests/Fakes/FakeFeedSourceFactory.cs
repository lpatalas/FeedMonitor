using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;

namespace FeedMonitor.UnitTests.Fakes
{
	public class FakeFeedSourceFactory : IFeedSourceFactory
	{
		public virtual IFeedSource CreateFromUrl(string url)
		{
			return new FakeFeedSource(url);
		}
	}
}
