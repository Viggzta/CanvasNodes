using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace CanvasNodes.DrawNodes.Conditions
{
	class NotNode : NodeBody
	{
		public NotNode(Canvas parentCanvas) : base(parentCanvas)
		{
			Text = "NOT";

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
			Active = !inputSockets[0].Active;
		}
	}
}
