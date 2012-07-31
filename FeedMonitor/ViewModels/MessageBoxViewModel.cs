using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace FeedMonitor.ViewModels
{
	public class MessageBoxViewModel : ViewAware
	{
		private readonly IList<MessageBoxButtonMapping> buttons;
		private readonly string message;
		private MessageBoxResult result;
		private readonly string title;

		public IList<MessageBoxButtonMapping> Buttons
		{
			get { return buttons; }
		}

		public string Message
		{
			get { return message; }
		}

		public MessageBoxResult Result
		{
			get { return result; }
		}

		public string Title
		{
			get { return title; }
		}

		public MessageBoxViewModel(string message, string title, params MessageBoxButtonMapping[] buttons)
		{
			Contract.Requires(!string.IsNullOrEmpty(message));
			Contract.Requires(!string.IsNullOrEmpty(title));
			Contract.Requires(buttons.Length > 0);

			this.buttons = new ReadOnlyCollection<MessageBoxButtonMapping>(buttons);
			this.message = message;
			this.title = title;
		}

		public void Commit(MessageBoxButtonMapping buttonMapping)
		{
			this.result = buttonMapping.Result;

			var window = GetView() as Window;
			if (window != null)
				window.Close();
		}
	}
}
