using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace CanvasNodes.DrawNodes.LogicGates
{
	class ToggleSwitchNode : NodeBody
	{
		public ToggleSwitchNode(Canvas parentCanvas) : base(parentCanvas)
		{
			Text = "Toggle";

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
			if (inputSockets[0].Active)
			{
				Active = !Active;
			}
		}
	}
}
