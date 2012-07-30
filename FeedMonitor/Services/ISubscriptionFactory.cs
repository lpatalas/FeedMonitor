using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public interface ISubscriptionFactory
	{
		Subscription Create(string url);
	}
}
