using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace FeedMonitor.Services
{
	public class FeedDownloader : IFeedDownloader
	{
		public SyndicationFeed GetFeed(string url)
		{
			using (var reader = XmlReader.Create(url))
			{
				return SyndicationFeed.Load(reader);
			}
		}
	}
}
