using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map.Internal
{
    public class MapTile : IMapTile
    {
        public TileType TileType { get; }
        public int X { get; }
        public int Y { get; }
        public MapSpot MapSpot => new MapSpot(X, Y); 


        public MapTile(int x, int y, TileType tileType)
        {
            TileType = tileType;
            this.X = x;
            this.Y = y;
        }
    }
}
