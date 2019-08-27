using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Map.MapStructure;

namespace Village.Core.Map.Internal
{
    internal class MapController : IMapController
    {
        private Dictionary<string, IMapStructure> _mapStructs;
        private Dictionary<string, IMapLayer> _layers;

        public IMapRenderer Renderer { get; }
        public MapConfig Config;

        public int MaxWidth => Config.MaxWidth;
        public int MaxHeight => Config.MaxHeight;
        public int MinWidth => Config.MinWidth;
        public int MinHeight => Config.MinHeight;

        public MapController(IMapRenderer renderer)
        {
            Renderer = renderer ?? throw new ArgumentException(nameof(renderer));
            Config = ConfigLoader.LoadConfig<MapConfig>("Village.Core.Map.Internal.MapConfig.json");

            _layers = new Dictionary<string, IMapLayer>();
            _mapStructs = new Dictionary<string, IMapStructure>();
            BuildMap();
        }
        
        private void BuildMap()
        {
            var ran = new Random();
            var tiles = new MapTile[MaxWidth - MinWidth, MaxHeight - MinWidth];
            for (int x = MinWidth; x < MaxHeight; x++)
                for (int y = MinHeight; y < MaxHeight; y++)
                    tiles[x - MinWidth, y - MinHeight] = new MapTile(x, y, ran.Next(2) > 0 ? TileType.Grass : TileType.Water, "GROUND");
            var layer = new MapLayer("GROUND", this, tiles);
            _layers.Add("GROUND", layer);
        }


        public IMapLayer GetLayer(string LayerName)
        {
            if (!_layers.ContainsKey(LayerName))
                throw new Exception($"Failed to find layer by name '{LayerName}'");
            
            return _layers[LayerName];
        }

        public void CreateEmptyLayer(string LayerName)
        {
            throw new NotImplementedException();
        }

        public bool AreMapSpotsClear(IEnumerable<MapSpot> footprint, string LayerName)
        {
            var layer = GetLayer(LayerName);
            return layer.AreSpotsClear(footprint);
        }

        public bool AddMapStruct(string layerName, IMapStructure mapStruct)
        {
            var layer = GetLayer(layerName);
            if (VerifyMapStructPlacement(mapStruct, layer))
            {
                _mapStructs.Add(mapStruct.Id, mapStruct);
                foreach(var spot in mapStruct.MapSpots)
                {
                    var tile = layer.GetTileAt(spot);
                    tile.AddMapStruct(mapStruct.Id);
                }
            }
            return false;
        }

        public IEnumerable<IMapStructure> GetMapStructsAt(string layerName, MapSpot mapSpot)
        {
            foreach (var ret in GetMapStructsAt(layerName, mapSpot.X, mapSpot.Y))
                yield return ret;
        }

        public IEnumerable<IMapStructure> GetMapStructsAt(string layerName, int x, int y)
        {
            var tile = GetLayer(layerName).GetTileAt(x, y);
            foreach (var id in tile.MapStructs)
                yield return _mapStructs[id];
        }

        public void RemoveMapStruct(IMapStructure mapStruct)
        {
            var layer = _layers[mapStruct.MapLayerName];
            foreach (var spot in mapStruct.MapSpots)
            {
                var tile = layer.GetTileAt(spot);
                tile.RemoveStruct(mapStruct.Id);
            }
            _mapStructs.Remove(mapStruct.Id);
        }

        private bool VerifyMapStructPlacement(IMapStructure mapStruct, IMapLayer layer)
        {

            foreach (var spot in mapStruct.MapSpots)
            {
                if (mapStruct.FillMapSpots)
                {
                    if (GetMapStructsAt(layer.LayerName, spot).Any())
                        return false;
                }
                else
                {
                    var sides = mapStruct.GetOccupiedSides(spot);
                    var curt = GetMapStructsAt(layer.LayerName, spot).SelectMany(x => x.GetOccupiedSides(spot));

                    if (curt.Intersect(sides).Any())
                        return false;
                }
            }
            return true;
        }
    }
}
