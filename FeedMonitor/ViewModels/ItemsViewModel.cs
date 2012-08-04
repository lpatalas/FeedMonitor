using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Caliburn.Micro;
using FeedMonitor.Models;
using FeedMonitor.Services;

namespace FeedMonitor.ViewModels
{
	public class ItemsViewModel : IItemsViewModel
	{
		private readonly CompositeCollection aggregatedFeedItems = new CompositeCollection();
		private readonly ICollectionView feedItemsView;
		private readonly ISubscriptions subscriptions;

		public string DisplayName { get; set; }

		public ICollectionView FeedItems
		{
			get { return feedItemsView; }
		}

		public ItemsViewModel(ISubscriptions subscriptions)
		{
			Contract.Requires(subscriptions != null);

			this.DisplayName = "items";
			this.feedItemsView = CollectionViewSource.GetDefaultView(aggregatedFeedItems);
			this.subscriptions = subscriptions;
			
			subscriptions.Feeds.CollectionChanged += OnFeedCollectionChanged;
		}

		private void OnFeedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (Feed feed in e.NewItems)
					aggregatedFeedItems.Add(new CollectionContainer { Collection = feed.Items });
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (Feed feed in e.OldItems)
				{
					var itemToRemove = aggregatedFeedItems
						.OfType<CollectionContainer>()
						.Where(container => container.Collection == feed.Items)
						.FirstOrDefault();

					if (itemToRemove != null)
						aggregatedFeedItems.Remove(itemToRemove);
				}
			}
		}
	}
}
