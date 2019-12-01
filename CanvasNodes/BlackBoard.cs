using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using CanvasNodes.DrawNodes;

namespace CanvasNodes
{
	public static class BlackBoard
	{
		public static NodeEdge HeldEdge = null;
		public static SolidColorBrush AxisRed = (SolidColorBrush)(new BrushConverter().ConvertFrom("#fe003f"));
		public static SolidColorBrush AxisYellow = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffcf4c"));
		public static SolidColorBrush AxisOrange = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffad31"));
	}
}
