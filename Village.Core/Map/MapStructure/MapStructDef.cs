using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map.MapStructure
{
    public class MapStructDef : Def
    {
        public int Width;
        public int Height;
        public bool FillMapSpots;
        public IEnumerable<int[]> Footprint;
        public Dictionary<Tuple<int, int>, List<MapStructSide>> OccupiesSides;
    }
}
