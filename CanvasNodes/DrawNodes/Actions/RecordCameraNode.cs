using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace CanvasNodes.DrawNodes.Actions
{
	class RecordCameraNode : NodeBody
	{
		public RecordCameraNode(Canvas parentCanvas) : base(parentCanvas)
		{
			TextPrompt textPrompt = new TextPrompt();
			Text = "Record";
			if (textPrompt.ShowDialog() == true)
			{
				if (!string.IsNullOrEmpty(textPrompt.ResponseText))
				{
					Text = textPrompt.ResponseText;
				}
			}

			AddSocket(SocketType.Input);
		}

		public override void OnActivate()
		{

		}

		public override void OnDeactivate()
		{
		}

		public override void OnInput()
		{
			Active = inputSockets[0].Active;
		}
	}
}
