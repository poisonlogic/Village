
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Village.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTopVillage.GameClasses
{
    public static class MapRenderer
    {
        public static int Tile_Width = 64;
        public static int Tile_Height = 64;
        public static int Scaled_Width { get { return (int)(Tile_Width * Zoom); } }
        public static int Scaled_Height { get { return (int)(Tile_Height * Zoom); } }

        public static decimal Zoom = 1;
        public static VillageMap Map;


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
                spriteBatch.Draw(rect, dest, Color.White);;
                spriteBatch.DrawString(SpriteFont, string.Format("({0},{1})", tile.Key.X, tile.Key.Y), coor + new Vector2(10,10), Color.Black);
            }
            spriteBatch.End();
        }
    }
}
