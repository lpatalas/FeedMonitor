using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public interface IFeedAggregator
	{
		IEnumerable<FeedItem> AllItems { get; }

		IEnumerable<IFeedSource> Sources { get; }

		void AddSource(IFeedSource source);
	}
}
