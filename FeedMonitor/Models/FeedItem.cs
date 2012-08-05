using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public class FeedItem : IEquatable<FeedItem>
	{
		private readonly string feedTitle;
		private readonly string id;
		private readonly DateTimeOffset publishDate;
		private readonly string title;

		public string FeedTitle
		{
			get { return feedTitle; }
		}

		public string Id
		{
			get { return id; }
		}

		public DateTimeOffset PublishDate
		{
			get { return publishDate; }
		}

		public string Title
		{
			get { return title; }
		}

		public FeedItem(string id, DateTimeOffset publishDate, string title)
			: this(string.Empty, id, publishDate, title)
		{
		}

		public FeedItem(string feedTitle, string id, DateTimeOffset publishDate, string title)
		{
			this.feedTitle = feedTitle;
			this.id = id;
			this.publishDate = publishDate;
			this.title = title;
		}

		public static FeedItem FromSyndicationItem(SyndicationItem sourceItem, SyndicationFeed sourceFeed)
		{
			Contract.Requires(sourceItem != null);
			Contract.Requires(sourceFeed != null);

			return new FeedItem(sourceItem, sourceFeed);
		}

		public override bool Equals(object obj)
		{
			var otherItem = obj as FeedItem;
			return otherItem != null && otherItem.Equals(this);
		}

		public bool Equals(FeedItem other)
		{
			return id.Equals(other.id, StringComparison.Ordinal);
		}

		public override int GetHashCode()
		{
			return id.GetHashCode();
		}

		private FeedItem(SyndicationItem item, SyndicationFeed sourceFeed)
		{
			this.feedTitle = sourceFeed.Title.Text;
			this.id = item.Id;
			this.publishDate = item.PublishDate;
			this.title = item.Title.Text;
		}
	}
}
