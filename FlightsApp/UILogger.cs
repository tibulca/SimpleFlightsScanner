using System;

namespace FlightsApp
{
	public class UILogger : ILogger
	{
		private readonly global::Gtk.TextView textview;

		public UILogger(global::Gtk.TextView textview)
		{
			this.textview = textview;
		}

		public void Info(string message)
		{
			textview.Buffer.Text += $"{message}{Environment.NewLine}";
		}

		public void Error(string message)
		{
			textview.Buffer.Text += $"Error: {message}{Environment.NewLine}";
		}

		public void Error(Exception ex)
		{
			textview.Buffer.Text += $"Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}";
		}
	}
}
