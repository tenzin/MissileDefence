using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence.MacOS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D landScape, launchPad;
        City city1, city2, city3, city4;
        Missile missile;
        KeyboardState keyState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            keyState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            landScape = Content.Load<Texture2D>("Images/LandScape");
            launchPad = Content.Load<Texture2D>("Images/LaunchPad");
            Texture2D c1 = Content.Load<Texture2D>("Images/City1");
            Texture2D c2 = Content.Load<Texture2D>("Images/City2");
            Texture2D c3 = Content.Load<Texture2D>("Images/City3");
            Texture2D c4 = Content.Load<Texture2D>("Images/City4");
            Texture2D missileTexture = Content.Load<Texture2D>("Images/Missile");
            city1 = new City(c1, 100, 430);
            city2 = new City(c2, 200, 430);
            city3 = new City(c3, 500, 430);
            city4 = new City(c4, 600, 430);
            missile = new Missile(missileTexture, new Vector2(400, 470));


            //TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyState.IsKeyDown(Keys.Escape))
                Exit();

            missile.Update(keyState);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            //TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(landScape, new Vector2(0, GraphicsDevice.Viewport.Height - landScape.Height), Color.White);
            spriteBatch.Draw(launchPad, new Vector2(370, 450), Color.White);
            spriteBatch.End();
            city1.Draw(spriteBatch);
            city2.Draw(spriteBatch);
            city3.Draw(spriteBatch);
            city4.Draw(spriteBatch);
            missile.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
