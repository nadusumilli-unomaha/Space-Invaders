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
    abstract class Sprite
    {
        protected Texture2D textureImage;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 screenSize;
        protected Point frameSize;
        protected Point currentFrame;
        protected Point sheetSize;
        protected Vector2[] HitCenter;
        protected float[] HitRadia;
        protected Vector2[] Boxpos;
        protected bool alive = true;
        protected int timeSinceLastFrame = 0;
        protected int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 16;
        const int sw = 700;
        const int sh = 400;

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            Point currentFrame, Point sheetSize)
            : this(textureImage, position, frameSize, currentFrame,
            sheetSize, defaultMillisecondsPerFrame,sw,sh)
        {
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
    Point currentFrame, Point sheetSize, int millisecondsPerFrame,int screenwidth,int screenheight)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.screenSize = new Vector2(screenwidth, screenheight);
            this.HitCenter = new Vector2[20];
            this.Boxpos = new Vector2[20];
            this.HitRadia = new float[20];
        }

        //Create a bunch of setter and getter methods 
        //to make modifications to the position and other variables.
        public Vector2 getPosition()
        {
            return this.position;
        }

        public Vector2 getScreenSize()
        {
            return this.screenSize;
        }

        public Vector2 getHitCenter(int pos)
        {
            return this.HitCenter[pos];
        }

        public float getHitRadia(int pos)
        {
            return this.HitRadia[pos];
        }

        public bool getAlive()
        {
            return this.alive;
        }

        public Vector2 getHitBox(int pos)
        {
            return this.Boxpos[pos];
        }

        public int getHitBoxlength()
        {
            return this.Boxpos.Length;
        }

        public int getHitCenterlength()
        {
            return this.HitCenter.Length;
        }

        public Point getFrame()
        {
            return this.frameSize;
        }

        protected void setHitCenter(Vector2 v,int pos)
        {
            this.HitCenter[pos] = v;
        }

        protected void setHitRadia(float v, int pos)
        {
            this.HitRadia[pos] = v;
        }

        protected void setHitBox(Vector2 v, int pos)
        {
            this.Boxpos[pos] = v;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

    }
}
