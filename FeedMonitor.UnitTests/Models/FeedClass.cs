﻿using System;
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
			protected readonly FakeFeedDownloader feedDownloader;
			protected readonly Feed feed;
			protected const string feedUrl = "http://website.org/rss";

			public Test()
			{
				feedDownloader = new FakeFeedDownloader();
				feed = new Feed(feedDownloader, feedUrl);
			}
		}
			 
		public class UpdateMethod : Test
		{
			[Fact]
			public void Should_fill_Items_collection_with_all_items_downloaded_from_feed()
			{
				// Arrange
				feedDownloader.FeedItems.Add(new FeedItem("1", new DateTime(2012, 2, 1, 11, 12, 0), "First"));
				feedDownloader.FeedItems.Add(new FeedItem("2", new DateTime(2012, 1, 23, 15, 34, 18), "Second"));

				// Act
				feed.Update();

				// Assert
				feed.Items.Should().Contain(feedDownloader.FeedItems);
			}

			[Fact]
			public void Should_set_Title_property_to_title_found_in_downloaded_feed()
			{
				// Arrange

				// Act
				feed.Update();

				// Assert
				feed.Title.Should().Be(feedDownloader.FeedTitle);
			}

			[Fact]
			public void Should_use_IFeedDownloader_to_get_feed_from_specfied_URL()
			{
				// Arrange
				string requestedUrl = null;
				feedDownloader.GetFeed = url =>
				{
					requestedUrl = url;
					return feedDownloader.GetFeedDefaultImpl(url);
				};

				// Act
				feed.Update();

				// Assert
				requestedUrl.Should().Be(feedUrl);
			}
		}
	}
}
