using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace FeedMonitor.Models
{
	public class FeedSource : IFeedSource
	{
		public IEnumerable<FeedItem> AllItems { get; set; }

		public FeedSource(string url)
		{
			Contract.Requires(!string.IsNullOrEmpty(url));

			AllItems = new BindableCollection<FeedItem>();
		}
	}
}
