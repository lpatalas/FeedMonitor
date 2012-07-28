using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FeedMonitor.Models;
using FeedMonitor.UnitTests.Fakes;
using FeedMonitor.ViewModels;
using Xunit;

namespace FeedMonitor.UnitTests.ViewModels
{
	public class ReaderViewModelClass
	{
		private const string testUrl = @"http://blogs.msdn.com/b/ericlippert/rss.aspx";

		public abstract class TestBase
		{
			protected IFeedAggregator aggregator;
			protected IFeedSourceFactory feedSourceFactory;
			protected ReaderViewModel readerViewModel;

			public TestBase()
			{
				aggregator = A.Fake<IFeedAggregator>();
				feedSourceFactory = A.Fake<FakeFeedSourceFactory>();
				readerViewModel = new ReaderViewModel(aggregator, feedSourceFactory);
			}
		}

		public class AddFeedMethod : TestBase
		{
			[Fact]
			public void Should_create_feed_source_based_on_given_URL()
			{
				// Arrange
				var sourceUrl = testUrl;
				
				// Act
				readerViewModel.AddFeed(sourceUrl);

				// Assert
				A.CallTo(() => feedSourceFactory.CreateFromUrl(sourceUrl))
					.MustHaveHappened();
			}

			[Fact]
			public void Should_add_feed_source_to_aggregator()
			{
				// Arrange
				var sourceUrl = testUrl;

				A.CallTo(() => feedSourceFactory.CreateFromUrl(A<string>.Ignored))
					.CallsBaseMethod();

				// Act
				readerViewModel.AddFeed(testUrl);

				// Assert
				A.CallTo(() => aggregator.AddSource(
					A<IFeedSource>.That.Matches(source => ((FakeFeedSource)source).SourceUrl == sourceUrl)))
					.MustHaveHappened(Repeated.Exactly.Once);
			}
		}
	}
}
