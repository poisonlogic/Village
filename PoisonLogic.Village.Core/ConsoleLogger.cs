using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Village.Core
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string s)
        {
            Console.WriteLine(s);
        }
    }
}
