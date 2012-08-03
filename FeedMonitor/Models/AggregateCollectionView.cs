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
			collections.Add(input);
			RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, input));
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
