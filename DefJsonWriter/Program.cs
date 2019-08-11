using Newtonsoft.Json;
using PoisonLogic.Dim;
using System;
using System.IO;

namespace DefJsonWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            //var minerJob = new JobDef
            //{
            //    DefName = "Miner",
            //    SourcePackageName = "PoisonLogic.Village.Jobs"

            //};

            //WriteDef<JobDef>(@"C:\Users\Jack\source\repos\Village\PoisonLogic.Village.Core\DimPackages\PoisonLogic.Village.Jobs\Miner.json", minerJob);
        }

        static void WriteDef<T>(string path, T def)
            where T : DimDef
        {
            var defJson = JsonConvert.SerializeObject(def);
            File.WriteAllText(path, defJson);
        }
    }
}
