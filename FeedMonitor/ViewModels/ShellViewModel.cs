namespace FeedMonitor.ViewModels {
	using System.ComponentModel.Composition;
	using System.Windows;
	using System.Windows.Controls;

	[Export(typeof(IShell))]
	public class ShellViewModel : IShell
	{
		public object CurrentViewModel { get; private set; }

		public ShellViewModel()
		{
			this.CurrentViewModel = new SubscriptionsViewModel();
		}
	}
}
