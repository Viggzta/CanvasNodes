using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CanvasNodes.DrawNodes.LogicGates
{
	class AndNode : NodeBody
	{
		public AndNode(Canvas parentCanvas) : base(parentCanvas)
		{
			Text = "AND";

			AddSocket(SocketType.Input);
			AddSocket(SocketType.Input);

			AddSocket(SocketType.Output);
		}

		public override void OnActivate()
		{
			
		}

		public override void OnDeactivate()
		{
		}

		public override void OnInput()
		{
			foreach (NodeSocket socket in inputSockets)
			{
				if (!socket.Active)
				{
					Active = false;
					return;
				}
			}

			Active = true;
		}
	}
}
