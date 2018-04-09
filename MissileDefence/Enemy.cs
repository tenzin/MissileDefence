using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence
{
    public class Enemy
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        float rotation, speed;
        int screenWidth, screenHeight;
        Random random;
        public Enemy(Texture2D texture, int screenWidth, int screenHeight)
        {
            this.texture = texture;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            random = new Random();
            GeneratePositionVelocityRotationSpeed();
        }

        private void GeneratePositionVelocityRotationSpeed()
        {
            position.X = random.Next(0, screenWidth);
            position.Y = 0;
            rotation = (float)random.NextDouble() * (float)(Math.PI / 2) - (float)Math.PI / 4;
            speed = random.Next(45, 90);
            velocity = new Vector2((float)Math.Cos(rotation + Math.PI / 2) * speed, (float)Math.Sin(rotation + Math.PI / 2) * speed);
        }

        private bool OutOfBounds()
        {
            if ((position.X < 0 || position.X > screenWidth) || (position.Y < 0 || position.Y > screenHeight))
                return true;
            else
                return false;
        }

        public void Update(GameTime gameTime)
        {
            if(OutOfBounds())
                GeneratePositionVelocityRotationSpeed();
            else
                position = Vector2.Add(position, Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height), 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
