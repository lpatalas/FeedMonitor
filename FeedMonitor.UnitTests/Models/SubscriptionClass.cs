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

		public class RefreshFeedMethod : TestBase
		{
			[Fact]
			public void Should_use_IFeedDownloader_to_get_feed_data_from_specified_url()
			{
				// Arrange
				string requestedUrl = null;

				feedDownloader.GetFeed = (url) =>
				{
					requestedUrl = url;
					return feedDownloader.GetFeedDefaultImpl(url);
				};

				// Act
				var task = subscription.RefreshFeed();
				task.Wait();

				// Assert
				requestedUrl.Should().Be(subscription.Url);
			}
		}

		public class TitleProperty : TestBase
		{
			[Fact]
			public void Should_contain_title_specified_in_feed()
			{
				// Arrange

				// Act
				var task = subscription.RefreshFeed();
				task.Wait();

				// Assert
				subscription.Title.Should().Be(feedDownloader.FeedTitle);
			}
		}
	}
}
