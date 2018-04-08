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
        public Missile(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            //velocity = new Vector2(1, 2);
            rotation = 0;
            deltaRotation = 0.03F;
            launched = false;
            speed = 3;
        }

        private void OutOfBounds()
        {
            
        }

        public void Update(KeyboardState keystate)
        {
            if (keystate.IsKeyDown(Keys.Up))
            {
                launched = true;
                velocity = new Vector2((float)Math.Cos(rotation - Math.PI / 2), (float)Math.Sin(rotation - Math.PI / 2));
                velocity.Normalize();
            }

            //if(OutOfBounds())
            //{
                
            //}

            if (launched)
            {
                position += velocity * speed;
                
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
