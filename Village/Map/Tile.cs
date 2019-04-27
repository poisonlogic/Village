using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Map
{
    public class Tile
    {
        public int X { get; }
        public int Y { get; }
        public int Id { get; }
        public VillageMap Map { get; }
        public IMapStructure MapStructure { get; }

        public Tile(int index, VillageMap map)
        {
            this.Map = map;
            this.Id = Id;
            this.X = index % map.Width;
            this.Y = (int)Math.Floor((decimal)index / (decimal)map.Width);
        }
    }
}
