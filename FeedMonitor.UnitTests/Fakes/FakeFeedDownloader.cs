using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FeedMonitor.Services;

namespace FeedMonitor.UnitTests.Fakes
{
	public class FakeFeedProvider : IFeedProvider
	{
		public const string FeedTitle = "Test Feed";
		public const string FeedUrl = "http://www.test.com/";

		public Func<string, SyndicationFeed> GetFeed;

		public FakeFeedProvider()
		{
			GetFeed = GetFeedDefaultImpl;
		}

		SyndicationFeed IFeedProvider.GetFeed(string url)
		{
			return GetFeed(url);
		}

		public SyndicationFeed GetFeedDefaultImpl(string url)
		{
			using (var reader = XmlReader.Create(new StringReader(feedSource)))
			{
				return SyndicationFeed.Load(reader);
			}
		}

		private const string feedSource = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<rss version=""2.0"">
<channel>
        <title>" + FeedTitle + @"</title>
        <description>This is an example of an RSS feed</description>
        <link>" + FeedUrl + @"</link>
        <lastBuildDate>Mon, 06 Sep 2010 00:01:00 +0000 </lastBuildDate>
        <pubDate>Mon, 06 Sep 2009 16:45:00 +0000 </pubDate>
        <ttl>1800</ttl>
 
        <item>
                <title>Example entry</title>
                <description>Here is some text containing an interesting description.</description>
                <link>http://www.wikipedia.org/</link>
                <guid>unique string per item</guid>
                <pubDate>Mon, 06 Sep 2009 16:45:00 +0000 </pubDate>
        </item>
 
</channel>
</rss>
";
	}
}
