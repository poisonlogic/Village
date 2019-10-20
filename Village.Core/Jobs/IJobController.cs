using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Jobs
{
    public interface IJobController
    {

        IEnumerable<IActiveJob> AllActiveJobs { get; }
        IEnumerable<IJobWorker> AllWorkers { get; }
        void RegisterNewWorker(IJobWorker worker);
        void UpdateJobs();

    }
}
