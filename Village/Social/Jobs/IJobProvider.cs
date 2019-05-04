using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Social.Jobs;

namespace Village.Social
{
    public interface IJobProvider
    {
        string Name { get; }
        Guid InstanceId { get; }
        IEnumerable<string> Tags { get; }

        IEnumerable<Villager> Workers { get; }
        IEnumerable<Villager> FreeWorkers { get; }
        bool HasOpenWorkerSlot { get; }
        bool TryAddWorker(Villager worker);
        bool TryRemoveWorker(Villager worker);


        IEnumerable<JobDef> AllJobDrafts { get; }
        IEnumerable<IJobInstance> ActiveJobs { get; }
        void TryStartAllJobs();
        void TryCancelAllJobs();
        void AnyJobsFinished();
    }
}
