using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FeedMonitor.Services
{
	public class FeedProvider : IFeedProvider
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
