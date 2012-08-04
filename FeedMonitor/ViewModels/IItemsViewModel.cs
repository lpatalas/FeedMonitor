using System;
using System.ComponentModel;
using Caliburn.Micro;

namespace FeedMonitor.ViewModels
{
	public interface IItemsViewModel : IHaveDisplayName
	{
		ICollectionView FeedItems { get; }
	}
}
