using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeedMonitor.Models
{
	public class FeedItem
	{
		public DateTime PublishDate { get; set; }

		public string Summary { get; set; }

		public string Title { get; set; }

		public static FeedItem FromSyndicationItem(SyndicationItem sourceItem)
		{
			return new FeedItem
			{
				PublishDate = sourceItem.PublishDate.LocalDateTime,
				Summary = ExtractSummary(sourceItem.Summary.Text),
				Title = sourceItem.Title.Text
			};
		}

		public FeedItem()
		{
		}

		private static string AddEllipsis(string input)
		{
			string result;

			var lastSpaceIndex = input.LastIndexOfAny(new char[] { ' ', '\t' });
			if (lastSpaceIndex > 0)
				result = input.Substring(0, lastSpaceIndex);
			else
				result = input;

			return result.Trim() + "...";
		}

		private static string ExtractSummary(string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			var summary = StripTags(input);
			if (summary.Length > 200)
				summary = summary.Substring(0, 200);

			return AddEllipsis(summary);
		}

		private static string StripTags(string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			return Regex.Replace(input, @"<[^>]*>", string.Empty);
		}
	}
}
