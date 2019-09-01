using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;

namespace Village.Core.Buildings
{
    public interface IBuildingController
    {
        List<IBuilding> AllBuildings { get; }
        bool LoadBuildings();
        bool AddBuilding(MapSpot spot, string defName);
        void Update();
    }
}
