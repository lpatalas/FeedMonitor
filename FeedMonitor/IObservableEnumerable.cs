using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor
{
	public interface IObservableEnumerable<out T> : IEnumerable<T>, INotifyCollectionChanged
	{
	}
}
