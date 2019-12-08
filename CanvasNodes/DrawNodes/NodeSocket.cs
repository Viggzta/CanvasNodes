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
		public List<NodeEdge> edges;

		private bool isActive;

		public bool Active
		{
			get => isActive;
			set
			{
				if (isActive != value)
				{
					isActive = value;
					UpdateColor();
					if (IsOutput)
					{
						NotifyEdges(isActive);
					}
					else
					{
						parent.OnInput();
					}
				}
			}
		}

		public bool IsOutput { get; private set; }

		public Point Position
		{
			get => new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse));
		}
		public Vector Origin
		{
			get => new Vector(ellipse.Width / 2, ellipse.Height / 2);
		}
		public Point Offset { get; set; }

		public NodeSocket(Canvas parentCanvas, NodeBody parent, Point offset, bool isOutput)
		{
			this.parentCanvas = parentCanvas;
			this.parent = parent;
			this.Offset = offset;
			IsOutput = isOutput;
			edges = new List<NodeEdge>();

			ellipse = new Ellipse();
			ellipse.Width = 10;
			ellipse.Height = 10;
			UpdateColor();

			ellipse.MouseDown += AddEdge;
			ellipse.MouseUp += AddEdgeEnd;
			parentCanvas.MouseUp += RemoveLooseEdges;

			parentCanvas.Children.Add(ellipse);

			Canvas.SetLeft(ellipse, parent.Position.X + offset.X);
			Canvas.SetTop(ellipse, parent.Position.Y + offset.Y);
		}

		private void NotifyEdges(bool value)
		{
			foreach (NodeEdge edge in edges)
			{
				edge.Active = value;
			}
		}

		private void UpdateColor()
		{
			if (Active)
			{
				ellipse.Fill = BlackBoard.AxisRed;
			}
			else
			{
				ellipse.Fill = Brushes.Black;
			}
		}

		public void MoveWithParent()
		{
			Canvas.SetLeft(ellipse, parent.Position.X + Offset.X - (ellipse.Width / 2));
			Canvas.SetTop(ellipse, parent.Position.Y + Offset.Y - (ellipse.Height / 2));
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
				BlackBoard.HeldEdge.to.Active = BlackBoard.HeldEdge.Active;
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
