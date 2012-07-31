using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMonitor.Services
{
	public interface IMessageBoxService
	{
		bool ShowYesNoDialog(string message, string title);
	}
}
