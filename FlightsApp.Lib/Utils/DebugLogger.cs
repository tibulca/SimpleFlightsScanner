using System;
using System.Diagnostics;

namespace FlightsApp.Lib.Utils
{
    public class DebugLogger : ILogger
    {
        public void Info(string message)
        {
            Debug.WriteLine($"{message}");
        }

        public void Error(string message)
        {
            Debug.WriteLine($"Error: {message}");
        }

        public void Error(Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
        }
    }
}
