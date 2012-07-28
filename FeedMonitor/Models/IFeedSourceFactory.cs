using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public interface IFeedSourceFactory
	{
		IFeedSource CreateFromUrl(string url);
	}
}
