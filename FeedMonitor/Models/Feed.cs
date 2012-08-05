using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using FeedMonitor.Services;

namespace FeedMonitor.Models
{
	public class Feed
	{
		private readonly IFeedDownloader feedDownloader;
		private readonly IObservableCollection<FeedItem> items = new BindableCollection<FeedItem>();
		private readonly string url;

		public IObservableCollection<FeedItem> Items
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

			CopyItems(syndicationFeed);
		}

		private void CopyItems(SyndicationFeed sourceFeed)
		{
			items.IsNotifying = false;
			items.Clear();

			foreach (var sourceItem in sourceFeed.Items)
			{
				var newItem = FeedItem.FromSyndicationItem(sourceItem, sourceFeed);
				items.Add(newItem);
			}

			items.IsNotifying = true;
			items.Refresh();
		}
	}
}
