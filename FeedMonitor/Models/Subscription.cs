using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using FeedMonitor.Services;

namespace FeedMonitor.Models
{
	public class Subscription : PropertyChangedBase
	{
		private const string defaultTitle = "Untitled";

		private readonly IFeedProvider feedProvider;

		public string Title { get; private set; }
		public string Url { get; private set; }

		public Subscription(string url, IFeedProvider feedProvider)
			: this(defaultTitle, url, feedProvider)
		{
		}

		public Subscription(string title, string url, IFeedProvider feedProvider)
		{
			Contract.Requires(feedProvider != null);
			Contract.Requires(url != null);

			this.Url = url;
			this.feedProvider = feedProvider;

			RefreshFeed();
		}

		private void RefreshFeed()
		{
			if (string.IsNullOrEmpty(Title))
				Title = "Loading...";
			else
				Title = string.Format("{0} (refreshing ...)", Title);
			
			Task.Factory.StartNew(() =>
			{
				var feed = feedProvider.GetFeed(Url);
				this.Title = feed.Title.Text;
			});
		}
	}
}
