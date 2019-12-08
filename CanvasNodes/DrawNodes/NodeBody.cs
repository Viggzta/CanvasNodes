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
	public abstract class NodeBody
	{
		private Nullable<Point> dragStart = null;
		private Canvas parentCanvas;
		private Rectangle rectangle;
		protected List<NodeSocket> inputSockets;
		protected List<NodeSocket> outputSockets;

		private TextBlock textBlock;
		protected readonly Vector textBlockOffset = new Vector(10, 21);

		private bool isActive;

		public bool Active
		{
			get => isActive;
			set
			{
				if (isActive != value)
				{
					isActive = value;
					NotifySockets(isActive);
					if (isActive) OnActivate();
					else OnDeactivate();
				}
			}
		}

		public Point Position
		{
			get => new Point(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle));
		}

		public double Height
		{
			get => rectangle.Height;
			set => rectangle.Height = value;
		}

		public double Width
		{
			get => rectangle.Width;
			set => rectangle.Width = value;
		}

		protected string Text
		{
			get => textBlock.Text;
			set => textBlock.Text = value;
		}

		public NodeBody(Canvas parentCanvas)
		{
			this.parentCanvas = parentCanvas;
			inputSockets = new List<NodeSocket>();
			outputSockets = new List<NodeSocket>();

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
			Panel.SetZIndex(textBlock, 5);
			parentCanvas.Children.Add(textBlock);

			rectangle.MouseDown += MoveNodeStart;
			rectangle.MouseUp += MoveNodeStop;
			rectangle.MouseMove += MoveNode;

			parentCanvas.Children.Add(rectangle);

			Active = false;
		}

		private void NotifySockets(bool value)
		{
			foreach (NodeSocket socket in outputSockets)
			{
				socket.Active = value;
			}
		}

		public void AddSocket(SocketType socketType)
		{
			switch (socketType)
			{
				case (SocketType.Input):
					inputSockets.Add(new NodeSocket(parentCanvas, this, new Point(0, 0), false));
					break;
				case (SocketType.Output):
					outputSockets.Add(new NodeSocket(parentCanvas, this, new Point(Width, 0), true));
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(socketType), socketType, null);
			}

			ReArrangeSockets(socketType);
		}

		private void ReArrangeSockets(SocketType socketType)
		{
			List<NodeSocket> socketsToMove = 
				(socketType == SocketType.Input) ? inputSockets : outputSockets;

			double offset = Height / (socketsToMove.Count + 1);

			for (var i = 0; i < socketsToMove.Count; i++)
			{
				NodeSocket inputSocket = socketsToMove[i];
				inputSocket.Offset = new Point(inputSocket.Offset.X,offset * (i + 1));
			}
		}

		private void MoveNodeStart(object sender, RoutedEventArgs e)
		{
			if ((e as MouseEventArgs).RightButton == MouseButtonState.Pressed)
			{
				Active = !Active;
			}

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

		protected virtual void MoveNode(object sender, RoutedEventArgs e)
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

				foreach (NodeSocket socket in inputSockets)
				{
					socket.MoveWithParent();
				}
				foreach (NodeSocket socket in outputSockets)
				{
					socket.MoveWithParent();
				}
			}
		}

		public abstract void OnActivate();

		public abstract void OnDeactivate();

		public abstract void OnInput();
	}
}
