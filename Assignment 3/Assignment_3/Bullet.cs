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
    class Bullet:Sprite
    {
        public Bullet(Texture2D textureImage, Vector2 position, Point frameSize,
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

        public void Draw1(GameTime gametime, SpriteBatch sprite,SpriteEffects n)
        {
            sprite.Begin();
            sprite.Draw(this.textureImage, this.position, new Rectangle(this.currentFrame.X * this.frameSize.X,
            this.currentFrame.Y * this.frameSize.Y, this.frameSize.X, this.frameSize.Y),
            Color.White, 0, Vector2.Zero, 1, n, 0);
            sprite.End();
        }

        public void UpdateBullets(int w, int h)
        {
            if (this.alive)
            {
                this.position += this.velocity;
                Rectangle viewPortRect = new Rectangle(0, 0, w, h);
                if (!viewPortRect.Contains(new Point((int)this.position.X, (int)this.position.Y)))
                {
                    this.alive = false;
                    this.position = new Vector2(w,h);
                }
            }
        }

        public bool firebullet(Vector2 pos,Vector2 speed)
        {
            if (this.alive == false)
            {
                this.alive = true;
                this.position = pos;
                this.velocity = speed;
                return true;
            }
            return false;
        }

        public void Move() //check where the sprite WILL be, if off screen change velocity
        {
            if (position.X + frameSize.X + velocity.X > screenSize.X)
            {
                velocity.X = -velocity.X;
                velocity.Y = -velocity.Y;
            }
            if (position.Y + frameSize.Y + velocity.Y > screenSize.Y)
            {
                velocity.X = -velocity.X;
                velocity.Y = -velocity.Y;
            }
            if (position.X + velocity.X < 0)
            {
                velocity.X = -velocity.X;
                velocity.Y = -velocity.Y;
            }
            if (position.Y + velocity.Y < 0)
            {
                velocity.X = -velocity.X;
                velocity.Y = -velocity.Y;
            }

            position += velocity;
        }

        public void Dispose()
        {
            this.textureImage.Dispose();
        }

        public void loadContent()
        {
            this.setHitBox(new Vector2(14, 8), 0);
            this.setHitBox(new Vector2(17, 23), 1);
        }

        public void loadContent1()
        {
            this.setHitBox(new Vector2(19, 22), 0);
            this.setHitBox(new Vector2(22, 25), 1);
            this.setHitBox(new Vector2(16, 19), 2);
            this.setHitBox(new Vector2(19, 22), 3);
            this.setHitBox(new Vector2(13, 16), 4);
            this.setHitBox(new Vector2(19, 22), 5);
            this.setHitBox(new Vector2(13, 16), 6);
            this.setHitBox(new Vector2(10, 13), 7);
            this.setHitBox(new Vector2(13, 16), 8);
            this.setHitBox(new Vector2(8, 11), 9);
            this.setHitBox(new Vector2(8, 11), 10);
        }

        public void loadContent2()
        {
            this.setHitBox(new Vector2(6, 22), 0);
            this.setHitBox(new Vector2(9, 25), 1);
            this.setHitBox(new Vector2(9, 19), 2);
            this.setHitBox(new Vector2(12, 22), 3);
            this.setHitBox(new Vector2(12, 16), 4);
            this.setHitBox(new Vector2(15, 19), 5);
            this.setHitBox(new Vector2(15, 13), 6);
            this.setHitBox(new Vector2(18, 16), 7);
            this.setHitBox(new Vector2(18, 11), 8);
            this.setHitBox(new Vector2(20, 13), 9);
        }

        public void loadContent3()
        {
            this.setHitCenter(new Vector2(16, 13), 0);
            this.setHitRadia(7F, 0);
            this.setHitBox(new Vector2(15, 5), 0);
            this.setHitBox(new Vector2(18, 8), 1);
            this.setHitBox(new Vector2(16, 2), 2);
            this.setHitBox(new Vector2(22, 5), 3);
        }

        public void setAlive(bool v)
        {
            this.alive = v;
        }

        public void setPosition(Vector2 v)
        {
            this.position = v;
        }

        public void setVelocity(Vector2 v)
        {
            this.velocity = v;
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
