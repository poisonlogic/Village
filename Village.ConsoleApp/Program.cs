using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using Village.ConsoleApp.Classes;
using Village.Core;
using Village.Core.Map;
using Village.Core.Time;

namespace Village.ConsoleApp
{
    class Program
    {
        private static Char GetKeyPress(String msg, Char[] validChars)
        {
            ConsoleKeyInfo keyPressed;
            bool valid = false;

            Console.WriteLine();
            do
            {
                Console.Write(msg);
                keyPressed = Console.ReadKey();
                Console.WriteLine();
                if (Array.Exists(validChars, ch => ch.Equals(Char.ToUpper(keyPressed.KeyChar))))
                    valid = true;

            } while (!valid);
            return keyPressed.KeyChar;
        }
        static void Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            var gameMaster = serviceProvider.GetRequiredService<GameMaster>();
            gameMaster.Update();

            while(true)
            {
                gameMaster.Update();
                Thread.Sleep(600);
                Console.Clear();
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static ServiceProvider BuildServiceProvider()
        {

            var serviceCollection = new ServiceCollection()
                .AddSingleton<GameMaster>()
               .AddTransient<IFileHandler, FileHandler>()
               .AddTransient<ILogger, Logger>()
               .AddTransient<IMapRenderer, MapRenderer>();

            Village.Core.Map.ServiceInjector.AddServices(serviceCollection);
            Village.Core.Time.ServiceInjector.AddServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }
    }
}
