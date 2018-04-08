using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence
{
    public class City
    {
        Texture2D texture { get; set; }
        float positionX { get; set; }
        float positionY { get; set; }
        public City(Texture2D texture, float positionX, float positionY)
        {
            this.texture = texture;
            this.positionX = positionX;
            this.positionY = positionY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(positionX, positionY), Color.White);
            spriteBatch.End();
        }
    }
}
