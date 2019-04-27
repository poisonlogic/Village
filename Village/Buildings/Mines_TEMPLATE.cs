using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core;
using Village.Resources;
using Village.Social;

namespace Village.Buildings
{
    public class Mines_TEMPLATE : IResourceUser, IJobProvider
    {
        public string Name { get; private set; }
        public Guid InstanceId { get; private set; }
        public IEnumerable<string> Tags { get; }

        public ResourceUserType Type { get; private set; }
        public bool HasActiveRequest { get; private set; }
        public List<string> AllResourceIds { get; private set; }

        public bool HasOpenJobs { get; }
        public List<Villager> Workers { get; }
        public IEnumerable<Job> AllPossibleJobs { get; }

        public List<IActiveJob> ActiveJobs { get; }



        public List<ResourceRequest> GetAllActiveRequest()
        {
            throw new NotImplementedException();
        }

        public Mines_TEMPLATE()
        {
            InstanceId = Guid.NewGuid();
            Workers = new List<Villager>();
            AllPossibleJobs = LoadJobs();

            ActiveJobs = new List<IActiveJob>();
        }

        public void DoProductionCycle()
        {

        }

        public List<Job> LoadJobs()
        {
            var miner = new Job
            {
                JobName = "Miner",
                PayLevel = PayLevel.Poor
            };
            return new List<Job>();
        }

        public IEnumerable<Job> GetOpenJobs()
        {
            throw new NotImplementedException();
        }
    }
}
