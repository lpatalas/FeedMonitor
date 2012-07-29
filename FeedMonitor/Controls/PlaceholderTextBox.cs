using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FeedMonitor.Controls
{
	public class PlaceholderTextBox : TextBox
	{
		public Visibility PlaceholderVisibility
		{
			get { return (Visibility)GetValue(PlaceholderVisibilityProperty); }
			private set { SetValue(PlaceholderVisibilityProperty, value); }
		}

		public static readonly DependencyProperty PlaceholderVisibilityProperty =
			DependencyProperty.Register("PlaceholderVisibility", typeof(Visibility), typeof(PlaceholderTextBox), new PropertyMetadata(Visibility.Visible));

		public string PlaceholderText
		{
			get { return (string)GetValue(PlaceholderTextProperty); }
			set { SetValue(PlaceholderTextProperty, value); }
		}

		public static readonly DependencyProperty PlaceholderTextProperty =
			DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);
			
			if (e.Property.Name.Equals("Text") || e.Property.Name.Equals("IsFocused"))
			{
				if (Text.Length > 0 || IsFocused)
					PlaceholderVisibility = Visibility.Hidden;
				else
					PlaceholderVisibility = Visibility.Visible;
			}
		}
	}
}
