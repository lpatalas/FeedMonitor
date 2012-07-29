using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Services;

namespace FeedMonitor.Models
{
	public class Subscription
	{
		private const string defaultTitle = "Untitled";

		private readonly IFeedProvider feedProvider;
		private string title;
		private string url;

		public string Title
		{
			get { return title; }
		}

		public string Url
		{
			get { return url; }
		}

		public Subscription(string url, IFeedProvider feedProvider)
			: this(defaultTitle, url, feedProvider)
		{
		}

		public Subscription(string title, string url, IFeedProvider feedProvider)
		{
			Contract.Requires(feedProvider != null);
			Contract.Requires(url != null);

			this.url = url;
			this.feedProvider = feedProvider;

			RefreshFeed();
		}

		private void RefreshFeed()
		{
			var feed = feedProvider.GetFeed(url);
			this.title = feed.Title.Text;
		}
	}
}
