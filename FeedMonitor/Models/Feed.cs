using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Services;

namespace FeedMonitor.Models
{
	public class Feed
	{
		private readonly IFeedDownloader feedDownloader;
		private readonly IList<FeedItem> items = new List<FeedItem>();
		private readonly string url;

		public IList<FeedItem> Items
		{
			get { return items; }
		}

		public string Title
		{
			get;
			private set;
		}

		public string Url
		{
			get { return url; }
		}

		public Feed(IFeedDownloader feedDownloader, string url)
		{
			Contract.Requires(feedDownloader != null);
			Contract.Requires(!string.IsNullOrEmpty(url));

			this.feedDownloader = feedDownloader;
			this.url = url;
		}

		public void Update()
		{
			var syndicationFeed = feedDownloader.GetFeed(url);
			Title = syndicationFeed.Title.Text;

			CopyItems(syndicationFeed.Items);
		}

		private void CopyItems(IEnumerable<SyndicationItem> sourceItems)
		{
			items.Clear();

			foreach (var sourceItem in sourceItems.OrderByDescending(item => item.PublishDate))
			{
				var newItem = FeedItem.FromSyndicationItem(sourceItem);
				items.Add(newItem);
			}
		}
	}
}
