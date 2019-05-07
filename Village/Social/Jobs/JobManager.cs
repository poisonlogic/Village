using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Jobs
{
    public class JobManager
    {
        public static JobManager Instance;

        private Dictionary<string, IJobInstance> _jobs;
        private Dictionary<string, IJobWorker> _registeredWorkers;
        private Dictionary<string, IJobProvider> _registeredJobProviders;

        public IEnumerable<IJobInstance> AllJobs { get { return _jobs.Select(x => x.Value); } }
        public IEnumerable<IJobInstance> OpenJobs { get { return AllJobs.Where(x => x.HasOpenPosition()); } }
        public IEnumerable<IJobWorker> AllWorkersOnJobs { get { return AllJobs.SelectMany(x => x.Workers); } }

        public JobManager()
        {
            Instance = this;
            _jobs = new Dictionary<string, IJobInstance>();
            _registeredWorkers = new Dictionary<string, IJobWorker>();
            _registeredJobProviders = new Dictionary<string, IJobProvider>();

        }

        public bool TryRegisterNewWorker(IJobWorker worker)
        {
            if (_registeredWorkers.ContainsKey(worker.InstanceId))
                return false;
            _registeredWorkers.Add(worker.InstanceId, worker);
            return true;
        }

        public bool TryRegisterJobProvider(IJobProvider jobProvider)
        {
            if (_registeredJobProviders.ContainsKey(jobProvider.InstanceId))
                return false;
            _registeredJobProviders.Add(jobProvider.InstanceId, jobProvider);
            return true;

        }

        public IEnumerable<IJobInstance> FindOpenJobsFor(IJobWorker worker)
        {
            var options = OpenJobs.Where(x => x.CanAddWorker(worker));

            return options;
        }

        public bool FindOpenJobForAndHire(IJobWorker worker)
        {
            var options = FindOpenJobsFor(worker);
            if (!options.Any())
                return false;

            var priorityOrdered = options.OrderBy(op => op.Priority);

            foreach(var job in priorityOrdered)
            {
                var hired = job.TryAddWorker(worker);
                if (hired)
                {
                    worker.TryTransferToNewJob(job);
                    return true;
                }
            }
            return false;
        }

        public IJobProvider GetJobProvider(IJobWorker worker)
        {
            var jobInstanceId = worker.JobId;
            if(!_jobs.ContainsKey(jobInstanceId))
                throw new Exception("Worker found with JobInstanceId: " + jobInstanceId + " But no job matches id");
            return _jobs[jobInstanceId].JobProvider;
        }

        public IJobInstance GetJob(string jobId)
        {
            return _jobs[jobId];
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

        public string PrintState()
        {
            var sb = new StringBuilder();
            foreach(var jobPair in _jobs)
            {
                var job = jobPair.Value;
                sb.AppendLine("------------------------------------------------------");
                sb.AppendLine(string.Format("Job Name: {0} InstanceId: {1}", job.JobDef.JobName, job.InstanceId));
                sb.AppendLine(string.Format("Possible Works: {0}, Current Workers: {1}", job.JobDef.MaxWorkerCount, job.Workers.Count()));
                sb.AppendLine(string.Format("Workers: {0}", string.Join(", ", job.Workers.Select(x => x.Label))));
                sb.AppendLine("------------------------------------------------------");
            }
            return sb.ToString();
        }
    }
}
