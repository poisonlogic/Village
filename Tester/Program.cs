using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        public static bool Running;
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddLogging()
           //.AddSingleton<IFooService, FooService>()
           //.AddSingleton<IBarService, BarService>()
           .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);
            
        }
    }
}