using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence
{
    public class Enemy : GameObject
    {
        Vector2 velocity;
        float rotation;
        float speed;
        Random random;
        public bool collision;
        bool enableBoundingBox;
        bool fired;
        float timer;


        public Enemy(Texture2D texture, Vector2 position, Vector2 hotspot, int salt) : base(texture, position, hotspot)
        {
            random = new Random(salt);
            collision = false;
            enableBoundingBox = false;
            Reset();
            fired = false;
            timer = 0;
        }

        private void Reset()
        {
            position.X = random.Next(20, 800 - 20); //Enemy's position.X.
            position.Y = 0; 
            fired = false;
            timer = 0;
            rotation = (float)random.NextDouble() * (float)(3 * Math.PI / 4 - Math.PI / 4) + (float)Math.PI / 4; //Random rotation of 45degrees left or right wrt y axis
            speed = random.Next(60, 120);
            velocity = new Vector2((float)Math.Cos(rotation) * speed, (float)Math.Sin(rotation) * speed);
        }

        private bool OutOfBounds(float screenWidth, float screenHeight)
        {
            if ((position.X < 0 || position.X > screenWidth) || (position.Y < 0 || position.Y > screenHeight))
                return true;
            else
                return false;
        }

        public void Update(KeyboardState keystate, GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if(fired)
            {
                if (keystate.IsKeyDown(Keys.B))
                {
                    enableBoundingBox = !enableBoundingBox;
                }

                if (OutOfBounds(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height) || collision)
                {
                    Reset();
                    collision = false;
                }

                else
                    position = Vector2.Add(position, Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds));
            }

            else //Not fired ... do the timer thing
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 2)
                    fired = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if(fired)
            {
                spriteBatch.Draw(texture, position, null, Color.White, rotation, hotspot, 1, SpriteEffects.None, 0);
                if (enableBoundingBox)
                {
                    DrawBoundingBox(spriteBatch);
                    DrawHotSpot(spriteBatch);
                }
            }
            //else
                //spriteBatch.DrawString(font, "THREAT APPEARING IN " + (5 - (int)timer) + " SECONDS!", new Vector2(300, 10), Color.Red);
        }
    }
}
