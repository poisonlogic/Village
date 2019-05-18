using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.DIMCUP;

namespace Village.Social.Jobs
{
    public class JobManager<TDef> : BaseDimcupManager<TDef> where TDef : JobDef
    {
        public static JobManager<TDef> Instance;

        private Dictionary<string, IJobInstance<TDef>> _jobs;
        private Dictionary<string, IJobWorker<TDef>> _registeredWorkers;
        private Dictionary<string, IJobProvider<TDef>> _registeredJobProviders;

        public IEnumerable<IJobInstance<TDef>> AllJobs { get { return _jobs.Select(x => x.Value); } }
        public IEnumerable<IJobInstance<TDef>> OpenJobs { get { return AllJobs.Where(x => x.HasOpenPosition()); } }
        public IEnumerable<IJobWorker<TDef>> AllWorkersOnJobs { get { return AllJobs.SelectMany(x => x.Workers); } }

        public JobManager()
        {
            Instance = this;
            _jobs = new Dictionary<string, IJobInstance<TDef>>();
            _registeredWorkers = new Dictionary<string, IJobWorker<TDef>>();
            _registeredJobProviders = new Dictionary<string, IJobProvider<TDef>>();

        }

        public bool TryRegisterNewWorker(IJobWorker<TDef> worker)
        {
            if(!base.TryRegisterUser(worker))
            {
                return false;
            }
            if (_registeredWorkers.ContainsKey(worker.InstanceId))
                return false;
            _registeredWorkers.Add(worker.InstanceId, worker);
            return true;
        }

        public bool TryRegisterJobProvider(IJobProvider<TDef> jobProvider)
        {
            if (_registeredJobProviders.ContainsKey(jobProvider.InstanceId))
                return false;
            _registeredJobProviders.Add(jobProvider.InstanceId, jobProvider);
            return true;

        }

        public IEnumerable<IJobInstance<TDef>> FindOpenJobsFor(IJobWorker<TDef> worker)
        {
            var options = OpenJobs.Where(x => x.CanAddWorker(worker));

            return options;
        }

        public bool FindOpenJobForAndHire(IJobWorker<TDef> worker)
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

        public IJobProvider<TDef> GetJobProvider(IJobWorker<TDef> worker)
        {
            var jobInstanceId = worker.JobId;
            if(!_jobs.ContainsKey(jobInstanceId))
                throw new Exception("Worker found with JobInstanceId: " + jobInstanceId + " But no job matches id");
            return _jobs[jobInstanceId].JobProvider;
        }

        public IJobInstance<TDef> GetJob(string jobId)
        {
            return _jobs[jobId];
        }

        public IEnumerable<IJobWorker<TDef>> GetWorker(IJobProvider<TDef> jobProvider)
        {
            var jobIds = new List<string>();
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
                sb.AppendLine(string.Format("Job Name: {0} InstanceId: {1}", job.JobDef.DefName, job.InstanceId));
                sb.AppendLine(string.Format("Possible Works: {0}, Current Workers: {1}", job.JobDef.MaxWorkerCount, job.Workers.Count()));
                sb.AppendLine(string.Format("Workers: {0}", string.Join(", ", job.Workers.Select(x => x.Label))));
                sb.AppendLine("------------------------------------------------------");
            }
            return sb.ToString();
        }

        public override bool TryTransferInstance(IDimcupInstance<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfInstanceChange(IDimcupInstance<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfUserChange(IDimcupUser<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfProviderChange(IDimcupProvider<TDef> instance)
        {
            throw new NotImplementedException();
        }
    }
}
