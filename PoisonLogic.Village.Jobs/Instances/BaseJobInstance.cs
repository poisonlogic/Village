using System;
using System.Collections.Generic;
using System.Linq;
namespace PoisonLogic.Village.Jobs
{
    public class BaseJobInstance : JobInstance
    {
        private List<IJobWorker> _workers;

        public BaseJobInstance()
        {
        }

        public override bool CanAddWorker(IJobWorker worker)
        {
            if (Workers.Count() >= JobDef.MaxWorkerCount)
                return false;

            return true;
        }

        public override bool HasOpenPosition()
        {
            throw new NotImplementedException();
        }

        public override bool TryAddWorker(IJobWorker worker)
        {
            return true;
        }

        
    }
}
