using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;

namespace Village.Core.Buildings.Defs
{
    public class FruitTree : BaseBuilding, IBuilding
    {
        public FruitTree( BuildingDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) : base(layerName, def, anchor, controller, rotation)
        {

        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
