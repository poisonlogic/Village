using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map.Internal
{
    internal class MapController : IMapController
    {
        public IMapRenderer Renderer { get; }

        public MapConfig Config;
        public Dictionary<string, IMapLayer> Layers { get; }

        public int MaxWidth => Config.MaxWidth;
        public int MaxHeight => Config.MaxHeight;
        public int MinWidth => Config.MinWidth;
        public int MinHeight => Config.MinHeight;

        public MapController(IMapRenderer renderer)
        {
            Renderer = renderer ?? throw new ArgumentException(nameof(renderer));
            Config = ConfigLoader.LoadConfig<MapConfig>("Village.Core.Map.Internal.MapConfig.json");

            Layers = new Dictionary<string, IMapLayer>();
            BuildMap();
        }
        
        private void BuildMap()
        {
            var ran = new Random();
            var tiles = new MapTile[MaxWidth - MinWidth, MaxHeight - MinWidth];
            for (int x = MinWidth; x < MaxHeight; x++)
                for (int y = MinHeight; y < MaxHeight; y++)
                    tiles[x - MinWidth, y - MinHeight] = new MapTile(x, y, ran.Next(2) > 0 ? TileType.Grass : TileType.Water);

            Layers.Add("GROUND", new MapLayer("GROUND", this, tiles));
        }


        public IMapLayer GetLayer(string LayerName)
        {
            return Layers[LayerName];
        }

        public void CreateEmptyLayer(string LayerName)
        {
            throw new NotImplementedException();
        }

        public void AddMapObject(IMapObject mapObject)
        {
            throw new NotImplementedException();
        }

        public void RemoveMapObject(IMapObject mapObject)
        {
            throw new NotImplementedException();
        }
    }
}
