
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Village.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Map.MapStructures;

namespace DeskTopVillage.GameClasses
{
    public static class MapRenderer
    {
        public static int Tile_Width = 64;
        public static int Tile_Height = 64;
        public static int Scaled_Width { get { return (int)(Tile_Width * Zoom); } }
        public static int Scaled_Height { get { return (int)(Tile_Height * Zoom); } }
        
        public static decimal Zoom = 1;

        private static Dictionary<Tile, Texture2D> _tileGraphics;
        private static Texture2D defaultGraphic;
        private static Game CurrentGame;
        private static SpriteFont SpriteFont;

        private static int xOffset;
        private static int yOffset;
        private static float currentZoom;

        public static void Init(VillageMap map, Game game)
        {
            Map = map;
            CurrentGame = game;
            _tileGraphics = new Dictionary<Tile, Texture2D>();

            MapStructManager = new MapStructManager<MapStructDef>(Map);


            var defs = Village.Core.Loader.DefLoader.LoadDefs<MapStructDef>("C:/temp");
            var mapStruc = new BaseMapStructInstance<MapStructDef>(MapStructManager, Map, defs.First(), 2, 1);
            mapStruc.Def.SpriteDetails = new Village.Core.SpriteDetails
            {
                SpriteHeight = 2,
                SpriteWidth = 2,
                SpriteOffsetX = 0,
                SpriteOffsetY = -1,
                SpriteName = "house"
            };
            MapStructManager.TryAddStructure(mapStruc);
            DebugDef = defs.First();
            MapStructManager.TryAddStructure(new BaseMapStructInstance<MapStructDef>(MapStructManager, Map, defs.Last(), 8, 3));


            foreach (var tile in Map.Tiles)
            {
                if (defaultGraphic == null)
                    defaultGraphic = MakeTileGraphic(tile);
                _tileGraphics.Add(tile, defaultGraphic);
            }
        }

        internal static void TryZoom(double v) { TryZoom((decimal)v); }

        public static void TryScroll(int xChange, int yChange)
        {

            xOffset += xChange;
            //if (xOffset < 0)
            //    xOffset = 0;
            yOffset += yChange;
            //if (yOffset < 0)
            //    yOffset = 0;
        }

        public static void TryZoom(decimal zoom)
        {
            Zoom += zoom;
        }

        private static Texture2D MakeTileGraphic(Tile tile)
        {
            Texture2D tex = new Texture2D(CurrentGame.GraphicsDevice, Tile_Width, Tile_Height);

            Color[] data = new Color[Tile_Height * Tile_Width];
            for(int y = 0; y < Tile_Height; y++)
                for (int x = 0; x < Tile_Width; x++)
                {
                    var index = y * Tile_Height + x;
                    if (x < 5 || x > Tile_Width - 5)
                        data[index] = Color.Black;

                    else if (y < 5 || y > Tile_Height - 5)
                        data[index] = Color.Black;
                    else
                        data[index] = Color.Wheat;
                }
            tex.SetData(data);

            return tex;
        }

        public static void DrawMap(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            if (SpriteFont == null)
                SpriteFont = CurrentGame.Content.Load<SpriteFont>("8Bit");
            
            spriteBatch.Begin();
            foreach (var tile in _tileGraphics)
            {
                var coor = new Vector2(tile.Key.X * Scaled_Width + xOffset, tile.Key.Y * Scaled_Height + yOffset);
                var dest = new Rectangle((int)coor.X, (int)coor.Y, Scaled_Width, Scaled_Height);
                var rect = tile.Value;

                var color = Color.Wheat;



                var mapStuc = MapStructManager.StructureAt(tile.Key.X, tile.Key.Y);
                if (mapStuc != null && mapStuc.XAnchor == tile.Key.X && mapStuc.YAnchor == tile.Key.Y)
                {
                    var scaledDest = new Rectangle(
                        dest.X + (mapStuc.MapStructDef.SpriteDetails.SpriteOffsetX * Tile_Width), 
                        dest.Y + (mapStuc.MapStructDef.SpriteDetails.SpriteOffsetY * Tile_Height),
                        dest.Width * mapStuc.MapStructDef.SpriteDetails.SpriteWidth, 
                        dest.Height * mapStuc.MapStructDef.SpriteDetails.SpriteHeight);
                    spriteBatch.Draw(Game1.texts["house"], scaledDest, color);
                }
                else if(mapStuc != null)
                {

                }
                else
                {

                    spriteBatch.Draw(rect, dest, color); ;
                    spriteBatch.DrawString(SpriteFont, string.Format("({0},{1})", tile.Key.X, tile.Key.Y), coor + new Vector2(10, 10), Color.Black);

                }


                if (DebugTool.CurrentX == tile.Key.X && DebugTool.CurrentY == tile.Key.Y)
                {
                    var text = DebugTool.createCircleText(Tile_Width / 2, graphics.GraphicsDevice);
                    if (mapStuc == null)
                        spriteBatch.Draw(text, dest, Color.Gray);
                    else
                        spriteBatch.Draw(text, dest, Color.Red);
                }
            }
            wasLast++;
            if (wasLast > 100)
                wasLast = -100;
            spriteBatch.End();
        }

        public static int wasLast;

        public static void MakeNewStructAtLocation(int x, int y)
        {
            var mapStruc = new BaseMapStructInstance<MapStructDef>(MapStructManager, Map, DebugDef, x, y);
            mapStruc.Def.SpriteDetails = new Village.Core.SpriteDetails
            {
                SpriteHeight = 2,
                SpriteWidth = 2,
                SpriteOffsetX = 0,
                SpriteOffsetY = -1,
                SpriteName = "house"
            };
            MapStructManager.TryAddStructure(mapStruc);
        }
    }
}
