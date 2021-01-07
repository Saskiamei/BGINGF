using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

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
        SoundEffect backgroundSound;
        public Scrolling (Texture2D newTexture, Rectangle newRectangle, SoundEffect newBackgroundSound)
        {
            texture = newTexture;
            rectangle = newRectangle;
            backgroundSound = newBackgroundSound;
        }   

        public void Update()
        {
            rectangle.X -= 1;
        }
    }
}
