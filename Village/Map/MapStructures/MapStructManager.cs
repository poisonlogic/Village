using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.DIMCUP;

namespace Village.Map.MapStructures
{
    public class MapStructManager<TDef> : BaseDimManager<TDef> where TDef : MapStructDef
    {
        private Dictionary<string, IMapStructInstance<TDef>> _mapStructs;
        private string[,] _cachedRefMap;

        public override Type TypeOfInstances => typeof(BaseMapStructInstance<TDef>);
        public override Type TypeOfUsers => null;
        public override Type TypeOfProviders => typeof(BaseMapStructInstance<TDef>);

        public IEnumerable<IMapStructInstance<TDef>> AllMapStructs => _mapStructs.Select(x => x.Value);
        public IMapStructProvider<TDef> TileMap { get; private set; }

        public MapStructManager(IMapStructProvider<TDef> tileMap)
        {
            TileMap = tileMap;
            _cachedRefMap = new string[tileMap.Width,tileMap.Height];
            _mapStructs = new Dictionary<string, IMapStructInstance<TDef>>();
        }

        public IMapStructInstance<TDef> StructureAt(int x, int y)
        {
            var id = _cachedRefMap[x, y];
            if (id == null)
                return null;
            else
                return _mapStructs[id];
        }

        public bool CanAddInstance(IMapStructInstance<TDef> instance)
        {
            var footPrint = instance.GetFootprint();

            foreach (var print in footPrint)
            {
                var x = print[0];
                var y = print[1];

                if (_cachedRefMap[x, y] != null)
                    return false;
                if (!TileMap.IsOpenToMapStructure(x, y))
                    return false;
            }
            return true;
        }

        public bool TryAddStructure(IMapStructInstance<TDef> mapStruct)
        {
            if (!CanAddInstance(mapStruct))
                return false;
            
            var footPrint = mapStruct.GetFootprint();
            _mapStructs.Add(mapStruct.InstanceId, mapStruct);
            foreach (var print in footPrint)
            {
                var x = print[0];
                var y = print[1];
                _cachedRefMap[x, y] = mapStruct.InstanceId;
            }

            return true;
        }

        private void RecachRefMap()
        {
            _cachedRefMap = new string[TileMap.Width, TileMap.Height];

            foreach(var struc in _mapStructs)
            {
                var footprint = struc.Value.GetFootprint();
                foreach(var print in footprint)
                {
                    var x = print[0]; var y = print[1];
                    _cachedRefMap[x, y] = struc.Key;
                }
            }
        }

        public override bool TryTransferInstance(IDimInstance<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfInstanceChange(IDimInstance<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfUserChange(IDimUser<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfProviderChange(IDimProvider<TDef> instance)
        {
            throw new NotImplementedException();
        }
    }
}
