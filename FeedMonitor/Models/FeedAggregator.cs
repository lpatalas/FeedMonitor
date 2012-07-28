using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace FeedMonitor.Models
{
	public class FeedAggregator : PropertyChangedBase, IFeedAggregator
	{
		private readonly IList<IFeedSource> sources = new List<IFeedSource>();

		public IEnumerable<FeedItem> AllItems
		{
			get
			{
				return sources
					.SelectMany(source => source.AllItems)
					.OrderBy(item => item.PublishDate);
			}
		}

		public IEnumerable<IFeedSource> Sources
		{
			get { return sources; }
		}

		public FeedAggregator()
		{
		}

		public void AddSource(IFeedSource source)
		{
			sources.Add(source);
		}
	}
}
