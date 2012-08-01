using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedMonitor.Models;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.Models
{
	public class FeedItemClass
	{
		[Fact]
		public void Should_check_equality_by_comparing_Id()
		{
			// Arrange
			var firstItem = new FeedItem("1", DateTime.Now, "First");
			var secondItem = new FeedItem("2", firstItem.PublishDate, firstItem.Title);
			var thirdItem = new FeedItem("2", DateTime.Now.AddDays(1), "Third");

			// Act
			var firstEqualsSecond = firstItem.Equals(secondItem);
			var secondEqualsThird = secondItem.Equals(thirdItem);

			// Assert
			firstEqualsSecond.Should().BeFalse();
			secondEqualsThird.Should().BeTrue();
		}
	}
}
