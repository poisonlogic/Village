using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Jobs
{
    public abstract class BaseTask : ITask
    {
        protected bool _paused;
        protected bool _active;
        public IJobWorker Worker { get; }
        public bool IsActive => _active && !_paused;

        public BaseTask(IJobWorker worker)
        {
            Worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }
        
        public abstract bool CanPause();
        public abstract bool CanCancel();
        public abstract bool CanStart();

        public virtual void Start()
        {
            _active = true;
        }

        public virtual void Pause()
        {
            _paused = true;
        }

        public virtual void UnPause()
        {
            _paused = false;
        }

        public virtual void Cancel()
        {
            _active = false;
        }

        public abstract bool IsCompleted();
        public abstract void DoUpdate();
    }
}
