using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence.MacOS
{
    //Static class to define a Global variable
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D backGround;
        List<City> cities;
        List<Enemy> enemies;
        Missile missile;
        KeyboardState keyState;
        SpriteFont font;

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

            //Create pixel texture
            Globals.pixel = new Texture2D(this.GraphicsDevice, 1, 1);
            Color[] colourData = new Color[1];
            colourData[0] = Color.White; //The Colour of the rectangle
            Globals.pixel.SetData<Color>(colourData);

            backGround = Content.Load<Texture2D>("Images/BackGround");

            Texture2D c1 = Content.Load<Texture2D>("Images/City1");
            Texture2D c2 = Content.Load<Texture2D>("Images/City2");
            Texture2D c3 = Content.Load<Texture2D>("Images/City3");
            Texture2D c4 = Content.Load<Texture2D>("Images/City4");
            Texture2D missileTexture = Content.Load<Texture2D>("Images/Missile");
            Texture2D enemyTexture = Content.Load<Texture2D>("Images/Threat");
            Texture2D enemyTexture2 = Content.Load<Texture2D>("Images/Threat2");
            Texture2D enemyTexture3 = Content.Load<Texture2D>("Images/Threat3");
            font = Content.Load<SpriteFont>("Fonts/Font");

            cities = new List<City>();
            cities.Add(new City(c1, new Vector2(100, 430), new Vector2(0, 0)));
            cities.Add(new City(c2, new Vector2(200, 430), new Vector2(0, 0)));
            cities.Add(new City(c3, new Vector2(500, 430), new Vector2(0, 0)));
            cities.Add(new City(c4, new Vector2(600, 430), new Vector2(0, 0)));

            missile = new Missile(missileTexture, new Vector2(395, 445), new Vector2(missileTexture.Width / 2, missileTexture.Height / 2));

            enemies = new List<Enemy>();

            enemies.Add(new Enemy(enemyTexture3, new Vector2(1, 1), new Vector2(enemyTexture3.Width / 2, enemyTexture3.Height / 2), 1));
            enemies.Add(new Enemy(enemyTexture2, new Vector2(1, 1), new Vector2(enemyTexture2.Width / 2, enemyTexture2.Height / 2), 4900));
            enemies.Add(new Enemy(enemyTexture, new Vector2(1, 1), new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 345609));

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

            //Update cities
            foreach (City city in cities)
                city.Update(keyState, gameTime, GraphicsDevice);

            //update missile and enemy rocket
            missile.Update(keyState, gameTime, GraphicsDevice);
            foreach (Enemy enemy in enemies)
                enemy.Update(keyState, gameTime, GraphicsDevice);

            CollisionDetection();

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

            spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            foreach(City city in cities)
            {
                city.Draw(spriteBatch, font);
            }
            missile.Draw(spriteBatch, font);

            foreach(Enemy enemy in enemies)
                enemy.Draw(spriteBatch, font);

            spriteBatch.DrawString(font, "Number of enemies" + enemies.Count, new Vector2(300, 300), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CollisionDetection()
        {

            foreach(Enemy enemy in enemies)
            {
                if (enemy.BoundingBox.Intersects(missile.BoundingBox))
                {
                    enemy.collision = true;
                    missile.collision = true;
                }
                foreach(City city in cities)
                {
                    if (enemy.BoundingBox.Intersects(city.BoundingBox))
                    {
                        city.collision = true;
                        enemy.collision = true;
                        break;
                    }
                }
            }
        }
    }
}
