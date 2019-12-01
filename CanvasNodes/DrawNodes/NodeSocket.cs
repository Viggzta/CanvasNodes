using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CanvasNodes.DrawNodes
{
	public class NodeSocket
	{
		private Nullable<Point> dragStart = null;
		private Canvas parentCanvas;
		private NodeBody parent;
		private Ellipse ellipse;
		private Point offset;
		public List<NodeEdge> edges;

		public bool IsOutput { get; private set; }

		public Point Position
		{
			get => new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse));
		}
		public Vector Origin
		{
			get => new Vector(ellipse.Width / 2, ellipse.Height / 2);
		}

		public NodeSocket(Canvas parentCanvas, NodeBody parent, Point offset, bool isOutput)
		{
			this.parentCanvas = parentCanvas;
			this.parent = parent;
			this.offset = offset;
			IsOutput = isOutput;
			edges = new List<NodeEdge>();

			ellipse = new Ellipse();
			ellipse.Width = 10;
			ellipse.Height = 10;
			ellipse.Fill = BlackBoard.AxisRed;

			ellipse.MouseDown += AddEdge;
			ellipse.MouseUp += AddEdgeEnd;
			parentCanvas.MouseUp += RemoveLooseEdges;

			parentCanvas.Children.Add(ellipse);

			Canvas.SetLeft(ellipse, parent.Position.X + offset.X);
			Canvas.SetTop(ellipse, parent.Position.Y + offset.Y);
		}

		public void MoveWithParent()
		{
			Canvas.SetLeft(ellipse, parent.Position.X + offset.X - (ellipse.Width / 2));
			Canvas.SetTop(ellipse, parent.Position.Y + offset.Y - (ellipse.Height / 2));
		}

		private void AddEdge(object sender, RoutedEventArgs e)
		{
			if (IsOutput)
			{
				NodeEdge edge = new NodeEdge(parentCanvas, this);
				BlackBoard.HeldEdge = edge;
				edges.Add(edge);
			}
		}
		private void AddEdgeEnd(object sender, RoutedEventArgs e)
		{
			if (!IsOutput && BlackBoard.HeldEdge != null)
			{
				BlackBoard.HeldEdge.to = this;
				edges.Add(BlackBoard.HeldEdge);
				BlackBoard.HeldEdge = null;
			}
		}

		private void RemoveLooseEdges(object sender, MouseButtonEventArgs e)
		{
			var temp = edges.Where(e => e.to == null).ToList();
			for (int i = temp.Count - 1; i >= 0; i--)
			{
				parentCanvas.Children.Remove(temp[i].line);
				edges.Remove(temp[i]);
			}

			BlackBoard.HeldEdge = null;
		}
	}
}
