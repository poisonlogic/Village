using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;

namespace Village.Core.Buildings.Internal
{
    internal class BuildingController : IBuildingController
    {
        private IMapController _mapController;
        private Dictionary<string, BuildingDef> _defs;
        private Dictionary<string, IBuilding> _buildings;

        public List<IBuilding> AllBuildings => throw new NotImplementedException();

        public BuildingController(IMapController mapController)
        {
            _mapController = mapController ?? throw new ArgumentNullException(nameof(mapController));

            _defs = DefLoader.LoadDefCatalog<BuildingDef>("Village.Core.Buildings.Defs.BuildingDefs.json");
            _buildings = new Dictionary<string, IBuilding>();
        }

        public bool LoadBuildings()
        {


            return true;
        }

        public bool AddBuilding(MapSpot spot, string defName)
        {
            var def = _defs[defName];
            var building = DefLoader.CreateInstanct<IBuilding>(def, "GROUND", spot, _mapController, MapRotation.Default);
            _mapController.AddMapStruct("GROUND", building);
            return true;
        }
    }
}
