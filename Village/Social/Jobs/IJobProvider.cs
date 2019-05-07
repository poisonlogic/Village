using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Social.Jobs;

namespace Village.Social.Jobs
{
    public interface IJobProvider
    {
        string Label { get; }
        string InstanceId { get; }
        IEnumerable<string> Tags { get; }

        //bool HasOpenWorkerSlot { get; }
        //bool TryAddWorker(IJobWorker worker);
        //bool TryRemoveWorker(IJobWorker worker);
        
        //void TryStartAllJobs();
        //void TryCancelAllJobs();
        //void AnyJobsFinished();

        //IEnumerable<string> GetCurrentJobsId();
        //void SetCurrentJobsId(IEnumerable<string> jobids);
    }
}
