using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace CanvasNodes.DrawNodes
{
	public class NodeBody
	{
		private Nullable<Point> dragStart = null;
		private Canvas parentCanvas;
		private Rectangle rectangle;
		private List<NodeSocket> sockets;

		private TextBlock textBlock;
		private Vector textBlockOffset = new Vector(10, 21);

		public Point Position
		{
			get => new Point(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle));
		}

		public NodeBody(Canvas parentCanvas)
		{
			this.parentCanvas = parentCanvas;
			sockets = new List<NodeSocket>();

			rectangle = new Rectangle();
			rectangle.Width = 100;
			rectangle.Height = 60;
			rectangle.Fill = BlackBoard.AxisYellow;
			rectangle.RadiusX = 2;
			rectangle.RadiusY = 2;
			rectangle.Stroke = BlackBoard.AxisOrange;
			rectangle.StrokeThickness = 1;

			textBlock = new TextBlock();
			textBlock.Foreground = Brushes.Black;
			textBlock.FontSize = 12;
			textBlock.TextAlignment = TextAlignment.Center;
			textBlock.Width = 80;
			textBlock.IsHitTestVisible = false;
			Panel.SetZIndex(textBlock,5);
			parentCanvas.Children.Add(textBlock);

			TextPrompt textPrompt = new TextPrompt();
			if (textPrompt.ShowDialog() == true)
			{
				textBlock.Text = textPrompt.ResponseText;
			}

			rectangle.MouseDown += MoveNodeStart;
			rectangle.MouseUp += MoveNodeStop;
			rectangle.MouseMove += MoveNode;

			parentCanvas.Children.Add(rectangle);
		}

		public void AddSocket()
		{
			NodeSocket socket1 = new NodeSocket(parentCanvas, this, new Point(0, 30), false);
			sockets.Add(socket1);
			NodeSocket socket2 = new NodeSocket(parentCanvas, this, new Point(100, 30), true);
			sockets.Add(socket2);
		}

		private void MoveNodeStart(object sender, RoutedEventArgs e)
		{
			var element = (UIElement)sender;
			dragStart = (e as MouseButtonEventArgs).GetPosition(element);
			element.CaptureMouse();
		}

		private void MoveNodeStop(object sender, RoutedEventArgs e)
		{
			var element = (UIElement)sender;
			dragStart = null;
			element.ReleaseMouseCapture();
		}

		private void MoveNode(object sender, RoutedEventArgs e)
		{
			if (dragStart != null &&
			    (e as MouseEventArgs).LeftButton == MouseButtonState.Pressed)
			{
				var element = (UIElement)sender;
				var p2 = (e as MouseEventArgs).GetPosition(parentCanvas);
				Canvas.SetLeft(element, p2.X - dragStart.Value.X);
				Canvas.SetTop(element, p2.Y - dragStart.Value.Y);

				Canvas.SetLeft(textBlock, Position.X + textBlockOffset.X);
				Canvas.SetTop(textBlock, Position.Y + textBlockOffset.Y);

				foreach (NodeSocket socket in sockets)
				{
					socket.MoveWithParent();
				}
			}
		}
	}
}
