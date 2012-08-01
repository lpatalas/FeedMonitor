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
		private readonly string title;

		public string Title
		{
			get { return title; }
		}

		public static FeedItem FromSyndicationItem(SyndicationItem sourceItem)
		{
			return new FeedItem(sourceItem);
		}

		private FeedItem(SyndicationItem item)
		{
			this.title = item.Title.Text;
		}
	}
}
