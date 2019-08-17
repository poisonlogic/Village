using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map.Internal
{
    internal class MapLayer : IMapLayer
    {
        public int MaxWidth => Controller.MaxWidth;
        public int MaxHeight => Controller.MaxHeight;
        public int MinWidth => Controller.MinWidth;
        public int MinHeight => Controller.MinHeight;
        private bool _dirty;
        private IMapTile[,] _tiles;

        public string LayerName { get; }
        public IMapController Controller { get; }

        public IEnumerable<IMapTile> Tiles()
        {
            for (int y = MinHeight; y < MaxHeight; y++)
                for (int x = MinWidth; x < MaxWidth; x++)
                    yield return _tiles[x - MinWidth, y - MinHeight];
        }

        public MapLayer(string layerName, IMapController controller, IMapTile[,] tiles)
        {
            Controller = controller;
            LayerName = layerName;
            _tiles = tiles;
        }

        public IMapTile GetTileAt(int x, int y)
        {
            if (!IsValidPosition(x, y))
                return null;

            else
                return _tiles[x,y];
        }

        public bool IsTileFree(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool IsValidPosition(int x, int y)
        {
            if (Controller == null)
                throw new Exception($"MapLayer {LayerName} has null controller");

            if (x < 0 || x >= Controller.MaxWidth)
                return false;

            if (y < 0 || y >= Controller.MaxHeight)
                return false;

            return true;
        }
    }
}
