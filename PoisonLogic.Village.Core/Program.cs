using Newtonsoft.Json;
using PoisonLogic.Dim;
using System;
using System.IO;

namespace PoisonLogic.Village.Core
{
    class Program
    {
        public static bool Running;
        public static Administrator Administrator;

        static void Main(string[] args)
        {
            Administrator = new Administrator(new ConsoleLogger(), @"C:\VillagePackets");// C:\Users\Jack\source\repos\Village\PoisonLogic.Village.Core\DimPackages");
            Administrator.PacketWarehouse.LoadAllPackets();
            Administrator.LoadAllAssemblies();
            Administrator.LoadAllManagers();

            Administrator.ListAllInstances();
            Running = true;
            while (Running)
            {
                var line = Console.ReadLine();
                ProcessInput(line);
            }


        }

        public static void ProcessInput(string line)
        {
            var tokens = line.Split(' ');
            switch (tokens[0])
            {
                case "quit":
                case "q":
                    Running = false;
                    break;
                case "print":
                    if (tokens.Length > 1 && tokens[1] == "pop")
                        Administrator.Log(Administrator.GetPacketManager("PoisonLogic.Village.Population").PrintState());
                    else if (tokens.Length > 1 && tokens[1] == "job")
                        Administrator.Log(Administrator.GetPacketManager("PoisonLogic.Village.Jobs").PrintState());
                    break;
                case "new":
                    //if (tokens.Length > 1 && tokens[1] == "pop")
                    //    //CreateNewPop();
                        break;
                default:
                    Administrator.Log($"token {tokens[0]} not recognized.");
                    Administrator.Log("--------------------------------------------------------------");
                    break;

            }

        }
    }
}
