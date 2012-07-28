using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public class FeedSourceFactory : IFeedSourceFactory
	{
		public IFeedSource CreateFromUrl(string url)
		{
			return new FeedSource(url);
		}
	}
}
