using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;

namespace FeedMonitor.Services
{
	public interface ISubscriptionFactory
	{
		Feed Create(string url);
	}
}
