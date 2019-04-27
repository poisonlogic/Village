using Village.Map;
using Village.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;

namespace Village.Core
{
    public class VillageManager
    {
        public VillageMap Map { get; private set; }
        public ResourceMaster ResourceManager { get; private set; }
        public List<IBuilding> Buildings { get; private set; }

        public void Init()
        {
            this.Map = new VillageMap(20, 20);
            this.ResourceManager = new ResourceMaster();
        }

        public void RegisterBuilding(IBuilding building)
        {
            if (!Buildings.Contains(building))
                Buildings.Add(building);

            if (building is IProducer)
                ResourceManager.TryRegiseringProducer(building as IProducer);

            if (building is IConsumer)
                ResourceManager.TryRegiseringConsumer(building as IConsumer);
        }
    }
}
