using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core;
using Village.Core.Buildings;
using Village.Core.Map;
using Village.Core.Rendering;
using Village.Core.Time;

namespace Village.DesktopApp.Classes
{
    public class MapRenderer : IMapRenderer
    {

        private Guid Id = new Guid();
        public static int TileSize = 64;
        public GameMaster GameMaster => GameMaster.Instance;
        public Dictionary<TileType, Texture2D> TileToSprite;
        public Dictionary<string, Texture2D> Sprites;

        public MapRenderer()
        {
            TileToSprite = new Dictionary<TileType, Texture2D>();
            Sprites = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(ContentManager contentManager)
        {

            TileToSprite.Add(TileType.Grass, contentManager.Load<Texture2D>("Grass"));
            TileToSprite.Add(TileType.Water, contentManager.Load<Texture2D>("Water"));
            TileToSprite.Add(TileType.DirtXXX, contentManager.Load<Texture2D>("Error"));

            var buildingControler = GameMaster.Instance.GetController<IBuildingController>();
            foreach (var def in buildingControler.AllDefs)
                if(def.Sprites?.Any() ?? false)
                foreach (var map in def.Sprites)
                    Sprites.Add(def.DefName + map, contentManager.Load<Texture2D>(map));
        }

        public void DrawLayer(SpriteBatch spriteBatch, IMapLayer layer)
        {
            int x = 0;
            int y = 0;
            foreach (var tile in layer.Tiles())
            {
                var mapStructs = layer.Controller.GetMapStructsAt(layer.LayerName, tile.MapSpot);
                Texture2D tileText = null;


                Color grassColor = Color.White;
                if (Console.BackgroundColor == ConsoleColor.Green)
                {
                    var time = GameMaster.GetController<ITimeKeeper>();
                    var season = time.Time.GetValue("SEAS");
                    if (season == 0)
                        grassColor = Color.White;
                    if (season == 1)
                        grassColor = Color.Yellow;
                    if (season == 2)
                        grassColor = Color.Red;
                    if (season == 3)
                        grassColor = Color.White;
                }
                spriteBatch.Draw(TileToSprite[tile.TileType], new Vector2(x, y), tile.TileType == TileType.Grass ? grassColor : Color.White);

                if (mapStructs.Any())
                {

                    var sprite = mapStructs.Single().GetSprite();

                    if (string.IsNullOrEmpty(sprite))
                    {
                        tileText = TileToSprite[TileType.DirtXXX];
                    }
                    else
                    {
                        tileText = Sprites[sprite];
                    }
                }

                if(tileText != null)
                spriteBatch.Draw(tileText, new Vector2(x, y), Color.White);


                x = x + TileSize;
                if (tile.X == layer.MaxWidth - 1)
                {
                    x = 0;
                    y = y + TileSize;
                }
            }
        }

        public void DrawMap(SpriteBatch spriteBatch, IMapController controller)
        {
            DrawLayer(spriteBatch, controller.GetLayer("GROUND"));
        }

        public void DrawMap(IMapController controller, object args)
        {
            var batch = args as SpriteBatch;
            DrawMap(batch, controller);
        }

        public void DrawLayer(IMapLayer layer, object args)
        {
            throw new NotImplementedException();
        }
    }
}
