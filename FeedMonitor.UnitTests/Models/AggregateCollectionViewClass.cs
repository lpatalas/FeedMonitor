using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.Models
{
	public class AggregateCollectionViewClass
	{
		public class AddCollectionMethod
		{
			[Fact]
			public void Should_include_all_items_from_added_collection_in_the_view()
			{
				// Arrange
				var aggregateCollectionView = new AggregateCollectionView<int>();
				var input = new[] { 1, 2, 3 };

				// Act
				aggregateCollectionView.AddCollection(input);

				// Assert
				aggregateCollectionView.Should().Contain(input);
			}

			[Fact]
			public void Should_raise_CollectionChanged_event_with_Add_action()
			{
				// Arrange
				var aggregateCollectionView = new AggregateCollectionView<int>();
				var input = new[] { 1, 2, 3 };
				NotifyCollectionChangedEventArgs raisedEventArgs = null;

				aggregateCollectionView.CollectionChanged += (sender, e) => { raisedEventArgs = e; };

				// Act
				aggregateCollectionView.AddCollection(input);

				// Assert
				raisedEventArgs.Should().NotBeNull();
				raisedEventArgs.Action.Should().Be(NotifyCollectionChangedAction.Add);
			}

			[Fact]
			public void Should_union_added_items_with_any_items_already_in_the_collection()
			{
				// Arrange
				var aggregateCollectionView = new AggregateCollectionView<int>();
				var firstItems = new[] { 1, 2, 3 };
				var secondItems = new[] { 4, 5, 6 };

				// Act
				aggregateCollectionView.AddCollection(firstItems);
				aggregateCollectionView.AddCollection(secondItems);

				// Assert
				var expectedResult = firstItems.Union(secondItems);
				aggregateCollectionView.Should().Contain(expectedResult);
			}
		}
	}
}
