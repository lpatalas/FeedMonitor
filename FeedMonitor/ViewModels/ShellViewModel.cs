namespace FeedMonitor.ViewModels {
	using System.ComponentModel.Composition;
	using System.Windows;
	using System.Windows.Controls;

	[Export(typeof(IShell))]
	public class ShellViewModel : IShell
	{
		public FrameworkElement CurrentView { get; private set; }

		public ShellViewModel()
		{
			this.CurrentView = new TextBlock { Text = "Empty" };
		}
	}
}
