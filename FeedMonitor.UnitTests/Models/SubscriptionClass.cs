using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FeedMonitor.Models;
using FeedMonitor.Services;
using FeedMonitor.UnitTests.Fakes;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.Models
{
	public class SubscriptionClass
	{
		public abstract class TestBase
		{
			protected readonly FakeFeedDownloader feedDownloader;
			protected readonly Subscription subscription;
			protected const string subscriptionUrl = "http://www.subscription.com/rss";

			public TestBase()
			{
				feedDownloader = new FakeFeedDownloader();
				subscription = new Subscription(subscriptionUrl, feedDownloader);
			}
		}
	}
}
