using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FeedMonitor.UnitTests.Fakes;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.Models
{
	public class SubscriptionsClass
	{
		public abstract class Test
		{
			protected readonly FakeFeedDownloader feedDownloader;
			protected readonly FakeFeedFactory feedFactory;
			protected readonly Subscriptions subscriptions;

			public Test()
			{
				feedDownloader = new FakeFeedDownloader();
				feedFactory = new FakeFeedFactory(feedDownloader);
				subscriptions = new Subscriptions(feedFactory);
			}
		}

		public class AddMethod : Test
		{
			[Fact]
			public void Should_create_feed_for_specified_URL_and_add_it_to_the_list_of_feeds()
			{
				// Arrange
				var url = @"http://blogs.msdn.com/b/ericlippert/rss.aspx";

				// Act
				subscriptions.Add(url);

				// Assert
				subscriptions.Feeds.Should().Contain(feed => feed.Url.Equals(url, StringComparison.Ordinal));
			}

			[Fact]
			public void Should_throw_when_URL_is_already_subscribed()
			{
				// Arrange
				var url = @"http://blogs.msdn.com/b/ericlippert/rss.aspx";
				subscriptions.Add(url);

				// Act
				Action act = () => { subscriptions.Add(url); };

				// Assert
				act.ShouldThrow<InvalidOperationException>();
			}
		}

		public class RemoveMethod : Test
		{
			[Fact]
			public void Should_remove_feed_from_list_of_feeds()
			{
				// Arrange
				var url = @"http://blogs.msdn.com/b/ericlippert/rss.aspx";
				subscriptions.Add(url);

				var feed = subscriptions.Feeds.First();

				// Act
				subscriptions.Remove(feed);

				// Assert
				subscriptions.Feeds.Should().NotContain(feed);
			}
		}
	}
}
