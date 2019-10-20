using Microsoft.Extensions.DependencyInjection;
using System;
using Village.Core;
using Village.Core.Rendering;
using Village.DesktopApp.Classes;

namespace Village.DesktopApp
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var gameMaster = BuildServiceProvider().GetRequiredService<GameMaster>();
            using (var game = new GameWindow(gameMaster))
                game.Run();
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
            Village.Core.Items.ServiceInjector.AddServices(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
