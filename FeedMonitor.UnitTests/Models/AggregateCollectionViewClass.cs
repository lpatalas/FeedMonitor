using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		public abstract class Test
		{
			protected readonly AggregateCollectionView<int> testedView = new AggregateCollectionView<int>();
		}

		public abstract class FilledTest : Test
		{
			protected readonly IEnumerable<int> firstItems = new[] { 1, 2, 3 };
			protected readonly IEnumerable<int> secondItems = new[] { 4, 5, 6 };

			public FilledTest()
			{
				testedView.AddCollection(firstItems);
				testedView.AddCollection(secondItems);
			}
		}

		public class AddCollectionMethod : Test
		{
			[Fact]
			public void Should_throw_when_the_same_collection_is_added_second_time()
			{
				// Arrange
				var input = new[] { 1, 2, 3 };

				// Act
				testedView.AddCollection(input);
				Action act = () => { testedView.AddCollection(input); };

				// Assert
				act.ShouldThrow<InvalidOperationException>();
			}

			[Fact]
			public void Should_include_all_items_from_added_collection_in_the_view()
			{
				// Arrange
				var input = new[] { 1, 2, 3 };

				// Act
				testedView.AddCollection(input);

				// Assert
				testedView.Should().Contain(input);
			}

			[Fact]
			public void Should_raise_CollectionChanged_event_with_Add_action()
			{
				// Arrange
				var input = new[] { 1, 2, 3 };
				NotifyCollectionChangedEventArgs raisedEventArgs = null;

				testedView.CollectionChanged += (sender, e) => { raisedEventArgs = e; };

				// Act
				testedView.AddCollection(input);

				// Assert
				raisedEventArgs.Should().NotBeNull();
				raisedEventArgs.Action.Should().Be(NotifyCollectionChangedAction.Add);
			}

			[Fact]
			public void Should_union_added_items_with_any_items_already_in_the_collection()
			{
				// Arrange
				var firstItems = new[] { 1, 2, 3 };
				var secondItems = new[] { 4, 5, 6 };

				// Act
				testedView.AddCollection(firstItems);
				testedView.AddCollection(secondItems);

				// Assert
				var expectedResult = firstItems.Union(secondItems);
				testedView.Should().Contain(expectedResult);
			}
		}

		public class CollectionChangedEvent : Test
		{
			protected readonly ObservableCollection<int> items = new ObservableCollection<int>(new[] { 1, 2, 3 });
			protected NotifyCollectionChangedEventArgs raisedEventArgs;

			public CollectionChangedEvent()
			{
				testedView.AddCollection(items);
				testedView.CollectionChanged += (sender, e) => { raisedEventArgs = e; };
			}

			[Fact]
			public void Should_be_raised_when_collection_added_to_view_is_modified()
			{
				// Arrange

				// Act
				items.Add(5);

				// Assert
				raisedEventArgs.Should().NotBeNull();
			}

			[Fact]
			public void Should_not_be_raised_when_collection_is_changed_after_removing_from_view()
			{
				// Arrange
				testedView.RemoveCollection(items);
				raisedEventArgs = null;

				// Act
				items.Add(5);

				// Assert
				raisedEventArgs.Should().BeNull();
			}
		}

		public class RemoveCollectionMethod : FilledTest
		{
			[Fact]
			public void Should_not_raise_CollectionChanged_event_if_specified_collection_is_not_included_in_view()
			{
				// Arrange
				var itemsToRemove = new[] { 9, 8, 7 };
				NotifyCollectionChangedEventArgs raisedEventArgs = null;
				testedView.CollectionChanged += (sender, e) => { raisedEventArgs = e; };

				// Act
				testedView.RemoveCollection(itemsToRemove);

				// Assert
				raisedEventArgs.Should().BeNull();
			}

			[Fact]
			public void Should_raise_CollectionChanged_event_with_Remove_action()
			{
				// Arrange
				NotifyCollectionChangedEventArgs raisedEventArgs = null;
				testedView.CollectionChanged += (sender, e) => { raisedEventArgs = e; };

				// Act
				testedView.RemoveCollection(firstItems);

				// Assert
				raisedEventArgs.Should().NotBeNull();
				raisedEventArgs.Action.Should().Be(NotifyCollectionChangedAction.Remove);
			}

			[Fact]
			public void Should_remove_all_items_contained_in_collection_from_view()
			{
				// Arrange

				// Act
				testedView.RemoveCollection(firstItems);

				// Assert
				testedView.Should().BeEquivalentTo(secondItems);
			}
		}
	}
}
