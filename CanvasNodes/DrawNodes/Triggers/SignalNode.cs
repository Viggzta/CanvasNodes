using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CanvasNodes.DrawNodes.Triggers
{
	class SignalNode : NodeBody
	{
		private Button button;

		public SignalNode(Canvas parentCanvas) : base(parentCanvas)
		{
			button = new Button();
			button.Width = 80;
			button.Height = 20;
			button.Click += OnButtonPressedAsync;
			Panel.SetZIndex(button, 5);
			parentCanvas.Children.Add(button);

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
			Active = inputSockets[0].Active;
		}

		public async void OnButtonPressedAsync(object sender, RoutedEventArgs e)
		{
			Active = true;
			await Task.Delay(100);
			Active = false;
		}

		protected override void MoveNode(object sender, RoutedEventArgs e)
		{
			base.MoveNode(sender, e);
			Canvas.SetLeft(button, Position.X + textBlockOffset.X);
			Canvas.SetTop(button, Position.Y + textBlockOffset.Y);
		}
	}
}
