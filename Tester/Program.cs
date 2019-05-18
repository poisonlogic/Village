using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core;
using Village.Core.Loader;
using Village.Map.MapStructures;
using Village.Social.Jobs;
using Village.Social.Jobs.ResourceJobs;
using Village.Social.Population;

namespace Tester
{
    class Program
    {
        public static bool Running;
        public static VillageManager VillageManager;
        static void Main(string[] args)
        {
            //MatingSim.MatingTest();
            //Console.ReadLine();

            //var job = new ResourceJobDef
            //{
            //    JobName = "Miner",
            //    JobType = JobType.Repeat,
            //    MaxWorkerCount = 3,
            //    TimeToComplete = new SimpleTime(0, 0, 0, 0, 5, 0),
            //    Tags = new string[] { "mine" },
            //    ResourceExchanges = new Dictionary<string, int>()
            //};

            //job.ResourceExchanges.Add("resource_iron", 50);
            //DefLoader.WriteDef<ResourceJobDef>("C:/temp/jobdefs.json", new List<ResourceJobDef> { job });

            VillageManager = new VillageManager();

            Running = true;
            while(Running)
            {
                var line = Console.ReadLine();
                ProcessInput(line);
            }
            //var defs = DefLoader.LoadDefs<MapStructDef>("C:/temp");
            //var first = defs.First();

        }

        static decimal AppropriateAge(int A, int B)
        {
            var low = (A < B) ? (decimal)A : (decimal)B;
            var high = (A < B) ? (decimal)B : (decimal)A;
            var avg = (low + high) / 2;
            var range = avg / 5;

            var minAppAge = avg - range;
            var maxAppAge = avg + range;

            var lowDif = Math.Abs(low - minAppAge);
            var highDif = Math.Abs(high - maxAppAge);


            return Math.Max(lowDif, highDif);
        }

        public static void ProcessInput(string line)
        {
            var tokens = line.Split(' ');
            switch(tokens[0])
            {
                case "quit":
                case "q":
                    Running = false;
                    break;
                case "print":
                    if (tokens.Length > 1 && tokens[1] == "pop")
                        VillageManager.PrintPop();
                    else if (tokens.Length > 1 && tokens[1] == "job")
                        VillageManager.PrintJobs();
                    break;
                case "new":
                    if (tokens.Length > 1 && tokens[1] == "pop")
                        CreateNewPop();
                    break;

            }

        }

        public static void CreateNewPop()
        {
            //var pop = PopulationManager.RandomVillager();
            //if (VillageManager.TryAddVillager(pop))
            //    Console.WriteLine(string.Format("New Pop: {0} Sucessfully added", pop.Label));
            //else
            //    Console.WriteLine("Failed to add new pop");
        }
    }



    public interface IDef{}
    public interface IInst<D> where D : IDef{}
    public interface IMan<D> where D: IDef { IInst<D> Instance { get; } }

    public interface IADef : IDef { }
    public interface IAInst<D> : IInst<D> where D : IADef { }
    public interface IAMan<D> : IMan<D> where D : IADef { }
    
    public interface IBDef : IDef { }
    public interface IBInst<D> : IInst<D> where D : IBDef { }
    public interface IBMan<D> : IMan<D> where D : IBDef { }

    public class BaseDef : IADef { }
    public class BaseInst<D> : IInst<D> where D : BaseDef { }
    public class BaseMan<D> : IMan<D> where D : BaseDef
    {
        public BaseInst<D> Instance { get; }

        IInst<D> IMan<D>.Instance => Instance as IInst<D>;
    }

    public class ABDef : IADef, IBDef { }
    public class ABInst : IAInst<ABDef>, IBInst<ABDef> { }
    public class ABMan : IAMan<BaseDef>, IBMan<ABDef>
    {
        public IInst<ABDef> Instance { get; }

        IInst<BaseDef> IMan<BaseDef>.Instance => throw new NotImplementedException();
    }



}
