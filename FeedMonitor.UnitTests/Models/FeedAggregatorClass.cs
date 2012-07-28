using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FeedMonitor.Models;
using FeedMonitor.UnitTests.Fakes;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.Models
{
	public class FeedAggregatorClass
	{
		public class AddSourceMethod
		{
			[Fact]
			public void Should_include_all_items_from_added_feed_in_AllItems_property()
			{
				// Arrange
				var aggregator = new FeedAggregator();
				var items = GenerateFakeItems(5);
				var source = new FakeFeedSource(items);

				// Act
				aggregator.AddSource(source);

				// Assert
				aggregator.AllItems.Should().Contain(items);
			}

			[Fact]
			public void Should_merge_all_items_from_multiple_sources_added_to_aggregator()
			{
				// Arrange
				var aggregator = new FeedAggregator();
				var firstItems = GenerateFakeItems(5);
				var firstSource = new FakeFeedSource(firstItems);
				var secondItems = GenerateFakeItems(5);
				var secondSource = new FakeFeedSource(secondItems);

				// Act
				aggregator.AddSource(firstSource);
				aggregator.AddSource(secondSource);

				// Assert
				aggregator.AllItems.Should().Contain(firstItems.Concat(secondItems));
			}

			private static IEnumerable<FeedItem> GenerateFakeItems(int count)
			{
				var items = new List<FeedItem>();
				for (int i = 0; i < count; i++)
				{
					items.Add(new FeedItem { Title = "Item " + i });
				}

				return items;
			}
		}

		public class AllItemsProperty
		{
			[Fact]
			public void Should_be_sorted_by_PublishDate()
			{
				// Arrange
				var aggregator = new FeedAggregator();
				var items = new[]
				{
					new FeedItem { PublishDate = new DateTime(2010, 10, 10, 12, 50, 0) },
					new FeedItem { PublishDate = new DateTime(2010, 10, 10, 11, 0, 0) },
					new FeedItem { PublishDate = new DateTime(2012, 1, 1, 0, 0, 0) }
				};
				var source = new FakeFeedSource(items);

				// Act
				aggregator.AddSource(source);

				// Assert
				var expectedResult = items.OrderBy(item => item.PublishDate).ToList();

				aggregator.AllItems.Should().ContainInOrder(expectedResult);
			}
		}

		public class SourcesProperty
		{
			[Fact]
			public void Should_return_all_sources_added_to_aggregator()
			{
				// Arrange
				var sources = new[]
				{
					new FakeFeedSource(),
					new FakeFeedSource()
				};

				var aggregator = new FeedAggregator();

				// Act
				foreach (var source in sources)
					aggregator.AddSource(source);

				// Assert
				aggregator.Sources.Should().Contain(sources);
			}
		}
	}
}
