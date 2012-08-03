using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace FeedMonitor.Models
{
	public class AggregateCollectionView<TItem> : IEnumerable<TItem>, INotifyCollectionChanged
	{
		private readonly IList<IEnumerable<TItem>> collections = new List<IEnumerable<TItem>>();

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public void AddCollection(IEnumerable<TItem> input)
		{
			if (collections.Contains(input))
				throw new InvalidOperationException("Collection was already added to this view.");

			collections.Add(input);
			HookCollectionChangedEvent(input);
			RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, input));
		}

		public void RemoveCollection(IEnumerable<TItem> collection)
		{
			var wasRemoved = collections.Remove(collection);
			if (wasRemoved)
			{
				UnhookCollectionChangedEvent(collection);
				RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, collection));
			}
		}

		private void HookCollectionChangedEvent(IEnumerable<TItem> collection)
		{
			var observableCollection = collection as INotifyCollectionChanged;
			if (observableCollection != null)
				observableCollection.CollectionChanged += OnContainedCollectionChanged;
		}

		private void UnhookCollectionChangedEvent(IEnumerable<TItem> collection)
		{
			var observableCollection = collection as INotifyCollectionChanged;
			if (observableCollection != null)
				observableCollection.CollectionChanged -= OnContainedCollectionChanged;
		}

		private void OnContainedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaiseCollectionChanged(e);
		}

		private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs eventArgs)
		{
			var handler = CollectionChanged;
			if (handler != null)
				handler(this, eventArgs);
		}

		public IEnumerator<TItem> GetEnumerator()
		{
			var items = collections.SelectMany(collection => collection);
			return items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
