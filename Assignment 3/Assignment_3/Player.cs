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
    class Player : Sprite
    {
        public Player(Texture2D textureImage, Vector2 position, Point frameSize,
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

        public void MoveManual(KeyboardState s)
        {
            if (s.IsKeyDown(Keys.Left))
            {
                //move left
                if (position.X + velocity.X > 0)
                    position.X += -3;
                else
                    position.X += 0;
            }

            if (s.IsKeyDown(Keys.Right))
            {
                //move right
                if (position.X + frameSize.X + velocity.X < screenSize.X)
                    position.X += 3;
                else
                    position.X += 0;
            }
            /*
            if (s.IsKeyDown(Keys.Up))
            {
                //move up
                if (position.Y + velocity.Y > 0)
                    position.Y += -3;
                else
                    position.Y += 0;
            }

            if (s.IsKeyDown(Keys.Down))
            {
                //move down
                if (position.Y + frameSize.Y + velocity.Y < screenSize.Y)
                    position.Y += 3;
                else
                    position.Y += 0;
            }*/
        }
        public void loadContent()
        {
            this.setHitBox(new Vector2(6,6),0);
            this.setHitBox(new Vector2(10,24),1);
            this.setHitBox(new Vector2(20, 6), 2);
            this.setHitBox(new Vector2(24, 24), 3);
            this.setHitBox(new Vector2(10, 10), 4);
            this.setHitBox(new Vector2(20, 24), 5);
        }

        public void loadContent1()
        {
            this.setHitBox(new Vector2(5, 15), 0);
            this.setHitBox(new Vector2(26, 19), 1);
            this.setHitBox(new Vector2(8, 9), 2);
            this.setHitBox(new Vector2(23, 15), 3);
            this.setHitBox(new Vector2(7, 19), 4);
            this.setHitBox(new Vector2(11, 23), 5);
            this.setHitBox(new Vector2(20, 19), 6);
            this.setHitBox(new Vector2(24, 23), 7);
            this.setHitBox(new Vector2(13, 19), 8);
            this.setHitBox(new Vector2(18, 21), 9);
        }

        public void loadContent2()
        {
            this.setHitBox(new Vector2(4, 9), 0);
            this.setHitBox(new Vector2(26, 21), 1);
            this.setHitBox(new Vector2(8, 2), 2);
            this.setHitBox(new Vector2(22, 6), 3);
            this.setHitBox(new Vector2(8, 21), 4);
            this.setHitBox(new Vector2(22, 23), 5);
            this.setHitBox(new Vector2(12, 23), 6);
            this.setHitBox(new Vector2(19, 25), 7);
        }

        public void loadContent3()
        {
            this.setHitBox(new Vector2(4, 2), 0);
            this.setHitBox(new Vector2(28, 28), 1);
        }

        public override void Update(GameTime gameTime)
        {
            //creating animation for the sprite.
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X > 1)
                    currentFrame.X = 0;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                }
            }
        }

        public void setAlive(bool v)
        {
            this.alive = v;
        }

        public void setCurrentFrame(Point v)
        {
            this.currentFrame = v;
        }

        public void setVelocity(Vector2 v)
        {
            this.velocity = v;
        }

        public void setPos(Vector2 v)
        {
            this.position = v;
        }

        public void MoveAlien()
        {
            this.position += this.velocity;
        }

        public void MoveGift()
        {
            this.position += this.velocity;
        }

        public void Dispose()
        {
            this.textureImage.Dispose();
        }

        public bool collide(Sprite sprite)
        {
            //checking if the sprites collide with the outer shape.
            if (this.position.X + this.getFrame().X > sprite.getPosition().X &&
               sprite.getPosition().X + sprite.getFrame().X > this.position.X &&
               this.position.Y + this.getFrame().Y > sprite.getPosition().Y &&
               sprite.getPosition().Y + sprite.getFrame().Y > this.position.Y)
            {
                int x = 0;
                //if they do create Bounding Boxes and spheres.
                List<BoundingBox> currentBox = new List<BoundingBox>();
                List<BoundingBox> passedBox = new List<BoundingBox>();
                List<BoundingSphere> currentSphere = new List<BoundingSphere>();
                List<BoundingSphere> passedSphere = new List<BoundingSphere>();

                //creating all the current boxes.
                for (int i = 0; i < this.Boxpos.Length / 2; i++)
                {
                    currentBox.Add(new BoundingBox(new Vector3(this.Boxpos[x++] + this.position, 0), new Vector3(this.Boxpos[x++] + this.position, 0)));
                }

                x = 0;
                //creating all the passed boxes.
                for (int i = 0; i < sprite.getHitBoxlength() / 2; i++)
                {
                    passedBox.Add(new BoundingBox(new Vector3(sprite.getHitBox(x++) + sprite.getPosition(), 0), new Vector3(sprite.getHitBox(x++) + sprite.getPosition(), 0)));
                }

                //creating all the current sphere's.
                for (int i = 0; i < this.HitCenter.Length; i++)
                {
                    currentSphere.Add(new BoundingSphere(new Vector3((this.HitCenter[i] + this.position), 0), this.HitRadia[i]));
                }

                //creating all the passed sphere's.
                for (int i = 0; i < sprite.getHitCenterlength(); i++)
                {
                    passedSphere.Add(new BoundingSphere(new Vector3((sprite.getHitCenter(i) + sprite.getPosition()), 0), sprite.getHitRadia(i)));
                }

                //Checking if the current box intersects the passed sphere.
                for (int j = 0; j < passedSphere.Count; j++)
                {
                    for (int i = 0; i < currentBox.Count; i++)
                    {
                        if (currentBox[i].Intersects(passedSphere[j]))
                            return true;
                    }
                }

                //Checking if the current box intersects the passed box.
                for (int j = 0; j < passedBox.Count; j++)
                {
                    for (int i = 0; i < currentBox.Count; i++)
                    {
                        if (currentBox[i].Intersects(passedBox[j]))
                            return true;
                    }
                }

                //Checking if the current sphere intersects the passed box.
                for (int j = 0; j < passedBox.Count; j++)
                {
                    for (int i = 0; i < currentSphere.Count; i++)
                    {
                        if (currentSphere[i].Intersects(passedBox[j]))
                            return true;
                    }
                }

                //Checking if the current sphere intersects the passed sphere.
                for (int j = 0; j < passedSphere.Count; j++)
                {
                    for (int i = 0; i < currentSphere.Count; i++)
                    {
                        if (currentSphere[i].Intersects(passedSphere[j]))
                            return true;
                    }
                }

                return false;
            }
            else
                return false;
        }
    }
}
