using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Map;
using Village.Core.Map.MapStructure;

namespace Village.Core.Buildings
{
    public abstract class BaseBuilding : BaseMapStructure, IBuilding
    {
        private BuildingDef _def;
        
        public string Label => _def.Label;
        public IEnumerable<MapSpot> Footprint { get; }
        public MapSpot AnchorPoint { get; }

        public BaseBuilding(BuildingDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) : base(def, layerName, anchor, controller, rotation)
        {
            _def = def ?? throw new ArgumentNullException(nameof(def));
            AnchorPoint = anchor ?? throw new ArgumentNullException(nameof(anchor));
            

        }

        public abstract void Update();
    }
}
