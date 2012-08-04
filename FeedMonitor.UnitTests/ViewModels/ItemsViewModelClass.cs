using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FeedMonitor.Services;
using FeedMonitor.UnitTests.Fakes;
using FeedMonitor.ViewModels;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.ViewModels
{
	public class ItemsViewModelClass
	{
		public abstract class Test
		{
			protected readonly FakeFeedDownloader feedDownloader;
			protected readonly FakeFeedFactory feedFactory;
			protected readonly ISubscriptions subscriptions;
			protected readonly ItemsViewModel viewModel;

			protected Test()
			{
				feedDownloader = new FakeFeedDownloader();
				feedFactory = new FakeFeedFactory(feedDownloader);
				subscriptions = new Subscriptions(feedFactory);
				viewModel = new ItemsViewModel(subscriptions);
			}

			protected void AddFeed(string url, params FeedItem[] items)
			{
				feedDownloader.FeedItems.Clear();
				foreach (var item in items)
					feedDownloader.FeedItems.Add(item);

				feedDownloader.FeedUrl = url;

				subscriptions.Add(url);
				subscriptions.Feeds[subscriptions.Feeds.Count - 1].Update();
			}

			protected readonly FeedItem[] firstTestItems =
			{
				new FeedItem("11", DateTime.Now, "1-1"),
				new FeedItem("12", DateTime.Now.AddDays(1), "1-2")
			};

			protected readonly FeedItem[] secondTestItems =
			{
				new FeedItem("21", DateTime.Now.AddDays(-10), "2-1"),
				new FeedItem("22", DateTime.Now.AddDays(20), "2-2")
			};
		}

		public class FeedItemsProperty : Test
		{
			[Fact]
			public void Should_include_new_items_in_FeedItems_property_when_new_subscription_is_added()
			{
				// Arrange
				var addedItems = firstTestItems;
				
				// Act
				AddFeed("http://www.test.com/1", firstTestItems);
				AddFeed("http://www.test.com/2", secondTestItems);

				// Assert
				viewModel.FeedItems
					.Cast<FeedItem>()
					.Should().BeEquivalentTo(firstTestItems.Concat(secondTestItems));
			}

			[Fact]
			public void Should_remove_feed_items_when_containing_feed_is_unsubscribed()
			{
				// Arrange
				var removedItems = firstTestItems;

				// Act
				AddFeed("http://www.test.com/1", firstTestItems);
				AddFeed("http://www.test.com/2", secondTestItems);

				subscriptions.Remove(subscriptions.Feeds[1]);

				// Assert
				viewModel.FeedItems
					.Cast<FeedItem>()
					.Should().BeEquivalentTo(firstTestItems);
			}
		}
	}
}
