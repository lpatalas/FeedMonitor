using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Services
{
	public interface IFeedProvider
	{
		SyndicationFeed GetFeed(string url);
	}
}
