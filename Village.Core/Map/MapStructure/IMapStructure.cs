using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map.MapStructure
{
    public interface IMapStructure
    {
        string Id { get; }
        string MapLayerName { get; }
        bool FillMapSpots { get; }
        MapSpot Anchor { get; }
        MapRotation Rotation { get; }
        IEnumerable<MapSpot> MapSpots { get; }
        IEnumerable<MapStructSide> GetOccupiedSides(MapSpot spot);
        IMapController MapController { get; }
    }
}
