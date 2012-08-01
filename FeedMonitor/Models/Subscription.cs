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
	public class Subscription : Feed
	{
		public Subscription(string url, IFeedDownloader feedDownloader)
			: base(feedDownloader, url)
		{
		}
	}
}
