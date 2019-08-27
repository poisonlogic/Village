using System;
using System.Collections.Generic;
using System.Linq;
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
            if (_tiles == null)
                throw new Exception($"Tiles are null for layer '{LayerName}'");

            for (int y = MinHeight; y < MaxHeight; y++)
                for (int x = MinWidth; x < MaxWidth; x++)
                    yield return _tiles[x - MinWidth, y - MinHeight];
        }

        public MapLayer(string layerName, IMapController controller, IMapTile[,] mapTiles)
        {
            Controller = controller ?? throw new ArgumentNullException(nameof(controller));
            LayerName = layerName;
            _tiles = mapTiles ?? throw new ArgumentNullException(nameof(mapTiles));
        }

        public IMapTile GetTileAt(int x, int y)
        {
            if (!IsValidPosition(x, y))
                return null;

            else
                return _tiles[x - MinWidth, y - MinHeight];
        }

        public IMapTile GetTileAt(MapSpot spot)
        {
            return GetTileAt(spot.X, spot.Y);
        }

        public IEnumerable<IMapTile> GetTiles(IEnumerable<MapSpot> mapSpots)
        {
            var outList = new List<IMapTile>();
            foreach (var spot in mapSpots)
                outList.Add(GetTileAt(spot));
            return outList;
        }

        public bool IsSpotFree(MapSpot spot)
        {
            return IsTileFree(spot.X, spot.Y);
        }

        public bool IsTileFree(int x, int y)
        {
            return IsValidPosition(x, y);
        }

        public bool IsValidPosition(int x, int y)
        {
            if (Controller == null)
                throw new Exception($"MapLayer {LayerName} has null controller");

            if (x < MinWidth || x >= MaxWidth)
                return false;

            if (y < MinWidth || y >= MaxHeight)
                return false;

            return true;
        }

        public bool IsValidPosition(MapSpot spot)
        {
            return IsValidPosition(spot.X, spot.Y);
        }

        public bool AreSpotsClear(IEnumerable<MapSpot> spots)
        {
            return spots.Where(x => !IsSpotFree(x)).Any();
        }
    }
}
