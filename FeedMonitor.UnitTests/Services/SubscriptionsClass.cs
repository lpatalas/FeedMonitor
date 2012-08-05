using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FeedMonitor.Services;
using FeedMonitor.UnitTests.Fakes;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.Services
{
	public class SubscriptionsClass
	{
		public abstract class Test
		{
			protected readonly FakeFeedDownloader feedDownloader;
			protected readonly FakeFeedFactory feedFactory;
			protected readonly Subscriptions subscriptions;
			protected const string testUrl = @"http://blogs.msdn.com/b/ericlippert/rss.aspx";

			public Test()
			{
				feedDownloader = new FakeFeedDownloader();
				feedFactory = new FakeFeedFactory(feedDownloader);
				subscriptions = new Subscriptions(feedFactory);
			}
		}

		public class FeedsProperty : Test
		{
			[Fact]
			public void Should_notify_when_feed_was_added()
			{
				// Arrange
				var notifier = (INotifyCollectionChanged)subscriptions.Feeds;
				NotifyCollectionChangedEventArgs eventArgs = null;

				notifier.CollectionChanged += (sender, e) => { eventArgs = e; };

				// Act
				subscriptions.Add(testUrl);

				// Assert
				eventArgs.Should().NotBeNull();
			}

			[Fact]
			public void Should_notify_when_feed_was_removed()
			{
				// Arrange
				subscriptions.Add(testUrl);
				var feed = subscriptions.Feeds.First();

				var notifier = (INotifyCollectionChanged)subscriptions.Feeds;
				NotifyCollectionChangedEventArgs eventArgs = null;
				notifier.CollectionChanged += (sender, e) => { eventArgs = e; };

				// Act
				subscriptions.Remove(feed);

				// Assert
				eventArgs.Should().NotBeNull();
			}
		}

		public class AddMethod : Test
		{
			[Fact]
			public void Should_create_feed_for_specified_URL_and_add_it_to_the_list_of_feeds()
			{
				// Arrange

				// Act
				subscriptions.Add(testUrl);

				// Assert
				subscriptions.Feeds.Should().Contain(feed => feed.Url.Equals(testUrl, StringComparison.Ordinal));
			}

			[Fact]
			public void Should_download_feed_contents()
			{
				// Arrange
				var wasGetFeedCalled = false;
				feedDownloader.GetFeed = url =>
				{
					wasGetFeedCalled = true;
					return new SyndicationFeed("Title", "Description", null);
				};

				// Act
				subscriptions.Add(testUrl);

				// Assert
				wasGetFeedCalled.Should().BeTrue();
			}

			[Fact]
			public void Should_throw_when_URL_is_already_subscribed()
			{
				// Arrange
				subscriptions.Add(testUrl);

				// Act
				Action act = () => { subscriptions.Add(testUrl); };

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
				subscriptions.Add(testUrl);

				var feed = subscriptions.Feeds.First();

				// Act
				subscriptions.Remove(feed);

				// Assert
				subscriptions.Feeds.Should().NotContain(feed);
			}
		}
	}
}
