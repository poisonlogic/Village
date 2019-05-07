using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;
using Village.Core;

namespace Village.Social.Jobs
{
    public abstract class BaseJobInstance : IJobInstance
    {
        private List<IJobWorker> _workers;

        public string Name { get; private set; }
        public string InstanceId { get; private set; }
        public IEnumerable<IJobWorker> Workers { get { return _workers; } }
        public IEnumerable<string> Tags { get; }
        public JobDef JobDef { get; private set; }
        public IJobProvider JobProvider{ get; private set; }
        public int Priority { get; private set; }
        public bool Disabled { get; set; }
        public bool Running { get; private set; }

        public JobState JobState { get; private set; }

        private SimpleTime _startedAt;

        public BaseJobInstance(string jobDef, IJobProvider jobProvider)
        {
            JobDef = new JobDef();
            Name = JobDef.JobName;
            InstanceId = Guid.NewGuid().ToString();
            JobProvider = jobProvider;
            _workers = new List<IJobWorker>();
        }

        public virtual bool TryAddWorker(IJobWorker worker)
        {
            if(CanAddWorker(worker))
            {
                _workers.Add(worker);
                return true;
            }
            return false;
        }

        public virtual bool CanAddWorker(IJobWorker worker)
        {
            if (_workers.Count() >= JobDef.MaxWorkerCount)
                return false;
            return true;
        }

        public virtual bool CanDoCycle()
        {
            if (this._workers.Count() == 0)
                return false;
            if (this.Disabled)
                return false;
            return true;
        }

        public virtual bool TryStart()
        {
            if (this._workers.Count() == 0 || this.Disabled)
            {
                this.JobState = JobState.CantRun;
                return false;
            }

            this.JobState = JobState.Running;
            this._startedAt = SimpleTime.Now;
            return true;
        }

        public virtual bool TryCancel()
        {
            this.JobState = JobState.Pending;
            return true;
        }

        public abstract bool TryFinsh();

        public virtual SimpleTime StartedAt()
        {
            return _startedAt;
        }

        public virtual SimpleTime WillFinishAt()
        {
            return _startedAt + JobDef.TimeToComplete;
        }

        public bool HasOpenPosition()
        {
            throw new NotImplementedException();
        }
    }
}
