using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;
using Village.Core;
using Village.Social.Jobs;

namespace Village.Social
{
    public class BaseActiveJob : IJobInstance
    {
        public JobDef Job { get; private set; }
        public List<Villager> Workers { get; private set; }
        public IBuilding Building { get; private set; }

        public bool Disabled => throw new NotImplementedException();

        public bool Running => throw new NotImplementedException();

        public JobState JobState => throw new NotImplementedException();

        IEnumerable<IJobWorker> IJobInstance.Workers => throw new NotImplementedException();

        IPlaceOfWork IJobInstance.Building => throw new NotImplementedException();

        public bool CanAddWorker(Villager worker)
        {
            if (worker.EducationLevel < Job.RequiredEducationLevel)
                return false;
            return true;
        }

        public bool CanAddWorker(IJobWorker worker)
        {
            throw new NotImplementedException();
        }

        public SimpleTime StartedAt()
        {
            throw new NotImplementedException();
        }

        public bool TryAddWorker(Villager worker)
        {
            if(CanAddWorker(worker))
            {
                this.Workers.Add(worker);
                return true;
            }
            return false;
        }

        public bool TryAddWorker(IJobWorker worker)
        {
            throw new NotImplementedException();
        }

        public bool TryCancel()
        {
            throw new NotImplementedException();
        }

        public bool TryFinsh()
        {
            throw new NotImplementedException();
        }

        public bool TryStart()
        {
            throw new NotImplementedException();
        }

        public SimpleTime WillFinishAt()
        {
            throw new NotImplementedException();
        }
    }
}
