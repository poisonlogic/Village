using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Village.Map
{
    public class VillageMap
    {
        public Guid InstanceId { get; }
        public string Label { get; }

        public int Width { get; }
        public int Height { get; }
        public IEnumerable<Tile> Tiles { get { return _tiles; } }

        private List<Tile> _tiles;

        public VillageMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            _tiles = Enumerable.Range(0, Width * Height).Select(x => new Tile(x, this)).ToList();
        }

        public Tile this[int x, int y]
        {
            get { return this._tiles[y * Width + x]; }
            set { this._tiles[y * Width + x] = value; }
        }

        public bool OnMap(int x, int y)
        {
            return (x < Width && x >= 0) && (y < Height && y >= 0);
        }
    }
}
