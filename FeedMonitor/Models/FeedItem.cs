using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public class FeedItem : IEquatable<FeedItem>
	{
		private readonly string id;
		private readonly DateTimeOffset publishDate;
		private readonly string title;

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
		{
			this.id = id;
			this.publishDate = publishDate;
			this.title = title;
		}

		public static FeedItem FromSyndicationItem(SyndicationItem sourceItem)
		{
			return new FeedItem(sourceItem);
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

		private FeedItem(SyndicationItem item)
		{
			this.id = item.Id;
			this.title = item.Title.Text;
		}
	}
}
