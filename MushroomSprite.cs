using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BGINGF
{
    class MushroomSprite : Sprite
    {
        public MushroomSprite(Texture2D newSpriteSheet, Texture2D newCollisionTxr, Vector2 newLocation)
         : base(newSpriteSheet, newCollisionTxr, newLocation)
        {
            spriteOrigin = new Vector2(1.5f, 1.5f);
            isColliding = true;

            animations = new List<List<Rectangle>>();
            animations.Add(new List<Rectangle>());
            animations[0].Add(new Rectangle(0, 0, 48, 48));
        }
    }
}
