using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map
{
    public interface IMapLayer
    {
        int MaxWidth { get; }
        int MaxHeight { get; }
        int MinWidth { get; }
        int MinHeight { get; }
        string LayerName { get; }
        IMapController Controller { get; }
        
        IEnumerable<IMapTile> Tiles();
        IMapTile GetTileAt(int x, int y);
        IMapTile GetTileAt(MapSpot spot);
        IEnumerable<IMapTile> GetTiles(IEnumerable<MapSpot> mapSpots);

        bool AreSpotsClear(IEnumerable<MapSpot> spots);
        bool IsValidPosition(int x, int y);
        bool IsValidPosition(MapSpot spot);
        bool IsTileFree(int x, int y);
    }
}
