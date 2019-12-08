using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CanvasNodes.DrawNodes;
using CanvasNodes.DrawNodes.Actions;
using CanvasNodes.DrawNodes.Conditions;
using CanvasNodes.DrawNodes.LogicGates;
using CanvasNodes.DrawNodes.Triggers;

namespace CanvasNodes
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
		}

		// Triggers
		private void AddSignalNode_OnClick(object sender, RoutedEventArgs e)
		{
			new SignalNode(nodeCanvas);
		}

		// Conditions
		private void AddAndNode_OnClick(object sender, RoutedEventArgs e)
		{
			new AndNode(nodeCanvas);
		}
		private void AddOrNode_OnClick(object sender, RoutedEventArgs e)
		{
			new OrNode(nodeCanvas);
		}
		private void AddNotNode_OnClick(object sender, RoutedEventArgs e)
		{
			new NotNode(nodeCanvas);
		}
		private void AddToggleNode_OnClick(object sender, RoutedEventArgs e)
		{
			new ToggleSwitchNode(nodeCanvas);
		}
		private void AddTimedNode_OnClick(object sender, RoutedEventArgs e)
		{
			new TimedNode(nodeCanvas);
		}

		// Actions
		private void AddCameraRecordNode_OnClick(object sender, RoutedEventArgs e)
		{
			new RecordCameraNode(nodeCanvas);
		}
	}
}
