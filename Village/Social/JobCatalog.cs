using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social
{
    public static class JobCatalog
    {
        public static Dictionary<string, Job> All { get; private set; }
        public static void Init()
        {
            All = new Dictionary<string, Job>();
            All.Add("Miner", new Job
            {
                JobName = "Miner",
                PayLevel = PayLevel.Poor,
                MaxWorkerCount = 5,
                ProducedResources = new Dictionary<string, int>()
            });
            All["Miner"].ProducedResources.Add("Iron", 5);
        }
    }
}
