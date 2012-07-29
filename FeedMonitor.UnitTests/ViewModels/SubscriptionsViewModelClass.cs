﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			protected SubscriptionsViewModel viewModel;

			public TestBase()
			{
				viewModel = new SubscriptionsViewModel();
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
		}

		public class RemoveSubscriptionMethod : TestBase
		{
			[Fact]
			public void Should_remove_specified_subscription_from_list()
			{
				// Arrange
				viewModel.AddSubscription(testUrl);
				var subscription = viewModel.Subscriptions
					.First(item => item.Url.Equals(testUrl, StringComparison.Ordinal));

				// Act
				viewModel.RemoveSubscription(subscription);

				// Assert
				viewModel.Subscriptions.Should().NotContain(subscription);
			}
		}
	}
}
