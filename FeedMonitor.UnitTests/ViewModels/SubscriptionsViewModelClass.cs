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
			protected IFeedProvider feedProvider = new FakeFeedProvider();
			protected IMessageBoxService messageBoxService;
			protected ISubscriptionFactory subscriptionFactory;
			protected SubscriptionsViewModel viewModel;

			public TestBase()
			{
				feedProvider = new FakeFeedProvider();
				messageBoxService = A.Fake<IMessageBoxService>();
				subscriptionFactory = new FakeSubscriptionFactory(feedProvider);
				viewModel = new SubscriptionsViewModel(messageBoxService, subscriptionFactory);
			}
		}

		public abstract class TestWithSingleSubscription : TestBase
		{
			public TestWithSingleSubscription()
			{
				viewModel.AddSubscription(testUrl);
			}
		}

		public class AddSubscriptionMethod : TestBase
		{
			[Fact]
			public void Should_add_URL_to_list_of_subscriptions()
			{
				// Arrange
				var sourceUrl = testUrl;

				// Act
				viewModel.AddSubscription(sourceUrl);

				// Assert
				viewModel.Subscriptions
					.Should().Contain(item => item.Url.Equals(sourceUrl, StringComparison.Ordinal));
			}

			[Fact]
			public void Should_do_nothing_if_specified_URL_is_already_subscribed_to()
			{
				// Arrange
				var sourceUrl = testUrl;

				// Act
				viewModel.AddSubscription(sourceUrl);
				viewModel.AddSubscription(sourceUrl);

				// Assert
				viewModel.Subscriptions.Should().HaveCount(1);
			}
		}

		public class RemoveSubscriptionMethod : TestWithSingleSubscription
		{
			[Fact]
			public void Should_display_confirmation_dialog_box()
			{
				// Arrange
				var subscription = viewModel.Subscriptions.First();

				// Act
				viewModel.RemoveSubscription(subscription);

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

				var subscription = viewModel.Subscriptions.First();

				// Act
				viewModel.RemoveSubscription(subscription);

				// Assert
				viewModel.Subscriptions.Should().Contain(subscription);
			}

			[Fact]
			public void Should_remove_specified_subscription_from_list_when_user_confirms_removal()
			{
				// Arrange
				A.CallTo(() => messageBoxService.ShowYesNoDialog(A<string>.Ignored, A<string>.Ignored))
					.Returns(true);

				var subscription = viewModel.Subscriptions.First();

				// Act
				viewModel.RemoveSubscription(subscription);

				// Assert
				viewModel.Subscriptions.Should().NotContain(subscription);
			}
		}
	}
}
