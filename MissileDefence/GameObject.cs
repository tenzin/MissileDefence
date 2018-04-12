using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence
{
    public class GameObject
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 hotspot;
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)(position.X - hotspot.X),
                    (int)(position.Y - hotspot.Y),
                    texture.Width,
                    texture.Height);
            }
        }

        public GameObject(Texture2D texture, Vector2 position, Vector2 hotspot)
        {
            this.texture = texture;
            this.position = position;
            this.hotspot = hotspot;
        }

        public void DrawBoundingBox(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.pixel, new Rectangle(BoundingBox.X, BoundingBox.Y, BoundingBox.Width, 1), Color.White);
            spriteBatch.Draw(Globals.pixel, new Rectangle(BoundingBox.X, BoundingBox.Y, 1, BoundingBox.Height), Color.White);
            spriteBatch.Draw(Globals.pixel, new Rectangle(BoundingBox.X, BoundingBox.Bottom, BoundingBox.Width, 1), Color.White);
            spriteBatch.Draw(Globals.pixel, new Rectangle(BoundingBox.Right, BoundingBox.Y, 1, BoundingBox.Height), Color.White);
        }

        public void DrawHotSpot(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.pixel, new Rectangle(BoundingBox.X + (int)(hotspot.X - 2), BoundingBox.Y + (int)hotspot.Y, 4, 1), Color.Red);
            spriteBatch.Draw(Globals.pixel, new Rectangle(BoundingBox.X + (int)hotspot.X, BoundingBox.Y + (int)(hotspot.Y - 2), 1, 4), Color.Red);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
