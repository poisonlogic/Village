using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Jobs.Internal
{
    public class JobController : IJobController
    {
        private Dictionary<string, IJobWorker> _workers;
        private Dictionary<string, IActiveJob> _jobs;

        public IEnumerable<IActiveJob> AllActiveJobs => _jobs.Values;

        public IEnumerable<IJobWorker> AllWorkers => _workers.Values;

        public void RegisterNewWorker(IJobWorker worker)
        {
            if (_workers.ContainsKey(worker.Id))
                throw new Exception("Worker allready registerd");
            _workers.Add(worker.Id, worker);
        }

        public void UpdateJobs()
        {
            throw new NotImplementedException();
        }
    }
}
