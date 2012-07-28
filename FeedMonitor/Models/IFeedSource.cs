using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public interface IFeedSource
	{
		IEnumerable<FeedItem> AllItems { get; }
	}
}
