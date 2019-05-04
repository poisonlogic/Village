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
        public Guid InstanceId { get; private set; }
        public IEnumerable<IJobWorker> Workers { get { return _workers; } }
        public IEnumerable<string> Tags { get; }
        public JobDef Job { get; private set; }
        public IPlaceOfWork Building { get; private set; }

        public bool Disabled { get; set; }
        public bool Running { get; private set; }

        public JobState JobState { get; private set; }

        private SimpleTime _startedAt;

        public BaseJobInstance(JobDef JobDraft, IPlaceOfWork building)
        {
            Name = JobDraft.JobName;
            InstanceId = Guid.NewGuid();
            Building = building;
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
            if (_workers.Count() >= Job.MaxWorkerCount)
                return false;
            if (worker.EducationLevel < Job.RequiredEducationLevel)
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

        public virtual bool TryFinsh()
        {
            return true;
        }

        public virtual SimpleTime StartedAt()
        {
            return _startedAt;
        }

        public virtual SimpleTime WillFinishAt()
        {
            return _startedAt + Job.TimeToComplete;
        }
    }
}
