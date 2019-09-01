using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core;
using Village.Core.Map;
using Village.Core.Rendering;
using Village.Core.Time;

namespace Village.ConsoleApp.Classes
{
    public class MapRenderer : IMapRenderer
    {
        public GameMaster GameMaster => GameMaster.Instance;
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
            foreach (var tile in layer.Tiles())
            {
                if(tile != null)
                    Console.BackgroundColor = TileToColor[tile.TileType];
                else
                    Console.BackgroundColor = ConsoleColor.Cyan;


                var text = "";
                var mapStructs = layer.Controller.GetMapStructsAt(layer.LayerName, tile.MapSpot);


                if (mapStructs.Any())
                {

                    var sprite = mapStructs.Single().GetSprite() as FakeSprite;

                    if (sprite == null)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        text = text +  "XX";
                    }
                    else
                    {

                        Console.BackgroundColor = sprite.BackColor;
                        Console.ForegroundColor = sprite.MainColor;
                        text = text + sprite.Text;
                    }
                }
                else
                    text =  text + "  ";


                if (Console.BackgroundColor == ConsoleColor.Green)
                {
                    var time = GameMaster.GetController<ITimeKeeper>();
                    var season = time.Time.GetValue("SEAS");
                    if (season == 0)
                        Console.BackgroundColor = ConsoleColor.Green;
                    if (season == 1)
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    if (season == 2)
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    if (season == 3)
                        Console.BackgroundColor = ConsoleColor.White;
                }

                Console.Write(text);
                if (tile.X == layer.MaxWidth - 1)
                {
                    Console.WriteLine("");
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DrawMap(IMapController controller)
        {
            DrawLayer(controller.GetLayer("GROUND"));
        }
    }
}
