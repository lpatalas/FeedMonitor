using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using FeedMonitor.ViewModels;

namespace FeedMonitor.UnitTests.Fakes
{
	public class FakeWindowManager : IWindowManager
	{
		private MessageBoxResult? overridenDialogResult;

		public object DialogRootModel { get; private set; }
		public bool WasShowDialogCalled { get; private set; }

		public void OverrideDialogResult(MessageBoxResult result)
		{
			this.overridenDialogResult = result;
		}

		public bool? ShowDialog(object rootModel, object context = null, IDictionary<string, object> settings = null)
		{
			this.DialogRootModel = rootModel;
			this.WasShowDialogCalled = true;

			var messageBoxViewModel = rootModel as MessageBoxViewModel;
			if (messageBoxViewModel != null && overridenDialogResult.HasValue)
			{
				var mapping = messageBoxViewModel.Buttons.First(btn => btn.Result == overridenDialogResult);
				messageBoxViewModel.Commit(mapping);
			}

			return true;
		}

		public void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null)
		{
		}

		public void ShowWindow(object rootModel, object context = null, IDictionary<string, object> settings = null)
		{
		}
	}
}
