using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Map;

namespace Village.ConsoleApp.Classes
{
    public class MapRenderer : IMapRenderer
    {
        public Dictionary<TileType, ConsoleColor> TileToColor;

        public MapRenderer()
        {
            TileToColor = new Dictionary<TileType, ConsoleColor>();
            //TileToColor.Add(TileType.Dirt, ConsoleColor.DarkRed);
            TileToColor.Add(TileType.Grass, ConsoleColor.Green);
            TileToColor.Add(TileType.Water, ConsoleColor.Blue);
        }

        public void DrawLayer(IMapLayer layer)
        {
            foreach(var tile in layer.Tiles())
            {
                if(tile != null)
                    Console.BackgroundColor = TileToColor[tile.TileType];
                else
                    Console.BackgroundColor = ConsoleColor.Cyan;

                if (tile.MapStructs.Any())
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("XX");
                }
                else
                    Console.Write("  ");
                if (tile.X == layer.MaxWidth - 1)
                    Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DrawMap(IMapController controller)
        {
            DrawLayer(controller.GetLayer("GROUND"));
        }
    }
}
