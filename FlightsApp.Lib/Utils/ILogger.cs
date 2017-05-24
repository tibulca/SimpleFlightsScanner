using System;

namespace FlightsApp.Lib.Utils
{
    public interface ILogger
    {
        void Info(string message);

        void Error(string message);

        void Error(Exception ex);
    }
}
