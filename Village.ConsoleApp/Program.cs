using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using Village.ConsoleApp.Classes;
using Village.Core;
using Village.Core.Buildings;
using Village.Core.Map;
using Village.Core.Map.MapStructure;
using Village.Core.Rendering;
using Village.Core.Time;

namespace Village.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            var gameMaster = serviceProvider.GetRequiredService<GameMaster>();

            while (true)
            {
                Console.Clear();
                gameMaster.Update();
                Thread.Sleep(300);
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void ClearScreen()
        {
            for (int n = 0; n < 50; n++)
                Console.WriteLine();
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
            Village.Core.Buildings.ServiceInjector.AddServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }
    }
}
