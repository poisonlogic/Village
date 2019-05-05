using Village.Map;
using Village.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;
using Village.Social.Population;
using Village.Map.MapStructures;
using Village.Social.Jobs;

namespace Village.Core
{
    public class VillageManager
    {
        public VillageMap Map { get; private set; }
        public PopulationManager PopulationManager { get; }
        public MapStructManager MapStructManager { get; }
        public JobManager JobManager { get; }

        public VillageManager()
        {
            this.Map = new VillageMap(20, 20);
            PopulationManager = new PopulationManager();
            MapStructManager = new MapStructManager(Map);
            JobManager = new JobManager();
            //this.ResourceManager = new ResourceMaster();
        }

        public bool TryAddVillager(Villager newGuy)
        {

        }
    }
}
