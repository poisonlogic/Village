using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core;
using Village.Resources;
using Village.Social;
using Village.Social.Jobs;

namespace Village.Buildings
{
    public class Mines_TEMPLATE : IResourceUser
    {
        public string Name { get; private set; }
        public Guid InstanceId { get; private set; }
        public IEnumerable<string> Tags { get; }

        public ResourceUserType Type { get; private set; }
        public bool HasActiveRequest { get; private set; }
        public List<string> AllResourceIds { get; private set; }

        public bool HasOpenJobs { get; }
        public List<Villager> Workers { get; }
        public IEnumerable<JobDef> AllPossibleJobs { get; }

        public List<IJobInstance> ActiveJobs { get; }

        //IEnumerable<Villager> IJobProvider.Workers => throw new NotImplementedException();

        public IEnumerable<Villager> FreeWorkers => throw new NotImplementedException();

        public bool HasOpenWorkerSlot => throw new NotImplementedException();

        public IEnumerable<JobDef> AllJobDrafts => throw new NotImplementedException();

        //IEnumerable<IActiveJob> IJobProvider.ActiveJobs => throw new NotImplementedException();
        

        public List<ResourceRequest> GetAllActiveRequest()
        {
            throw new NotImplementedException();
        }

        public Mines_TEMPLATE()
        {
            InstanceId = Guid.NewGuid();
            Workers = new List<Villager>();
            AllPossibleJobs = LoadJobs();

            ActiveJobs = new List<IJobInstance>();
        }

        public void DoProductionCycle()
        {

        }

        public List<JobDef> LoadJobs()
        {
            var miner = new JobDef
            {
                JobName = "Miner",
                PayLevel = PayLevel.Poor
            };
            return new List<JobDef>();
        }

        public IEnumerable<JobDef> GetOpenJobs()
        {
            throw new NotImplementedException();
        }

        public bool TryAddWorker(Villager worker)
        {
            throw new NotImplementedException();
        }

        public bool TryRemoveWorker(Villager worker)
        {
            throw new NotImplementedException();
        }

        public void TryStartAllJobs()
        {
            throw new NotImplementedException();
        }

        public void TryCancelAllJobs()
        {
            throw new NotImplementedException();
        }

        public void AnyJobsFinished()
        {
            throw new NotImplementedException();
        }
    }
}
