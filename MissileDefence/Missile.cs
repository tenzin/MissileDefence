using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence
{
    public class Missile : GameObject
    {
        Vector2 velocity;
        float rotation;
        float deltaRotation;
        float speed;
        bool launched;
        public bool collision;
        bool enableBoundingBox;
        int score;

        public Missile(Texture2D texture, Vector2 position, Vector2 hotspot) : base(texture, position, hotspot)
        {
            rotation = 0;
            deltaRotation = 0.035F; //2 degrees
            speed = 230;
            launched = false;
            collision = false;
            enableBoundingBox = false;
            score = 0;
        }

        private bool OutOfBounds(GraphicsDevice graphicsDevice)
        {

            if ((position.X < 0 || position.X > graphicsDevice.Viewport.Width) || (position.Y < 0 || position.Y > graphicsDevice.Viewport.Height))
                return true;
            else
                return false;
        }

        public void Update(KeyboardState keystate, GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (keystate.IsKeyDown(Keys.B))
            {
                enableBoundingBox = !enableBoundingBox;
            }

            if (keystate.IsKeyDown(Keys.Up) && !launched)
            {
                launched = true;
                velocity = new Vector2((float)Math.Cos(rotation - Math.PI / 2) * speed, (float)Math.Sin(rotation - Math.PI / 2) * speed);
                //velocity.Normalize();
            }

            if (launched) //missile fired
            {
                //position += velocity * speed;
                position = Vector2.Add(position, Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds));

                if (OutOfBounds(graphicsDevice) || collision)
                {
                    launched = false;
                    position = new Vector2(395, 445);
                    rotation = 0;
                    score++;
                    collision = false;
                }
            }

            else //Missile not fired. Can rotate missile with Left & Right Key upto 45 degrees to both side
            {
                if (keystate.IsKeyDown(Keys.Left) && rotation > - (float)Math.PI / 4)
                    rotation -= deltaRotation;
                else if (keystate.IsKeyDown(Keys.Right) && rotation < (float)Math.PI / 4)
                    rotation += deltaRotation;
            }


        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, hotspot, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "SCORE: " + score, new Vector2(375, 385), Color.Green);
            if (enableBoundingBox)
            {
                DrawBoundingBox(spriteBatch);
                DrawHotSpot(spriteBatch);
            }
        }
    }
}
