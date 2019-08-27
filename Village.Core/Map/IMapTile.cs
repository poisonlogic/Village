using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map.MapStructure;

namespace Village.Core.Map
{
    public interface IMapTile
    {
        IMapController Controller { get; }
        string LayerName { get; }
        TileType TileType { get; }
        int X { get; }
        int Y { get; }
        MapSpot MapSpot { get; }

        IEnumerable<string> MapStructs { get; }
        void AddMapStruct(string id);
        void RemoveStruct(string id);
        void ClearStructs(string id);
    }
}
