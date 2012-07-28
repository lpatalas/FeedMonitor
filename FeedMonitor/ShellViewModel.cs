namespace FeedMonitor {
    using System.ComponentModel.Composition;
using FeedMonitor.ViewModels;

	[Export(typeof(IShell))]
	public class ShellViewModel : IShell
	{
		public ReaderViewModel CurrentView { get; private set; }

		public ShellViewModel()
		{
			this.CurrentView = new ReaderViewModel();
		}
	}
}
