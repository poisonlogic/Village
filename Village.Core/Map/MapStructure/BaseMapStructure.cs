using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Buildings;
using Village.Core.Map.Internal;
using Village.Core.Rendering;

namespace Village.Core.Map.MapStructure
{
    public abstract class BaseMapStructure : Inst, IMapStructure
    {
        private MapStructDef _def;
        private Dictionary<Tuple<int, int>, MapSpot> _rotationMapping;

        MapStructDef IMapStructure.MapStructDef => _def;
        public string MapLayerName { get; }
        public IMapController MapController { get; }
        public MapRotation Rotation { get; }
        public MapSpot Anchor { get; }
        public IEnumerable<MapSpot> MapSpots => _rotationMapping.Values;
        public bool IsFloorCover => throw new NotImplementedException();
        public bool FillMapSpots => _def.FillMapSpots;

        public BaseMapStructure(MapStructDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) : base(def)
        {
            MapController = controller ?? throw new ArgumentNullException(nameof(controller));
            Anchor = anchor ?? throw new ArgumentNullException(nameof(anchor));
            MapLayerName = layerName;

            _def = def ?? throw new ArgumentNullException(nameof(def));
            MapStructValidator.ValidateDef(def);
            _rotationMapping = MapStructHelper.FootprintToMapSpotsDictionary(def.Footprint, rotation, anchor);
        }

        public IEnumerable<MapStructSide> GetOccupiedSides(MapSpot spot)
        {
            if (!MapSpots.Any(s => spot.X == s.X && spot.Y == s.Y))
                return null;

            var print = _rotationMapping.Where(p => p.Value.X == spot.X && p.Value.Y == spot.Y).SingleOrDefault().Key;
            if (print == null)
                return null;

            var sides = _def.OccupiesSides[print];
            if (sides == null)
                return null;

            var rotated = MapStructHelper.RotateOccupiedSides(sides, Rotation).ToList();
            return rotated;
        }

        public abstract string GetSprite();
    }
}
