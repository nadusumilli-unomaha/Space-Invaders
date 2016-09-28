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
    class Enemy: Sprite
    {
        bool enemyRight,enemydown;
        int enemy;
        int TSLFenemy = 0, MMSenemy = 600;
        int ecpos = 0;
        int type = 0;

        public Enemy(Texture2D textureImage, Vector2 position, Point frameSize,
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

        public void moveLeft(GameTime gameTime,float speed)
        {
            TSLFenemy += gameTime.ElapsedGameTime.Milliseconds;
            if (TSLFenemy > MMSenemy)
            {
                TSLFenemy -= MMSenemy;
                this.position.X += (-10-speed);
            }
        }

        public void moveRight(GameTime gameTime, float speed)
        {
            TSLFenemy += gameTime.ElapsedGameTime.Milliseconds;
            if (TSLFenemy > MMSenemy)
            {
                TSLFenemy -= MMSenemy;
                this.position.X += (10+speed);
            }
        }

        public void MoveEnemy(GameTime gameTime, float ScreenSize,float columnRight,float columnLeft,float speed)
        {
                if (this.enemydown == true)
                {
                    this.position.Y += (10+speed);
                    enemy++;
                    if (enemy == 1)
                    {
                        this.enemydown = false;
                        enemy = 0;
                    }
                }
                else if (this.enemydown == false && this.enemyRight == true)
                {
                    if (columnRight > ScreenSize)
                    {
                        this.enemydown = true;
                        this.enemyRight = false;
                    }
                        this.moveRight(gameTime,speed);
                }
                else if (this.enemydown == false && this.enemyRight == false)
                {
                    if (columnLeft < 64)
                    {
                        this.enemydown = true;
                        this.enemyRight = true;
                    }
                        this.moveLeft(gameTime,speed);
                }
        }


        public void Dispose()
        {
            this.textureImage.Dispose();
        }

        public void setMoveEnemy()
        {
            this.enemydown = false;
            this.enemyRight = true;
        }

        public void loadContent()
        {

            this.setHitCenter(new Vector2(15.5F,14), 0);
            this.setHitRadia(7F, 0);
            this.setHitBox(new Vector2(7, 18), 0);
            this.setHitBox(new Vector2(24, 24), 1);
        }

        public void loadContent1()
        {
            this.setHitCenter(new Vector2(15.5F, 14.5F), 0);
            this.setHitRadia(9F, 0);
        }

        public void loadContent2()
        {
            this.setHitCenter(new Vector2(14F, 14.5F), 0);
            this.setHitRadia(9F, 0);
        }

        public void setAlive(bool v)
        {
            this.alive = v;
        }

        public void setEnemy(int v)
        {
            this.ecpos = v;
        }

        public void setType(int v)
        {
            this.type = v;
        }

        public int getType()
        {
            return this.type;
        }

        public int getEnemy()
        {
            return this.ecpos;
        }

        public void setCurrentFrame(int v)
        {
            this.currentFrame.X = v;
        }

        public int getCurrentFrame()
        {
            return this.currentFrame.X;
        }

        public void setPos(Vector2 v)
        {
            this.position = v;
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
