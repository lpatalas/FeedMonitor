using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.UnitTests.Fakes
{
	public class FakeFeedDownloader : IFeedDownloader
	{
		public IList<FeedItem> FeedItems { get; private set; }
		public string FeedTitle { get; set; }
		public string FeedUrl { get; set; }
		public Func<string, SyndicationFeed> GetFeed;

		public FakeFeedDownloader()
		{
			FeedItems = new List<FeedItem>();
			FeedTitle = "Test Feed";
			FeedUrl = "http://www.test.com/";
			GetFeed = GetFeedDefaultImpl;
		}

		SyndicationFeed IFeedDownloader.GetFeed(string url)
		{
			return GetFeed(url);
		}

		public SyndicationFeed GetFeedDefaultImpl(string url)
		{
			return GenerateFeed();
		}

		private SyndicationFeed GenerateFeed()
		{
			var feed = new SyndicationFeed();
			feed.Title = new TextSyndicationContent(FeedTitle);

			var items = from item in FeedItems
						select new SyndicationItem
						{
							Id = item.Id,
							PublishDate = item.PublishDate,
							SourceFeed = feed,
							Title = new TextSyndicationContent(item.Title)
						};

			feed.Items = items.ToList();

			return feed;
		}
	}
}
