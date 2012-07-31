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

		private readonly IFeedProvider feedProvider;
		private readonly string url;

		public string Title
		{
			get;
			private set;
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
		}

		public async Task Refresh()
		{
			SetLoadingTitle();
			var feed = await LoadFeed();
			Title = feed.Title.Text;
		}

		private Task<SyndicationFeed> LoadFeed()
		{
			return Task.Factory.StartNew(() => feedProvider.GetFeed(Url));
		}

		private void SetLoadingTitle()
		{
			if (string.IsNullOrEmpty(Title))
				Title = "Loading...";
			else
				Title = string.Format("{0} (refreshing ...)", Title);
		}
	}
}
