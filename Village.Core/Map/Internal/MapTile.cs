using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Map.MapStructure;

namespace Village.Core.Map.Internal
{
    public class MapTile : IMapTile
    {
        private List<string> _mapStructs;

        public IMapController Controller { get; }
        public string LayerName { get; }
        public TileType TileType { get; }
        public int X { get; }
        public int Y { get; }
        public MapSpot MapSpot => new MapSpot(X, Y);

        public IEnumerable<string> MapStructs => _mapStructs;

        public MapTile(int x, int y, TileType tileType, string layerName)
        {
            LayerName = layerName;
            TileType = tileType;
            this.X = x;
            this.Y = y;
            _mapStructs = new List<string>();
        }

        public void AddMapStruct(string id)
        {
            _mapStructs.Add(id);
        }

        public void RemoveStruct(string id)
        {
            _mapStructs.Remove(id);
        }

        public void ClearStructs(string id)
        {
            _mapStructs.Clear();
        }
    }
}
