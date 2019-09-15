using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;

namespace Village.Core.Buildings.Internal
{
    internal class BuildingController : IBuildingController
    {
        private IMapController _mapController => GameMaster.Instance.GetController<IMapController>();
        private Dictionary<string, BuildingDef> _defs;
        private Dictionary<string, IBuilding> _buildings;

        public List<IBuilding> AllBuildings => throw new NotImplementedException();

        public BuildingController()
        {
            _defs = DefLoader.LoadDefCatalog<BuildingDef>("Village.Core.Buildings.Defs.BuildingDefs.json");
            _buildings = new Dictionary<string, IBuilding>();
        }

        public bool LoadBuildings()
        {


            return true;
        }

        public bool TryAddBuilding(MapSpot anchor, string defName)
        {
            var def = _defs[defName];
            var building = DefLoader.CreateInstanct<IBuilding>(def, "GROUND", anchor, _mapController, MapRotation.Default);
            if (_mapController.CanAddMapStructure(building.MapLayerName, building.MapStructDef, anchor, building.Rotation))
            {
                _mapController.AddMapStructure(building);
                _buildings.Add(building.Id, building);
            }
            return true;
        }

        public void Update()
        {
            foreach (var building in _buildings.Values)
                building.Update();
        }
    }
}
