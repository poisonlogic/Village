using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Village.Core;
using Village.Core.Map;
using Village.DesktopApp.Classes;

namespace Village.DesktopApp
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWindow : Game
    {
        Logger _logger;
        double lastTick;
        public static GameWindow Instance { get; private set; }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MapRenderer MapRenderer;
        GameMaster _gameMaster;
        SpriteFont font;
        Texture2D GrassText;
        
        public GameWindow(GameMaster gameMaster)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 640;
            graphics.PreferredBackBufferWidth = 640;

            MapRenderer = new MapRenderer();
            Content.RootDirectory = "Content";
            _gameMaster = gameMaster;
            if (Instance != null)
                throw new System.Exception();
            Instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            GameMaster.Instance.Init();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            MapRenderer.LoadContent(Content);
            font = Content.Load<SpriteFont>("DefaultFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (gameTime.TotalGameTime.TotalMilliseconds > lastTick + GameMaster.MillsPerTick)
            {
                lastTick = gameTime.TotalGameTime.TotalMilliseconds;
                GameMaster.Instance.Update();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();



            var map = GameMaster.Instance.GetController<IMapController>();
            MapRenderer.DrawMap(map, spriteBatch);

            //Draw Time
            var box = new Texture2D(graphics.GraphicsDevice, 640, 30);
            Color[] data = new Color[640 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            box.SetData(data);
            spriteBatch.Draw(box, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, GameMaster.Instance.TimeKeeper.Print("[HOUR]:[MIN]:[SEC] [WEEK] the [DAY]th, [SEAS], [YEAR]") , new Vector2(0, 0), Color.White);

            //Draw Log
            (GameMaster.Instance._logger as Logger).DrawLog(spriteBatch, graphics.GraphicsDevice, font);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void DrawMap(SpriteBatch spriteBatch, IMapController mapController)
        {
            
        }
    }
}
