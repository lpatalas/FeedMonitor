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
			protected readonly IFeedProvider feedProvider;
			protected readonly Subscription subscription;
			protected const string subscriptionUrl = "http://www.subscription.com/rss";

			public TestBase()
			{
				feedProvider = new FakeFeedProvider();
				subscription = new Subscription(subscriptionUrl, feedProvider);
			}
		}

		public class TitleProperty : TestBase
		{
			[Fact]
			public void Should_contain_title_specified_in_feed()
			{
				// Arrange

				// Act
				var task = subscription.Refresh();
				task.Wait();

				// Assert
				subscription.Title.Should().Be(FakeFeedProvider.FeedTitle);
			}
		}
	}
}
