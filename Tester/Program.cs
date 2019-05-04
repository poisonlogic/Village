using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core;
using Village.Core.Loader;
using Village.Map.MapStructures;
using Village.Social.Population;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //MatingSim.MatingTest();
            //Console.ReadLine();

            //var msd = new MapStructDef();
            //DefLoader.WriteDef<MapStructDef>("C:/temp/msd.json", new List<MapStructDef> { msd });

            var defs = DefLoader.LoadDefs<MapStructDef>("C:/temp");
            var first = defs.First();

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
    }
}
