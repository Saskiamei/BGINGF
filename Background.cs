using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace BGINGF
{
    class Background
    {

        public Texture2D texture;
        public Rectangle rectangle;
        public SoundEffect backgroundSound;
        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    class Scrolling : Background
    {
        
        public Scrolling(Texture2D newTexture, Rectangle newRectangle, SoundEffect newBackgroundSoundEfffect)
        {
            texture = newTexture;
            rectangle = newRectangle;
            backgroundSound = newBackgroundSoundEfffect;
        }   

        public void Update()
        {
            rectangle.X -= 1;
        }
    }
}
