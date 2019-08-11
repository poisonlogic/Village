using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoisonLogic.Dim;

namespace PoisonLogic.Village.Jobs
{
    public class JobManager : IDimManager
    {
        public static JobManager Instance;

        private Dictionary<string, JobInstance> _jobs;
        private Dictionary<string, IJobWorker> _registeredWorkers;

        public IEnumerable<JobInstance> AllJobs { get { return _jobs.Select(x => x.Value); } }
        public IEnumerable<JobInstance> OpenJobs { get { return AllJobs.Where(x => x.HasOpenPosition()); } }
        public IEnumerable<IJobWorker> AllWorkersOnJobs { get { return AllJobs.SelectMany(x => x.Workers); } }

        IEnumerable<JobInstance> AllInstances => throw new NotImplementedException();

        IDimCatalog<JobDef> Catalog => throw new NotImplementedException();

        Type IDimManager.InstanceType => typeof(JobInstance);

        IEnumerable<IDimInstance> IDimManager.AllInstances => throw new NotImplementedException();

        public JobManager()
        {
            if (Instance == null)
                Instance = this;
            else
                throw new Exception("Try to create 2nd job manager");

            _jobs = new Dictionary<string, JobInstance>();
            _registeredWorkers = new Dictionary<string, IJobWorker>();

        }

        //public bool TryRegisterNewWorker(IJobWorker worker)
        //{
        //    if(!base.TryRegisterUser(worker))
        //    {
        //        return false;
        //    }
        //    if (_registeredWorkers.ContainsKey(worker.InstanceId))
        //        return false;
        //    _registeredWorkers.Add(worker.InstanceId, worker);
        //    return true;
        //}

        //public bool TryRegisterJobProvider(IJobProvider<JobDef> jobProvider)
        //{
        //    if (_registeredJobProviders.ContainsKey(jobProvider.InstanceId))
        //        return false;
        //    _registeredJobProviders.Add(jobProvider.InstanceId, jobProvider);
        //    return true;

        //}

        //public IEnumerable<IJobInstance<JobDef>> FindOpenJobsFor(IJobWorker<JobDef> worker)
        //{
        //    var options = OpenJobs.Where(x => x.CanAddWorker(worker));

        //    return options;
        //}

        //public bool FindOpenJobForAndHire(IJobWorker<JobDef> worker)
        //{
        //    var options = FindOpenJobsFor(worker);
        //    if (!options.Any())
        //        return false;

        //    var priorityOrdered = options.OrderBy(op => op.Priority);

        //    foreach(var job in priorityOrdered)
        //    {
        //        var hired = job.TryAddWorker(worker);
        //        if (hired)
        //        {
        //            worker.TryTransferToNewJob(job);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public IJobProvider<JobDef> GetJobProvider(IJobWorker<JobDef> worker)
        //{
        //    var jobInstanceId = worker.JobId;
        //    if(!_jobs.ContainsKey(jobInstanceId))
        //        throw new Exception("Worker found with JobInstanceId: " + jobInstanceId + " But no job matches id");
        //    return _jobs[jobInstanceId].JobProvider;
        //}

        //public IJobInstance<JobDef> GetJob(string jobId)
        //{
        //    return _jobs[jobId];
        //}

        //public IEnumerable<IJobWorker<JobDef>> GetWorker(IJobProvider<JobDef> jobProvider)
        //{
        //    var jobIds = new List<string>();
        //    foreach(var jobid in jobIds)
        //    {
        //        if (!_jobs.ContainsKey(jobid))
        //            throw new Exception("Worker found with JobInstanceId: " + jobid + " But no job matches id");
        //        else
        //            foreach(var worker in _jobs[jobid].Workers)
        //                yield return worker;
        //    }

        //}

        public string PrintState()
        {
            var sb = new StringBuilder();

            if (_jobs == null ||  _jobs.Count < 0)
                return "No jobs";

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

        public bool ValidateCanAccept(IDimInstance instance)
        {
            throw new NotImplementedException();
        }
        

        bool IDimManager.TryLinkForeignInstance(IDimInstance instance)
        {
            throw new NotImplementedException();
        }

        bool IDimManager.ValidateCanAccept(IDimInstance instance)
        {
            throw new NotImplementedException();
        }

        void IDimManager.Update()
        {
            throw new NotImplementedException();
        }

        //public override bool TryDestroyInstance(string instance)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool TryTransferInstance(JobInstance instance)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void InformOfInstanceChange(JobInstance instance)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
