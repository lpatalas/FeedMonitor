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
		private const string defaultTitle = "Untitled";

		private readonly string title;
		private readonly string url;

		public string Title
		{
			get { return title; }
		}

		public string Url
		{
			get { return url; }
		}

		public Subscription(string url)
			: this(defaultTitle, url)
		{
		}

		public Subscription(string title, string url)
		{
			Contract.Requires(url != null);
			this.title = title;
			this.url = url;
		}
	}
}
