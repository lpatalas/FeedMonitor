using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FeedMonitor.Models;
using FeedMonitor.Services;
using FeedMonitor.UnitTests.Fakes;
using FeedMonitor.ViewModels;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.ViewModels
{
	public class SubscriptionsViewModelClass
	{
		public const string testUrl = @"http://blogs.msdn.com/b/ericlippert/rss.aspx";

		public abstract class TestBase
		{
			protected readonly IFeedDownloader feedDownloader;
			protected readonly IFeedFactory feedFactory;
			protected readonly IMessageBoxService messageBoxService;
			protected readonly ISubscriptions subscriptions;
			protected readonly SubscriptionsViewModel viewModel;

			public TestBase()
			{
				feedDownloader = new FakeFeedDownloader();
				messageBoxService = A.Fake<IMessageBoxService>();
				feedFactory = new FakeFeedFactory(feedDownloader);
				subscriptions = new Subscriptions(feedFactory);
				viewModel = new SubscriptionsViewModel(feedFactory, messageBoxService, subscriptions);
			}
		}

		public abstract class TestWithSingleSubscription : TestBase
		{
			public TestWithSingleSubscription()
			{
				viewModel.Subscribe(testUrl);
			}
		}

		public class FeedsProperty : TestBase
		{
			[Fact]
			public void Should_return_list_of_subscribed_feeds()
			{
				// Arrange
				var firstUrl = @"http://blogs.msdn.com/b/ericlippert/rss.aspx";
				var secondUrl = @"http://feeds.feedburner.com/ScottHanselman";

				// Act
				subscriptions.Add(firstUrl);
				subscriptions.Add(secondUrl);

				// Assert
				viewModel.Feeds.Should().Contain(subscriptions.Feeds);
			}
		}

		public class SubscribeMethod : TestBase
		{
			[Fact]
			public void Should_add_URL_to_subscriptions()
			{
				// Arrange
				var sourceUrl = testUrl;

				// Act
				viewModel.Subscribe(sourceUrl);

				// Assert
				subscriptions.Feeds
					.Should().HaveCount(1)
					.And.Contain(feed => feed.Url.Equals(sourceUrl, StringComparison.Ordinal));
			}

			[Fact]
			public void Should_do_nothing_if_URL_is_empty()
			{
				// Arrange

				// Act
				viewModel.Subscribe(null);
				viewModel.Subscribe(string.Empty);

				// Assert
				subscriptions.Feeds.Should().BeEmpty();
			}

			[Fact]
			public void Should_not_try_to_subscribe_to_the_same_URL_twice()
			{
				// Arrange
				var sourceUrl = testUrl;

				// Act
				viewModel.Subscribe(sourceUrl);
				viewModel.Subscribe(sourceUrl);

				// Assert
				subscriptions.Feeds.Should().HaveCount(1);
			}
		}

		public class UnsubscribeMethod : TestWithSingleSubscription
		{
			[Fact]
			public void Should_display_confirmation_dialog_box()
			{
				// Arrange
				var feed = viewModel.Feeds.First();

				// Act
				viewModel.Unsubscribe(feed);

				// Assert
				A.CallTo(() => messageBoxService.ShowYesNoDialog(A<string>.Ignored, A<string>.Ignored))
					.MustHaveHappened();
			}

			[Fact]
			public void Should_not_remove_subscription_if_user_does_not_confirm_removal()
			{
				// Arrange
				A.CallTo(() => messageBoxService.ShowYesNoDialog(A<string>.Ignored, A<string>.Ignored))
					.Returns(false);

				var feed = viewModel.Feeds.First();

				// Act
				viewModel.Unsubscribe(feed);

				// Assert
				subscriptions.Feeds.Should().Contain(feed);
			}

			[Fact]
			public void Should_remove_specified_feed_from_list_when_user_confirms_removal()
			{
				// Arrange
				A.CallTo(() => messageBoxService.ShowYesNoDialog(A<string>.Ignored, A<string>.Ignored))
					.Returns(true);

				var feed = viewModel.Feeds.First();

				// Act
				viewModel.Unsubscribe(feed);

				// Assert
				subscriptions.Feeds.Should().NotContain(feed);
			}
		}
	}
}
