using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public class FeedItem
	{
		public string Title { get; set; }

		public static FeedItem FromSyndicationItem(SyndicationItem sourceItem)
		{
			return new FeedItem
			{
				Title = sourceItem.Title.Text
			};
		}

		public FeedItem()
		{
		}
	}
}
