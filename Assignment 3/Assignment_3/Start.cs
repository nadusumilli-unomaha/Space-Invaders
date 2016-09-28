using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Assignment_3
{
    class Start : Sprite
    {

         public Start(Texture2D textureImage, Vector2 position, Point frameSize,
    Point currentFrame, Point sheetSize, int millisecondsPerFrame, int screenwidth, int screenheight)
            :base( textureImage, position, frameSize,
     currentFrame, sheetSize, millisecondsPerFrame, screenwidth, screenheight)
        { 
        }
        public override void Draw(GameTime gametime, SpriteBatch sprite)
        {
            sprite.Begin();
            sprite.Draw(this.textureImage, this.position, new Rectangle(this.currentFrame.X * this.frameSize.X,
            this.currentFrame.Y * this.frameSize.Y, this.frameSize.X, this.frameSize.Y),
            Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            sprite.End();
        }
    }
}
