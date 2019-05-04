using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Map.MapStructures
{
    public class MapStructManager
    {
        private Dictionary<string, IMapStructInstance> _mapStructs;
        private string[,] _cachedRefMap;

        public IEnumerable<IMapStructInstance> AllMapStructs { get { return _mapStructs.Select(x => x.Value); } }
        public IMapStructUser TileMap { get; private set; }

        public MapStructManager(IMapStructUser tileMap)
        {
            TileMap = tileMap;
            _cachedRefMap = new string[tileMap.Width,tileMap.Height];
            _mapStructs = new Dictionary<string, IMapStructInstance>();
        }

        public IMapStructInstance StructureAt(int x, int y)
        {
            var id = _cachedRefMap[x, y];
            if (id == null)
                return null;
            else
                return _mapStructs[id];
        }

        public bool CanAddInstance(IMapStructInstance mapStruct)
        {
            var footPrint = mapStruct.GetFootprint();

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

        public bool TryAddStructure(IMapStructInstance mapStruct)
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
    }
}
