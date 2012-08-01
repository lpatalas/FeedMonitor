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
			protected IFeedDownloader feedDownloader;
			protected IFeedFactory feedFactory;
			protected IMessageBoxService messageBoxService;
			protected SubscriptionsViewModel viewModel;

			public TestBase()
			{
				feedDownloader = new FakeFeedDownloader();
				messageBoxService = A.Fake<IMessageBoxService>();
				feedFactory = new FakeFeedFactory(feedDownloader);
				viewModel = new SubscriptionsViewModel(feedFactory, messageBoxService);
			}
		}

		public abstract class TestWithSingleSubscription : TestBase
		{
			public TestWithSingleSubscription()
			{
				viewModel.Subscribe(testUrl);
			}
		}

		public class SubscribeMethod : TestBase
		{
			[Fact]
			public void Should_add_URL_to_list_of_feeds()
			{
				// Arrange
				var sourceUrl = testUrl;

				// Act
				viewModel.Subscribe(sourceUrl);

				// Assert
				viewModel.Feeds.Should().Contain(
					item => item.Url.Equals(sourceUrl, StringComparison.Ordinal));
			}

			[Fact]
			public void Should_do_nothing_if_specified_URL_is_already_subscribed_to()
			{
				// Arrange
				var sourceUrl = testUrl;

				// Act
				viewModel.Subscribe(sourceUrl);
				viewModel.Subscribe(sourceUrl);

				// Assert
				viewModel.Feeds.Should().HaveCount(1);
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
				viewModel.Feeds.Should().Contain(feed);
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
				viewModel.Feeds.Should().NotContain(feed);
			}
		}
	}
}
