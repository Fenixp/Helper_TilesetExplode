#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using CommonLib.Common;
#endregion

namespace Helper_TilesetExplode
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public string[] args;
        const int WINXSIZE = 700;
        const int WINYSIZE = 600;
        const float SHIFTSPEED = 5;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Vector2 cursorPosition;
        List<Tuple<string, TileInfo, Vector2, Vector2>> gameObjects;
        float lineSize = 0;
        float shiftX = 0;
        float shiftY = 0;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ContentSettings.Content = Content;
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
            graphics.PreferredBackBufferWidth = WINXSIZE;
            graphics.PreferredBackBufferHeight = WINYSIZE;
            graphics.ApplyChanges();
            gameObjects = new List<Tuple<string, TileInfo, Vector2, Vector2>>();
            base.Initialize();
            cursorPosition = new Vector2(0, 0);
            foreach (string str in args)
            {
                foreach (KeyValuePair<string, TileInfo> info in TilesetReader.GetSprites(str))
                {
                    ReadTileInfo(info);
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            //List<TileInfo> anim = TilesetReader.GetAnimation("ChomperSprites", "YellowRight");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            ReadInput();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (args.Length == 0)
            {
                spriteBatch.DrawString(font, "Make sure to start the application with at least a single parameter containing tileset name", new Vector2(0, 0), Color.White);
            }
            else
            {
                foreach (Tuple<string, TileInfo, Vector2, Vector2> obj in gameObjects)
                {
                    spriteBatch.Draw(obj.Item2.Texture, new Vector2(obj.Item3.X + shiftX, obj.Item3.Y + shiftY), obj.Item2.Rectangle, Color.White);
                    spriteBatch.DrawString(font, obj.Item1, new Vector2(obj.Item4.X + shiftX, obj.Item4.Y + shiftY), Color.White);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ReadInput()
        {
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                shiftY -= SHIFTSPEED;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                shiftY += SHIFTSPEED;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                shiftX += SHIFTSPEED;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                shiftX -= SHIFTSPEED;
            }
        }

        private void ReadTileInfo(KeyValuePair<string, TileInfo> info)
        {
            Vector2 stringSize = font.MeasureString(info.Key);

            if (cursorPosition.X + stringSize.X > WINXSIZE || cursorPosition.X + info.Value.Rectangle.Width > WINXSIZE)
            {
                cursorPosition.X = 0;
                cursorPosition.Y += lineSize + 10;
                lineSize = 0;
            }

            gameObjects.Add(new Tuple<string, TileInfo, Vector2, Vector2>(
                    info.Key,
                    info.Value,
                    cursorPosition,
                    new Vector2(cursorPosition.X, cursorPosition.Y + info.Value.Rectangle.Height + 5)
                ));

            if (lineSize < stringSize.Y + info.Value.Rectangle.Height + 5)
            {
                lineSize = stringSize.Y + info.Value.Rectangle.Height + 5;
            }

            if (stringSize.X < info.Value.Rectangle.Width)
            {
                cursorPosition.X += (info.Value.Rectangle.Height + 10);
            }
            else
            {
                cursorPosition.X += (stringSize.X + 10);
            }
        }
    }
}
