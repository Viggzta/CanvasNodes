using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CanvasNodes.DrawNodes
{
	public class NodeEdge
	{
		private readonly Vector offsetVector = new Vector(10, 0);

		private Canvas parentCanvas;
		public Polyline line;
		private PointCollection points;
		private Vector offset;

		public NodeSocket from;
		public NodeSocket to;

		public NodeEdge(Canvas parentCanvas, NodeSocket from)
		{
			this.parentCanvas = parentCanvas;
			this.from = from;

			offset = from.Origin;

			line = new Polyline();
			line.Stroke = Brushes.Black;
			line.StrokeThickness = 2;
			Canvas.SetZIndex(line, -1);

			points = new PointCollection();
			points.Add(from.Position + offset);
			points.Add(from.Position + offset);
			points.Add(from.Position + offset);
			points.Add(from.Position + offset);
			line.Points = points;
			parentCanvas.MouseMove += Move;
			line.MouseRightButtonDown += SelfDestruct;

			parentCanvas.Children.Add(line);
		}

		private void SelfDestruct(object sender, MouseButtonEventArgs e)
		{
			parentCanvas.Children.Remove(line);
			from.edges.Remove(this);
			to.edges.Remove(this);
		}

		private void Move(object sender, RoutedEventArgs e)
		{
			points[0] = from.Position + offset;
			points[1] = from.Position + offset + offsetVector;
			if (to !=null)
			{
				points[3] = to.Position + offset;
				points[2] = to.Position + offset - offsetVector;
			}
			else
			{
				points[3] = (e as MouseEventArgs).GetPosition(parentCanvas);
				points[2] = (e as MouseEventArgs).GetPosition(parentCanvas) - offsetVector;
			}
		}
	}
}
