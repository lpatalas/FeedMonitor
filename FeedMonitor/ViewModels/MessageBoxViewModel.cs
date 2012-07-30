using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.ViewModels
{
	public class MessageBoxViewModel
	{
		private readonly string message;
		private readonly string title;

		public string Message
		{
			get { return message; }
		}

		public string Title
		{
			get { return title; }
		}

		public MessageBoxViewModel(string message, string title)
		{
			this.message = message;
			this.title = title;
		}
	}
}
