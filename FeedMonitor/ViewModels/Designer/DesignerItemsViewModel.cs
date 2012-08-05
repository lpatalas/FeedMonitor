using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using FeedMonitor.Models;

namespace FeedMonitor.ViewModels.Designer
{
	public class DesignerItemsViewModel : IItemsViewModel
	{
		public string DisplayName { get; set; }
		public ICollectionView FeedItems { get; private set; }

		public DesignerItemsViewModel()
		{
			var items = new[]
			{
				new FeedItem("Lorem ipsum dolor sit amet", "1", DateTime.Now.AddDays(-20), "Lorem ipsum"),
				new FeedItem("Lorem ipsum dolor sit amet", "2", DateTime.Now.AddDays(-1), "Dolor sit amet"),
				new FeedItem("Lorem ipsum dolor sit amet", "3", DateTime.Now.AddDays(-3), "Sed vel facilisis magna"),
				new FeedItem("Lorem ipsum dolor sit amet", "4", DateTime.Now.AddHours(-23), "Suspendisse nec aliquet neque")
			};

			FeedItems = CollectionViewSource.GetDefaultView(items);
		}
	}
}
