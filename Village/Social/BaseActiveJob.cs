using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;

namespace Village.Social
{
    public class BaseActiveJob : IActiveJob
    {
        public Job Job { get; private set; }
        public List<Villager> Workers { get; private set; }
        public IBuilding Building { get; private set; }

        public bool CanAddWorker(Villager worker)
        {
            if (worker.EducationLevel < Job.RequiredEducationLevel)
                return false;
            return true;
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
    }
}
