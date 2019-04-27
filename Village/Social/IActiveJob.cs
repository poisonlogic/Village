using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;

namespace Village.Social
{
    public interface IActiveJob
    {
        Job Job { get; }
        List<Villager> Workers { get; }
        IBuilding Building { get; }

        bool TryAddWorker(Villager worker);
        bool CanAddWorker(Villager worker);
    }
}
