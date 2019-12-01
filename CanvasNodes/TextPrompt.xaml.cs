using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CanvasNodes
{
	public partial class TextPrompt : Window
	{
		public TextPrompt()
		{
			InitializeComponent();
		}

		public string ResponseText
		{
			get => ResponseTextBox.Text;
			set => ResponseTextBox.Text = value;
		}

		private void OkButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
