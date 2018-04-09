using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence
{
    public class Missile
    {

        Texture2D texture { get; set; }
        Vector2 position { get; set; }
        Vector2 velocity { get; set; }
        float rotation, deltaRotation, speed;
        bool launched { get; set; }
        int screenWidth, screenHeight;
        public Missile(Texture2D texture, Vector2 position, int screenWidth, int screenHeight)
        {
            this.texture = texture;
            this.position = position;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            rotation = 0;
            deltaRotation = 0.03F;
            launched = false;
            speed = 230;
        }

        private bool OutOfBounds()
        {
            if ((position.X < 0 || position.X > screenWidth) || (position.Y < 0 || position.Y > screenHeight))
                return true;
            else
                return false;
        }

        public void Update(KeyboardState keystate, GameTime gameTime)
        {
            if (keystate.IsKeyDown(Keys.Up) && !launched)
            {
                launched = true;
                velocity = new Vector2((float)Math.Cos(rotation - Math.PI / 2) * speed, (float)Math.Sin(rotation - Math.PI / 2) * speed);
                //velocity.Normalize();
            }

            if(OutOfBounds())
            {
                launched = false;
                position = new Vector2(395, 470);
                rotation = 0;
            }

            if (launched)
            {
                //position += velocity * speed;
                position = Vector2.Add(position, Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds));
                
            }

            else //Missile not fired
            {
                if (keystate.IsKeyDown(Keys.Left) && rotation > -(float)Math.PI / 4)
                    rotation -= deltaRotation;
                else if (keystate.IsKeyDown(Keys.Right) && rotation < (float)Math.PI / 4)
                    rotation += deltaRotation;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //if (launched)
            //    spriteBatch.Draw(texture, position, Color.White);
            //else
                spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height), 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
