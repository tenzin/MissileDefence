using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence
{
    public class City : GameObject
    {
        public bool collision;
        bool enableBoundingBox;
        bool alive;

        int strength;

        public City(Texture2D texture, Vector2 position, Vector2 hotspot) : base(texture, position, hotspot)
        {
            collision = false;
            enableBoundingBox = false;
            strength = 10;
            alive = true;
        }

        public void Update(KeyboardState keystate, GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (keystate.IsKeyDown(Keys.B))
            {
                enableBoundingBox = !enableBoundingBox;
            }
            if(collision)
            {
                strength--;
                if (strength < 1)
                    alive = false;

                collision = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if(alive)
            {
                spriteBatch.Draw(texture, position, Color.White);
                if (enableBoundingBox)
                {
                    DrawBoundingBox(spriteBatch);
                    DrawHotSpot(spriteBatch);
                }
                spriteBatch.DrawString(font, "LIFE: " + strength, new Vector2(position.X, position.Y - 15) , Color.White);
            }
        }
    }
}
