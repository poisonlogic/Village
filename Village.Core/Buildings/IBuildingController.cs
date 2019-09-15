using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;

namespace Village.Core.Buildings
{
    public interface IBuildingController : IController
    {
        List<IBuilding> AllBuildings { get; }
        bool LoadBuildings();
        bool TryAddBuilding(MapSpot spot, string defName);
        void Update();
    }
}
