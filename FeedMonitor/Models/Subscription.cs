using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public class Subscription
	{
		private readonly string url;

		public string Url
		{
			get { return url; }
		}

		public Subscription(string url)
		{
			Contract.Requires(url != null);
			this.url = url;
		}
	}
}
