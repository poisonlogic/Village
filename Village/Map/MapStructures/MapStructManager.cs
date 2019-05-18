using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.DIMCUP;

namespace Village.Map.MapStructures
{
    public class MapStructManager<TDef> : BaseDimcupManager<TDef> where TDef : MapStructDef
    {
        private Dictionary<string, IMapStructInstance<TDef>> _mapStructs;
        private string[,] _cachedRefMap;

        public IEnumerable<IMapStructInstance<TDef>> AllMapStructs { get { return _mapStructs.Select(x => x.Value); } }
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

        public override bool TryTransferInstance(IDimcupInstance<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfInstanceChange(IDimcupInstance<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfUserChange(IDimcupUser<TDef> instance)
        {
            throw new NotImplementedException();
        }

        public override void InformOfProviderChange(IDimcupProvider<TDef> instance)
        {
            throw new NotImplementedException();
        }
    }
}
