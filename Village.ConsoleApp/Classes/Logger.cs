using System;
using System.Collections.Generic;
using System.Text;

namespace Village.ConsoleApp.Classes
{
    public class Logger : Village.Core.ILogger
    {
        public void LogError(string message)
        {
            Console.WriteLine(message);
        }

        public void LogError(string message, Exception e)
        {
            Console.WriteLine(message);

        }
    }
}
