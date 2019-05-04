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

namespace Village.Core
{
    public class VillageManager
    {
        public VillageMap Map { get; private set; }
        public PopulationManager PopulationManager { get; private set; }
        public MapStructManager MapStructManager { get; private set; }


        public VillageManager()
        {
            this.Map = new VillageMap(20, 20);
            PopulationManager = new PopulationManager();
            MapStructManager = new MapStructManager(Map);
            //this.ResourceManager = new ResourceMaster();
        }


    }
}
