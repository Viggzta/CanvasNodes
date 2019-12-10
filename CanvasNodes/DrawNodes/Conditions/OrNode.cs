using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace CanvasNodes.DrawNodes.LogicGates
{
	class OrNode : NodeBody
	{
		public OrNode(Canvas parentCanvas) : base(parentCanvas)
		{
			Text = "OR";

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
				if (socket.Active)
				{
					Active = true;
					return;
				}
			}

			Active = false;
		}
	}
}
