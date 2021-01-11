﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BGINGF
{
    class Background
    {

        public Texture2D texture;
        public Rectangle rectangle;
        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        
        }
    }

    class Scrolling : Background
    {
        
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

    

        public void Update()
        {
            rectangle.X -= 1;
        }
    }
}
