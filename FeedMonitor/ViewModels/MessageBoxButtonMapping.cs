using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FeedMonitor.ViewModels
{
	public class MessageBoxButtonMapping
	{
		private readonly MessageBoxResult result;
		private readonly string text;

		public MessageBoxResult Result
		{
			get { return result; }
		}

		public string Text
		{
			get { return text; }
		}

		public MessageBoxButtonMapping(string text, MessageBoxResult result)
		{
			Contract.Requires(!string.IsNullOrEmpty(text));

			this.result = result;
			this.text = text;
		}
	}
}
