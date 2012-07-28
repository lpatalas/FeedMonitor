﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Caliburn.Micro;
using FeedMonitor.Models;

namespace FeedMonitor.ViewModels
{
	public class ReaderViewModel : PropertyChangedBase
	{
		private readonly IFeedAggregator aggregator;
		private readonly IFeedSourceFactory feedSourceFactory;

		public BindableCollection<FeedItem> Results { get; set; }

		public string SourceUrl { get; set; }

		public ReaderViewModel()
			: this(new FeedAggregator(), new FeedSourceFactory())
		{
		}

		public ReaderViewModel(IFeedAggregator aggregator, IFeedSourceFactory feedSourceFactory)
		{
			this.aggregator = aggregator ?? new FeedAggregator();
			this.feedSourceFactory = feedSourceFactory ?? new FeedSourceFactory();

			this.Results = new BindableCollection<FeedItem>();
			this.SourceUrl = @"http://blogs.msdn.com/b/ericlippert/rss.aspx";
		}

		public void Download()
		{
			if (string.IsNullOrEmpty(SourceUrl))
				return;

			using (var reader = XmlTextReader.Create(SourceUrl))
			{
				var feed = SyndicationFeed.Load(reader);
				CopyResults(feed);
			}
		}

		private void CopyResults(SyndicationFeed feed)
		{
			Results.IsNotifying = false;
			Results.Clear();

			foreach (var item in feed.Items)
			{
				Results.Add(FeedItem.FromSyndicationItem(item));
			}

			Results.IsNotifying = true;
			Results.Refresh();
		}

		public void AddFeed(string sourceUrl)
		{
			var feedSource = feedSourceFactory.CreateFromUrl(sourceUrl);
			aggregator.AddSource(feedSource);
		}
	}
}
