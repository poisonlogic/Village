using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Jobs
{
    public class JobManager
    {
        private Dictionary<string, IJobInstance> _jobs;

        public IEnumerable<IJobInstance> AllJobs { get { return _jobs.Select(x => x.Value); } }
        public IEnumerable<IJobInstance> OpenJobs { get { return AllJobs.Where(x => x.HasOpenPosition()); } }
        public IEnumerable<IJobWorker> AllWorkers { get { return AllJobs.SelectMany(x => x.Workers); } }

        public JobManager()
        {
            _jobs = new Dictionary<string, IJobInstance>();

        }

        public IEnumerable<IJobInstance> FindJobsFor(IJobWorker worker)
        {
            var options = OpenJobs.Where(x => x.CanAddWorker(worker));

            return options;
        }

        public bool FindJobForAndHire(IJobWorker worker)
        {
            var options = FindJobsFor(worker);
            if (!options.Any())
                return false;

            var priorityOrdered = options.OrderBy(op => op.Priority);

            foreach(var job in priorityOrdered)
            {
                var hired = job.TryAddWorker(worker);
                if (hired)
                {
                    worker.SetCurrentJobId(job.InstanceId);
                    return true;
                }
            }
            return false;
        }

        public IJobProvider GetJobProvider(IJobWorker worker)
        {
            var jobInstanceId = worker.GetCurrentJobId();
            if(!_jobs.ContainsKey(jobInstanceId))
                throw new Exception("Worker found with JobInstanceId: " + jobInstanceId + " But no job matches id");
            return _jobs[jobInstanceId].JobProvider;
        }

        public IEnumerable<IJobWorker> GetWorker(IJobProvider jobProvider)
        {
            var jobIds = jobProvider.GetCurrentJobsId();
            foreach(var jobid in jobIds)
            {
                if (!_jobs.ContainsKey(jobid))
                    throw new Exception("Worker found with JobInstanceId: " + jobid + " But no job matches id");
                else
                    foreach(var worker in _jobs[jobid].Workers)
                        yield return worker;
            }

        }
    }
}
