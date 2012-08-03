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

namespace FeedMonitor.UnitTests.Services
{
	public class FeedAggregatorClass
	{
		public abstract class Test
		{
			protected readonly FakeFeedDownloader feedDownloader;
			protected readonly FakeFeedFactory feedFactory;
			protected readonly Subscriptions subscriptions;
			protected readonly FeedAggregator feedAggregator;

			protected Test()
			{
				feedDownloader = new FakeFeedDownloader();
				feedFactory = new FakeFeedFactory(feedDownloader);
				subscriptions = new Subscriptions(feedFactory);
				feedAggregator = new FeedAggregator(subscriptions);
			}

			protected void AddFeedToSubscriptions(string url, params FeedItem[] items)
			{
				feedDownloader.FeedUrl = url;
				feedDownloader.FeedItems.Clear();
				foreach (var item in items)
					feedDownloader.FeedItems.Add(item);

				subscriptions.Add(url);
				subscriptions.Feeds[subscriptions.Feeds.Count - 1].Update();
			}
		}

		public class AllItemsProperty : Test
		{
			[Fact]
			public void Should_be_ordered_in_descending_order_by_publish_date()
			{
				// Arrange
				var firstItems = new[]
				{
					new FeedItem("1", DateTime.Now, "1-1"),
					new FeedItem("2", DateTime.Now.AddDays(1), "1-2")
				};

				var secondItems = new[]
				{
					new FeedItem("3", DateTime.Now.AddDays(-5), "2-1"),
					new FeedItem("4", DateTime.Now.AddDays(3), "2-2")
				};

				// Act
				AddFeedToSubscriptions("http://www.test.com/1", firstItems);
				AddFeedToSubscriptions("http://www.test.com/2", secondItems);

				// Assert
				var expectedItems = firstItems.Concat(secondItems)
					.OrderByDescending(item => item.PublishDate)
					.ToList();
				feedAggregator.AllItems.Should().ContainInOrder(expectedItems);
			}

			[Fact]
			public void Should_contain_all_items_from_all_subscribed_feeds_passed_to_constructor()
			{
				// Arrange
				var firstItems = new[]
				{
					new FeedItem("1", DateTime.Now, "1-1"),
					new FeedItem("2", DateTime.Now.AddDays(1), "1-2")
				};

				var secondItems = new[]
				{
					new FeedItem("3", DateTime.Now.AddDays(-5), "2-1"),
					new FeedItem("4", DateTime.Now.AddDays(3), "2-2")
				};

				AddFeedToSubscriptions("http://www.test.com/1", firstItems);
				AddFeedToSubscriptions("http://www.test.com/2", secondItems);

				// Act
				var newFeedAggregator = new FeedAggregator(subscriptions);

				// Assert
				var expectedItems = firstItems.Concat(secondItems).ToList();
				newFeedAggregator.AllItems.Should().BeEquivalentTo(expectedItems);
			}

			[Fact]
			public void Should_include_all_items_when_feed_is_added_to_subscriptions()
			{
				// Arrange
				var firstItems = new[]
				{
					new FeedItem("1", DateTime.Now, "1-1"),
					new FeedItem("2", DateTime.Now.AddDays(1), "1-2")
				};

				// Act
				AddFeedToSubscriptions("http://www.test.com/1", firstItems);

				// Assert
				feedAggregator.AllItems.Should().BeEquivalentTo(firstItems);
			}

			[Fact]
			public void Should_remove_items_when_feed_is_unsubscribed()
			{
				// Arrange
				var firstItems = new[]
				{
					new FeedItem("1", DateTime.Now, "1-1"),
					new FeedItem("2", DateTime.Now.AddDays(1), "1-2")
				};

				var secondItems = new[]
				{
					new FeedItem("3", DateTime.Now.AddDays(-5), "2-1"),
					new FeedItem("4", DateTime.Now.AddDays(3), "2-2")
				};

				AddFeedToSubscriptions("http://www.test.com/1", firstItems);
				AddFeedToSubscriptions("http://www.test.com/2", secondItems);

				// Act
				subscriptions.Remove(subscriptions.Feeds[1]);

				// Assert
				feedAggregator.AllItems.Should().BeEquivalentTo(firstItems);
			}
		}
	}
}
