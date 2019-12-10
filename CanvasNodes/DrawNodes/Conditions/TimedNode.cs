using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace CanvasNodes.DrawNodes.Conditions
{
	class TimedNode : NodeBody
	{
		private int time = 1;
		private Timer timer;

		public TimedNode(Canvas parentCanvas) : base(parentCanvas)
		{
			TextPrompt textPrompt = new TextPrompt();
			if (textPrompt.ShowDialog() == true)
			{
				if (!string.IsNullOrEmpty(textPrompt.ResponseText))
				{
					time = int.Parse(textPrompt.ResponseText);
				}
			}

			Text = "Timer: " + time;
			timer = new Timer(time*1000);
			timer.AutoReset = false;
			timer.Elapsed += TimerTick;

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
				Active = true;
				timer.Stop();
				timer.Start();
			}
		}

		private void TimerTick(object sender, EventArgs e)
		{
			timer.Stop();
			Application.Current.Dispatcher.Invoke(() =>
			{
				Active = false;
			});
		}
	}
}
