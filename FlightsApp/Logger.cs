using System;

namespace FlightsApp
{
	public class Logger
	{
		private readonly global::Gtk.TextView textview;

		public Logger(global::Gtk.TextView textview)
		{
			this.textview = textview;
		}

		public void Info(string message)
		{
			textview.Buffer.Text += "Info: " + message + Environment.NewLine;
		}

		public void Error(string message)
		{
			textview.Buffer.Text += "Error: " + message + Environment.NewLine;
		}
}
}
