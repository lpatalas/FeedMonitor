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
	public class FeedClass
	{
		public abstract class Test
		{
			protected readonly FakeFeedProvider feedProvider;
			protected readonly Feed feed;
			protected const string feedUrl = "http://website.org/rss";

			public Test()
			{
				feedProvider = new FakeFeedProvider();
				feed = new Feed(feedProvider, feedUrl);
			}
		}
			 
		public class UpdateMethod : Test
		{
			[Fact]
			public void Should_set_Title_property_to_title_found_in_downloaded_feed()
			{
				// Arrange

				// Act
				feed.Update();

				// Assert
				feed.Title.Should().Be(FakeFeedProvider.FeedTitle);
			}

			[Fact]
			public void Should_use_IFeedProvider_to_get_feed_from_specfied_URL()
			{
				// Arrange
				string requestedUrl = null;
				feedProvider.GetFeed = url =>
				{
					requestedUrl = url;
					return feedProvider.GetFeedDefaultImpl(url);
				};

				// Act
				feed.Update();

				// Assert
				requestedUrl.Should().Be(feedUrl);
			}
		}
	}
}
