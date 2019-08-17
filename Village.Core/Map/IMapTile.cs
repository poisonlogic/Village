using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map
{
    public interface IMapTile
    {
        TileType TileType { get; }
        int X { get; }
        int Y { get; }
        MapSpot MapSpot { get; }
    }
}
