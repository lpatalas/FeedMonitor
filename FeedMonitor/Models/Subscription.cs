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
	public class Subscription : PropertyChangedBase
	{
		private const string defaultTitle = "Untitled";

		private readonly Feed feed;
		private readonly IFeedDownloader feedDownloader;
		private readonly string url;

		public Feed Feed
		{
			get { return feed; }
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

		public Subscription(string url, IFeedDownloader feedDownloader)
			: this(defaultTitle, url, feedDownloader)
		{
		}

		public Subscription(string title, string url, IFeedDownloader feedDownloader)
		{
			Contract.Requires(feedDownloader != null);
			Contract.Requires(!string.IsNullOrEmpty(url));

			this.feed = new Feed(feedDownloader, url);
			this.feedDownloader = feedDownloader;
			this.Title = title;
			this.url = url;
		}
	}
}
