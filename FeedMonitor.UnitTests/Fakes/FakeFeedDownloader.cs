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
			var feedSource = GenerateFeed();
			var sourceReader = new StringReader(feedSource);

			using (var reader = XmlReader.Create(sourceReader))
			{
				return SyndicationFeed.Load(reader);
			}
		}

		private string GenerateFeed()
		{
			var xml = new XElement("rss",
				new XAttribute("version", "2.0"),
				new XElement("channel",
					new XElement("title", FeedTitle),
					new XElement("description", "Test feed"),
					new XElement("link", FeedUrl),
					new XElement("lastBuildDate", "Mon, 06 Sep 2010 00:01:00 +0000"),
					new XElement("pubDate", "Mon, 06 Sep 2009 16:45:00 +0000"),
					new XElement("ttl", 1800),
					from item in FeedItems
					select new XElement("item",
						new XElement("title", item.Title),
						new XElement("description", "Item description"),
						new XElement("link", "http://website.org/item"),
						new XElement("guid", item.Id),
						new XElement("pubDate", "Mon, 06 Sep 2009 16:45:00 +0000")
					)
				)
			);

			return xml.ToString();
		}
	}
}
