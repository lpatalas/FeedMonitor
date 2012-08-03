using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;

namespace FeedMonitor.Services
{
	public class FeedAggregator
	{
		private readonly AggregateCollectionView<FeedItem> allItems;
		private readonly ISubscriptions subscriptions;

		public IObservableEnumerable<FeedItem> AllItems
		{
			get { return allItems; }
		}

		public FeedAggregator(Subscriptions subscriptions)
		{
			this.allItems = new AggregateCollectionView<FeedItem>(OrderFeedItems);
			this.subscriptions = subscriptions;
			this.subscriptions.Feeds.CollectionChanged += OnSubscriptionsChanged;

			foreach (var feed in subscriptions.Feeds)
				allItems.AddCollection(feed.Items);
		}

		private void OnSubscriptionsChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					AddFeeds(e.NewItems);
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Remove:
					RemoveFeeds(e.OldItems);
					break;
				case NotifyCollectionChangedAction.Replace:
					AddFeeds(e.NewItems);
					RemoveFeeds(e.OldItems);
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					break;
			}
		}

		private void AddFeeds(IList newItems)
		{
			foreach (var feed in newItems.Cast<Feed>())
				allItems.AddCollection(feed.Items);
		}

		private void RemoveFeeds(IList oldItems)
		{
			foreach (var feed in oldItems.Cast<Feed>())
				allItems.RemoveCollection(feed.Items);
		}

		private IEnumerable<FeedItem> OrderFeedItems(IEnumerable<FeedItem> input)
		{
			return input.OrderByDescending(item => item.PublishDate);
		}
	}
}
