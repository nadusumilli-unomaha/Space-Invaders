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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //declaring all the local variables to be used in the game.
        SpriteBatch spriteBatch;
        List<Enemy> Enemy = new List<Enemy>();
        List<Bullet> Bullet = new List<Bullet>();
        List<Bullet> Side = new List<Bullet>();
        List<Bullet> Side1 = new List<Bullet>();
        List<Bullet> Bomb = new List<Bullet>();
        List<Player> Dplay = new List<Player>();
        Player Spaceship, Gift,Player,sheild;
        SpriteFont Font;
        Vector2 Fontpos,Fontpos1;
        Start startpage,background,gameOver;
        Bullet Ebull;
        int x = 32, y = 32, point = 0, alien = 0,scoreGift = 0,Score = 0,enemyBullet = 0,Lives = 3,Eending = 352;
        int ERpos = 32,lpos = 0,dpay,Psheild = 2, a = 0;
        float speed = 0;
        const int MaxBullets = 8;
        bool bullone,bulltwo,Game_Over = false,bullfour;
        String gameState = "Start";
        Random r = new Random();
        Vector2 prevpos = new Vector2(400,400);
        KeyboardState prevkb;
        ContentManager Content;
        GraphicsDeviceManager graphics;
        SoundBank soundBank;
        AudioEngine audioEngine;
        WaveBank waveBank;
        Song track;
        float columnRight = 0, columnLeft = 0;

        public SpriteManager(Game game,ContentManager Content,GraphicsDeviceManager graphics)
            : base(game)
        {
            // TODO: Construct any child components here
            this.Content = Content;
            this.graphics = graphics;
        }
        
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            int m = 1,c = 0;
            spriteBatch = new SpriteBatch(GraphicsDevice);
                //Using the code for creating all three sprites in the list each at a random position using the r.next() function.
                for (int i = 0; i < 70; i++)
                {
                    if ((x + 32) > Eending)
                    {
                        y += 32;
                        x = ERpos;
                    }
                    Enemy s = new Enemy(Content.Load<Texture2D>("enemy sheet1"), new Vector2(x, y), new Point(32, 32), new Point(0, point), new Point(3, 3), 250, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    Enemy.Add(s);
                    x += 32;
                    point++;
                    if (point > 2)
                        point = 0;
                }

                //Creating the bullets that can be used in the game using the max bullets by the player.
                for (int i = 0; i < MaxBullets; i++)
                    Bullet.Add(new Bullet(Content.Load<Texture2D>("Bullet"), new Vector2(32, 400), new Point(32, 32), new Point(0, 0), new Point(2, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

                if (dpay < 1)
                {
                    //Creating the player that can be used in the game to display lives.
                    for (int i = 0; i < 3; i++)
                        Dplay.Add(new Player(Content.Load<Texture2D>("Player"), new Vector2(32, 400), new Point(32, 32), new Point(0, 0), new Point(4, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
                }

                //Creating the bullets that can be used in the game using the max bullets by the player.
                for (int i = 0; i < MaxBullets; i++)
                    Side.Add(new Bullet(Content.Load<Texture2D>("Bullet"), new Vector2(32, 400), new Point(32, 32), new Point(1, 0), new Point(2, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

                //Creating the bullets that can be used in the game using the max bullets by the player.
                for (int i = 0; i < MaxBullets; i++)
                    Side1.Add(new Bullet(Content.Load<Texture2D>("Bullet"), new Vector2(32, 400), new Point(32, 32), new Point(1, 0), new Point(2, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

                //creating the bombs that can be used in the game by the player once the gifts reach 5.
                for (int i = 0; i < MaxBullets; i++)
                    Bomb.Add(new Bullet(Content.Load<Texture2D>("Bomb"), new Vector2(32, 400), new Point(32, 32), new Point(0, 0), new Point(2, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

                ///Creating the player sprite.
                Player = new Player(Content.Load<Texture2D>("Player"), prevpos, new Point(32, 32), new Point(0, 0), new Point(4, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                //Creating the spaceship sprite.
                Spaceship = new Player(Content.Load<Texture2D>("Spaceship"), prevpos, new Point(32, 32), new Point(0, 0), new Point(2, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                //Creating the gift sprite.
                Gift = new Player(Content.Load<Texture2D>("Gift"), prevpos, new Point(32, 32), new Point(0, 0), new Point(2, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                //Creating the bullet that can be used by the enemy sprites when they are moving across the screen.
                Ebull = new Bullet(Content.Load<Texture2D>("Bullet"), new Vector2(750, 500), new Point(32, 32), new Point(0, 0), new Point(2, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                //Creating the game Over sprite that can be displayed when the player sprite dies.
                sheild = new Player(Content.Load<Texture2D>("sheild"), new Vector2(0, 0), new Point(32,32), new Point(0, 0), new Point(1, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                //Creating the start screen page so that i can acess it to create the player entrance.
                startpage = new Start(Content.Load<Texture2D>("start"), new Vector2(0, 0), new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), new Point(0, 0), new Point(1, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                //Creating the background screen which can be displayed while the game is being played.
                background = new Start(Content.Load<Texture2D>("background"), new Vector2(0, 0), new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), new Point(0, 0), new Point(1, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                //Creating the game Over sprite that can be displayed when the player sprite dies.
                gameOver = new Start(Content.Load<Texture2D>("gameOver"), new Vector2(0, 0), new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), new Point(0, 0), new Point(1, 1), 160, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                //Setting the value of alive for all the Enemy in the list.
                foreach (Enemy s in Enemy)
                    s.setAlive(true);

                if (dpay < 1)
                {
                    //Setting the value of alive for all the player in the list.
                    foreach (Player s in Dplay)
                        s.setAlive(true);
                }

                //Setting the value of alive for the enemy bullet.
                Ebull.setAlive(false);

                //Calling the method load content for all the bullets in the list.
                foreach (Bullet s in Bullet)
                    s.loadContent();

                //Calling the method load content for all the bullets in the list.
                foreach (Bullet s in Side1)
                    s.loadContent2();

                //Calling the method load content for all the bullets in the list.
                foreach (Bullet s in Side)
                    s.loadContent1();

                //Calling the method load content for all the bombs in the list.
                foreach (Bullet s in Bomb)
                    s.loadContent3();

                //Calling the method load content for the enemy bullet.
                Ebull.loadContent();

                //Calling the method load content for all the Enemy sprites in the list.
                foreach (Enemy s in Enemy)
                {
                    if (m == 1)
                    {
                        s.loadContent();
                        s.setType(1);
                        m++;
                    }
                    else if (m == 2)
                    {
                        s.loadContent1();
                        s.setType(2);
                        m++;
                    }
                    else if (m == 3)
                    {
                        s.loadContent2();
                        s.setType(3);
                        m = 1;
                    }
                    s.setMoveEnemy();
                    s.setEnemy(c++);
                }

                //Setting the value of alive for all the bullets in the list.
                foreach (Bullet s in Bullet)
                    s.setAlive(false);

                //Setting the value of alive for all the bullets in the list.
                foreach (Bullet s in Side)
                    s.setAlive(false);

                //Setting the value of alive for all the bullets in the list.
                foreach (Bullet s in Side1)
                    s.setAlive(false);

                //Setting the value of alive for all the bombs in the list.
                foreach (Bullet s in Bomb)
                    s.setAlive(false);

                //Loading all the media content to be used in the game.
                track = Content.Load<Song>("spaceinvaders1");
                MediaPlayer.Play(track);
                MediaPlayer.IsRepeating = true;
                audioEngine = new AudioEngine(@"Content\a3.xgs");
                waveBank = new WaveBank(audioEngine, @"Content\Wave Bank.xwb");
                soundBank = new SoundBank(audioEngine, @"Content\Sound Bank.xsb");

                //Loadin all the spritefont content that can be used in the game to display the scores.
                Font = Content.Load<SpriteFont>("spritefont");
                Fontpos = new Vector2(680, 450);
                Fontpos1 = new Vector2(480, 450);

                //Loading the content of the player sprite, the alien space shit and the gift sprite.
                Player.loadContent();
                Spaceship.loadContent1();
                Gift.loadContent2();
                sheild.loadContent3();
                //setting the position of the gift to offscreen so that it is not displayed. 
                Gift.setPos(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

                //setting the position of the gift to offscreen so that it is not displayed. 
                sheild.setPos(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

                //Setting the velocity of the player sprite,alien spaceship and the gift.
                Player.setVelocity(new Vector2(1, 0));
                Spaceship.setVelocity(new Vector2(-2, 0));
                Gift.setVelocity(new Vector2(0, 3));
                sheild.setVelocity(new Vector2(1,0));

                //Setting the alive attribute to false so that it is not displayed on the screen.
                Gift.setAlive(false);

                if (dpay < 1)
                {
                    //Setting the alive attribute to false so that it is not displayed on the screen.
                    sheild.setAlive(false);
                }

                //Setting the player alive attribute and position at the beginning of the game.
                Spaceship.setPos(new Vector2(graphics.PreferredBackBufferWidth - 32, 0));
                Spaceship.setAlive(true);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            //Unloading each of the player content and disposing the sprites once this method is called.
            foreach (Enemy s in Enemy)
                s.Dispose();
            Player.Dispose();
            Spaceship.Dispose();
            Gift.Dispose();
            Ebull.Dispose();
            sheild.Dispose();
            foreach (Bullet s in Bullet)
                s.Dispose();
            foreach (Bullet s in Bomb)
                s.Dispose();
            foreach (Bullet s in Side)
                s.Dispose();
            foreach (Bullet s in Side1)
                s.Dispose();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //Initializing local variables to be used in the method.
            int m = 0;

            //Creating a keyboard state.
            KeyboardState k = Keyboard.GetState();

            //Checking if the game stae is In game or not. Updating the content based on that.
            if (gameState == "Game")
            {
                //If game State is in game we pause the music and start the game.
                MediaPlayer.Pause();

                //Setting the Enemy column positions so that the movement check for the enemy's can be made.
                setColumn();

                //Updating the position of the enemy using the move and Update method everytime.
                foreach (Enemy s in Enemy)
                {
                    s.MoveEnemy(gameTime, s.getScreenSize().X - 64F, columnRight, columnLeft,speed);
                    s.Update(gameTime);
                }

                //Setting the player to move manually using the keyboard and sending the instance of keyboard k. Also updating the player sprite after movement.
                Player.MoveManual(k);
                sheild.MoveManual(k);
                Player.Update(gameTime);
                Spaceship.Update(gameTime);
                Gift.Update(gameTime);

                //Moving the Alien Spaceship across the screen
                Spaceship.MoveAlien();

                //Checking for the fresh press of spacebar.
                if (k.IsKeyDown(Keys.Space) && prevkb.IsKeyUp(Keys.Space))
                {
                    //If it is pressed playing the shoot soundEffect and then checking which gift position the Sprite is in.
                    //if it is Gift 0 one bullet is shot, Gift 1 two bullets are shot, Gift 3 three bullets are shot and so on.
                    soundBank.PlayCue("shoot");
                    if (scoreGift == 0)
                    {
                        foreach (Bullet s in Bullet)
                        {
                            bullone = s.firebullet(Player.getPosition(),new Vector2(0,-3));
                            if (bullone)
                                break;
                        }
                    }
                    else if(scoreGift == 1)
                    {
                        foreach (Bullet s in Side)
                        {
                            bulltwo = s.firebullet(Player.getPosition(), new Vector2(-3,-3));
                            if (bulltwo)
                                break;
                        }

                        foreach (Bullet s in Side1)
                        {
                            bulltwo = s.firebullet(Player.getPosition(), new Vector2(3,-3));
                            if (bulltwo)
                                break;
                        }
                    }
                    else if(scoreGift == 2)
                    {
                        foreach (Bullet s in Side)
                        {
                            bullone = s.firebullet(Player.getPosition(), new Vector2(-3, -3));
                            if (bullone)
                                break;
                        }

                        foreach (Bullet s in Side1)
                        {
                            bulltwo = s.firebullet(Player.getPosition(), new Vector2(3, -3));
                            if (bulltwo)
                                break;
                        }

                        foreach (Bullet s in Bullet)
                        {
                            bullone = s.firebullet(Player.getPosition(), new Vector2(0, -3));
                            if (bullone)
                                break;
                        }
                    }
                    else if (scoreGift == 3)
                    {
                        foreach (Bullet s in Bomb)
                        {
                            bullfour = s.firebullet(Player.getPosition(), new Vector2(0, -3));
                            if (bulltwo)
                                break;
                        }
                    }
                    else
                    {
                        foreach (Bullet s in Bomb)
                        {
                            bullfour = s.firebullet(Player.getPosition(), new Vector2(0, -3));
                            if (bulltwo)
                                break;
                        }
                    }
                }
                prevkb = k;

                //Checking for the collision detection of the bullet and the enemy. If they collide settin the alive attribute
                //of both enemy and bullet to false and setting their position to offscreen.
                //Also accumulating the score to be displayed on the screen.
                foreach (Enemy n in Enemy)
                {
                    foreach (Bullet s in Bullet)
                    {
                        if (n.getAlive() == true)
                        {
                            if (s.collide(n) == true)
                            {
                                if (n.getType() == 1)
                                    Score += 10;
                                else if (n.getType() == 2)
                                    Score += 20;
                                else if (n.getType() == 3)
                                    Score += 30;
                                soundBank.PlayCue("invaderkilled");
                                s.setAlive(false);
                                s.setPosition(new Vector2(0, graphics.PreferredBackBufferHeight));
                                n.setCurrentFrame(2);
                            }
                        }
                    }
                    foreach (Bullet s in Side)
                    {
                        if (n.getAlive() == true)
                        {
                            if (s.collide(n) == true)
                            {
                                if (n.getType() == 1)
                                    Score += 10;
                                else if (n.getType() == 2)
                                    Score += 20;
                                else if (n.getType() == 3)
                                    Score += 30;
                                soundBank.PlayCue("invaderkilled");
                                s.setAlive(false);
                                s.setPosition(new Vector2(0, graphics.PreferredBackBufferHeight));
                                n.setCurrentFrame(2);
                            }
                        }
                    }
                    foreach (Bullet s in Side1)
                    {
                        if (n.getAlive() == true)
                        {
                            if (s.collide(n) == true)
                            {
                                if (n.getType() == 1)
                                    Score += 10;
                                else if (n.getType() == 2)
                                    Score += 20;
                                else if (n.getType() == 3)
                                    Score += 30;
                                soundBank.PlayCue("invaderkilled");
                                s.setAlive(false);
                                s.setPosition(new Vector2(0, graphics.PreferredBackBufferHeight));
                                n.setCurrentFrame(2);
                            }
                        }
                    }
                }

                //Checking for the collision of bullet and the alien sprite. Once the alien sprite is killed The gift attribute
                //Alive is set to true and the alien sprite is set to false. The gift is realeased after the death and score is
                //accumulated.
                foreach(Bullet s in Bullet)
                    if (s.collide(Spaceship) == true)
                    {
                        if(Spaceship.getAlive() == true)
                            Score += 200;
                        Spaceship.setAlive(false);
                        if (Gift.getAlive() == false)
                        {
                            Gift.setAlive(true);
                            Gift.setPos(Spaceship.getPosition());
                        }
                    }
                foreach (Bullet s in Side)
                    if (s.collide(Spaceship) == true)
                    {
                        if (Spaceship.getAlive() == true)
                            Score += 200;
                        Spaceship.setAlive(false);
                        if (Gift.getAlive() == false)
                        {
                            Gift.setAlive(true);
                            Gift.setPos(Spaceship.getPosition());
                        }
                        Score += 200;
                    }
                foreach (Bullet s in Side1)
                    if (s.collide(Spaceship) == true)
                    {
                        if (Spaceship.getAlive() == true)
                            Score += 200;
                        Spaceship.setAlive(false);
                        if (Gift.getAlive() == false)
                        {
                            Gift.setAlive(true);
                            Gift.setPos(Spaceship.getPosition());
                        }
                        Score += 200;
                    }
                foreach (Bullet s in Bomb)
                    if (s.collide(Spaceship) == true)
                    {
                        if (Spaceship.getAlive() == true)
                            Score += 200;
                        Spaceship.setAlive(false);
                        if (Gift.getAlive() == false)
                        {
                            Gift.setAlive(true);
                            Gift.setPos(Spaceship.getPosition());
                        }
                        Score += 200;
                    }
                //Checking collision detection for the bomb sprite and when it collides we have an area effect of 4 sprites. So
                //whenever it collides i kill the enemysprite to the left, Right, ablove and below the present collision sprite
                //I set the alive attribute for all those sprites and move them offscreen. Score is accumulated.
                foreach (Enemy n in Enemy)
                {
                    foreach (Bullet s in Bomb)    
                    {
                        if (n.getAlive() == true)
                        {
                            if (s.collide(n))
                            {
                                if (n.getType() == 1)
                                    Score += 10;
                                else if (n.getType() == 2)
                                    Score += 20;
                                else if (n.getType() == 3)
                                    Score += 30;

                                if (n.getEnemy() == 69 || n.getEnemy() == 59 || n.getEnemy() == 49 || n.getEnemy() == 39 || n.getEnemy() == 29 || n.getEnemy() == 19 || n.getEnemy() == 9) { }
                                else
                                {
                                    Enemy[n.getEnemy() + 1].setAlive(false);
                                    if (Enemy[n.getEnemy() + 1].getType() == 1)
                                        Score += 10;
                                    else if (Enemy[n.getEnemy() + 1].getType() == 2)
                                        Score += 20;
                                    else if (Enemy[n.getEnemy() + 1].getType() == 3)
                                        Score += 30;
                                }
                                if (n.getEnemy() == 60 || n.getEnemy() == 50 || n.getEnemy() == 40 || n.getEnemy() == 30 || n.getEnemy() == 20 || n.getEnemy() == 10 || n.getEnemy() == 0) { }
                                else
                                {
                                    Enemy[n.getEnemy() - 1].setAlive(false);
                                    if (Enemy[n.getEnemy() - 1].getType() == 1)
                                        Score += 10;
                                    else if (Enemy[n.getEnemy() - 1].getType() == 2)
                                        Score += 20;
                                    else if (Enemy[n.getEnemy() - 1].getType() == 3)
                                        Score += 30;
                                }
                                if (n.getEnemy() == 0 || n.getEnemy() == 1 || n.getEnemy() == 2 || n.getEnemy() == 3 || n.getEnemy() == 4 || n.getEnemy() == 5 || n.getEnemy() == 6 || n.getEnemy() == 7 || n.getEnemy() == 8 || n.getEnemy() == 9) { }
                                else
                                {
                                    Enemy[n.getEnemy() - 10].setAlive(false);
                                    if (Enemy[n.getEnemy() - 10].getType() == 1)
                                        Score += 10;
                                    else if (Enemy[n.getEnemy() - 10].getType() == 2)
                                        Score += 20;
                                    else if (Enemy[n.getEnemy() - 10].getType() == 3)
                                        Score += 30;
                                }
                                soundBank.PlayCue("Bomb");
                                s.setAlive(false);
                                s.setPosition(new Vector2(0, graphics.PreferredBackBufferHeight));
                                n.setAlive(false);
                            }
                        }
                    }
                }

                //Checking if the Gift attribute alive is true and if it is we move the gift sprite down the scree.
                if (Gift.getAlive())
                    Gift.MoveGift();

                //Checking if the alien ship is offscreen completely and then setting its Alive attribute to true. so that it can
                //be displayed again.
                if (Spaceship.getPosition().X + Spaceship.getFrame().X < 0)
                    Spaceship.setAlive(true);

                //Updatin the sprite graphics of the bullets and the bombs so that they can move.
                foreach (Bullet s in Bullet)
                    s.UpdateBullets(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
                foreach (Bullet s in Bomb)
                    s.UpdateBullets(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
                foreach (Bullet s in Side)
                    s.UpdateBullets(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
                foreach (Bullet s in Side1)
                    s.UpdateBullets(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

                //Updating the Enemy bullet graphics.
                Ebull.UpdateBullets(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

                //Setting the alien to move only once in every thousand loops. So that the gifts are not easily accessible.
                if (alien == 1000)
                {
                    alien = 0;
                    Spaceship.setPos(new Vector2(graphics.PreferredBackBufferWidth, 0));
                }
                alien++;

                //Creating a random position for the enemy bullet to be released. The bullet is also relaesed every 100 loops
                //so that it can be effective.
                m = r.Next(0, 70);
                if (enemyBullet == 100)
                {
                    if(Enemy[m].getAlive())
                    bullfour = Ebull.firebullet(Enemy[m].getPosition(),new Vector2(0,3));
                    enemyBullet = 0;
                }
                enemyBullet++;

                //Checking if the enemy bullet collides with the Player sprites. IF they do we reduce a life of the player. 
                //If the lives turn to 0 we set the game position to Game_Over. Setting the music to play again.
                if (Ebull.collide(Player))
                {
                    soundBank.PlayCue("explosion");
                    Dplay[Lives - 1].setAlive(false);
                        if (++a % 2 == 0)
                            Player.setCurrentFrame(new Point(2, 0));
                        else
                            Player.setCurrentFrame(new Point(3,0));
                        Lives--;
                    if (Lives == 0)
                    {
                        Game_Over = true;
                        MediaPlayer.Play(track);
                    }
                    else
                    {
                        Ebull.setPosition(new Vector2(0, graphics.PreferredBackBufferHeight));
                        if(a == 10000)
                            Reset();
                    }
                    scoreGift = 0;
                }

                if (sheild.getAlive())
                {
                    if (Ebull.collide(sheild))
                    {
                        Ebull.setPosition(new Vector2(0, graphics.PreferredBackBufferHeight));
                        Psheild--;
                        if (Psheild == 0)
                        {
                            sheild.setAlive(false);
                        }
                    }
                }

                //Enemy collision with player to set the game position to Game_Over.Setting the music to play again.
                foreach(Enemy s in Enemy)
                    if(s.getAlive())
                    if (s.collide(Player))
                    {
                        soundBank.PlayCue("explosion");
                        if(Lives > 0)
                            Dplay[Lives - 1].setAlive(false);
                        else
                            Dplay[Lives].setAlive(false);
                        Lives--;
                        if (Lives == 0)
                        {
                            Game_Over = true;
                            MediaPlayer.Play(track);
                        }
                        else
                            Reset();
                    }

                //Checking the collision between player and gift to see if the player has taken the gift or not.
                if (Gift.collide(Player))
                {
                    soundBank.PlayCue("electric");
                    scoreGift++;
                    Gift.setAlive(false);
                    Gift.setPos(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
                    if (scoreGift >= 4)
                    {
                        sheild.setAlive(true);
                        sheild.setPos(Player.getPosition());
                        Psheild = 2;
                    }
                }
                else if (Gift.getPosition().Y > graphics.PreferredBackBufferHeight)
                    Gift.setAlive(false);
            //The end of the in game position of the if statement.
            }

            //Checking what the game state is and restarting the game based on that.
            if (k.IsKeyDown(Keys.Enter))
            {
                if (gameState == "Game Over")
                {
                    gameState = "Start";
                    ResetGM();
                }
                else
                    gameState = "Game";
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            int count = 0;
            GraphicsDevice.Clear(Color.White);
            
            //Checking for the game state in draw method to draw the required stuff.
            if (Game_Over == false)
            {
                //If the game State is start screen then we dislay the start screen stuff on screen.
                if (gameState == "Start")
                {
                    startpage.Draw(gameTime, spriteBatch);
                }
                else if(gameState == "Game")//else if it is In Game we draw the background and all the sprites to the screen if
                {//they are aive.
                    background.Draw(gameTime, spriteBatch);
                    foreach (Enemy s in Enemy)
                    {
                        if (s.getAlive() == true)
                            s.Draw(gameTime, spriteBatch);
                        if (s.getCurrentFrame() == 2)
                        {
                            s.setAlive(false);
                        }
                    }

                    foreach (Bullet s in Bullet)
                        if (s.getAlive() == true)
                            s.Draw(gameTime, spriteBatch);

                    foreach (Bullet s in Side)
                        if (s.getAlive() == true)
                            s.Draw1(gameTime, spriteBatch,SpriteEffects.FlipVertically);

                    foreach (Bullet s in Side1)
                        if (s.getAlive() == true)
                            s.Draw(gameTime, spriteBatch);

                    foreach (Bullet s in Bomb)
                        if (s.getAlive() == true)
                            s.Draw(gameTime, spriteBatch);

                    if (Ebull.getAlive() == true)
                        Ebull.Draw1(gameTime, spriteBatch, SpriteEffects.FlipVertically);
                    if (Player.getAlive() == true)
                        Player.Draw(gameTime, spriteBatch);
                    if (Spaceship.getAlive() == true)
                        Spaceship.Draw(gameTime, spriteBatch);
                    if (Gift.getAlive() == true)
                        Gift.Draw(gameTime, spriteBatch);
                    if (sheild.getAlive() == true)
                        sheild.Draw(gameTime, spriteBatch);

                    //We are drawing the Score to the screen using the Spritefont method which is inbuilt.
                    //All i have done is accumulated the score and used the string to print the score to the screen.
                    String output = "Score: "+Score;
                    String lives = "Lives: ";
                    Vector2 FontOrigin = Font.MeasureString(output) / 2;
                    Vector2 FontOrigin1 = Font.MeasureString(lives) / 2;
                    spriteBatch.Begin();
                    spriteBatch.DrawString(Font, output, Fontpos, Color.LightGreen,0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    spriteBatch.DrawString(Font, lives, Fontpos1, Color.LightGreen, 0, FontOrigin1, 1.0f, SpriteEffects.None, 0.5f);
                    spriteBatch.End();

                    lpos = 0;
                    foreach (Player s in Dplay)
                    {
                        s.setPos(new Vector2(Fontpos1.X + FontOrigin1.X - 10 + lpos, Fontpos1.Y + FontOrigin1.Y - 30));
                        if (s.getAlive() == true)
                        {
                            s.Draw(gameTime, spriteBatch);
                        }
                        lpos += s.getFrame().X;
                    }
                }
                else if (gameState == "Game Over")
                {
                    //If game state is Game Over we draw the game over stuff to the screen.
                    gameOver.Draw(gameTime, spriteBatch);
                }
            }
            else
            {
                //Setting the game State to game Over and setting Game_Over to false to go to ingame.
                gameState = "Game Over";
                Game_Over = false;
            }

            //Checking if all the enemySprites are dead and Resetting the sprites position based on that to go to next level.
            foreach (Enemy s in Enemy)
            {
                if (s.getAlive() == false)
                    count++;
            }
            if (count == 70)
            {
                gameState = "Game";
                ResetLevel();
            }

            
            base.Draw(gameTime);
        }

        //This method reset all the attributes of the game so that we can have a fresh start. It is called after the game is over.
        public void ResetGM()
        {
            x = 32; y = 32;
            ERpos = 32;
            Eending = 352;
            Lives = 3;
            lpos = 0;
            Psheild = 2;
            prevpos = Player.getPosition();
            Enemy = new List<Enemy>();
            Bullet = new List<Bullet>();
            Dplay = new List<Player>();
            dpay = 0;
            point = 0;
            alien = 0;
            columnLeft = 0; columnRight = 0; 
            scoreGift = 0;
            bullone = false;
            bulltwo = false;
            Game_Over = false;
            this.LoadContent();
            Score = 0;
        }

        //This method resets all the attributes of the game but keeps track of the score because we moved to the next level.
        public void ResetLevel()
        {
            x = 32; y = 32;
            Enemy = new List<Enemy>();
            Bullet = new List<Bullet>();
            Side = new List<Bullet>();
            Side1 = new List<Bullet>();
            Bomb = new List<Bullet>();
            //Dplay = new List<Player>();
            dpay++;
            ERpos = 32;
            Eending = 352;
            lpos = 0;
            speed += 10;
            prevpos = Player.getPosition();
            point = 0;
            alien = 0;
            columnLeft = 0; columnRight = 0;
            bullone = false;
            bulltwo = false;
            Game_Over = false;
            this.LoadContent();
        }

        //This method resets all the attributes of the game but keeps track of the score because we moved to the next level.
        public void Reset()
        {
            x = (int)Enemy[0].getPosition().X;
            y = (int)Enemy[0].getPosition().Y;
            Eending = (int)columnRight;
            ERpos = x;
            lpos = 0;
            prevpos = Player.getPosition();
            point = 0;
            alien = 0;
            columnLeft = 0; columnRight = 0; 
            bullone = false;
            bulltwo = false;
            Game_Over = false;
        }

        //This method is used to set the left and right column position to the moving sprite block.
        public void setColumn()
        {
            //Setting the leftMost column.
            if (Enemy[9].getAlive() == true || Enemy[19].getAlive() == true || Enemy[29].getAlive() == true || Enemy[39].getAlive() == true || Enemy[49].getAlive() == true || Enemy[59].getAlive() == true || Enemy[69].getAlive() == true)
                columnRight = Enemy[9].getPosition().X + Enemy[9].getFrame().X + 10F;
            else if (Enemy[8].getAlive() == true || Enemy[18].getAlive() == true || Enemy[28].getAlive() == true || Enemy[38].getAlive() == true || Enemy[48].getAlive() == true || Enemy[58].getAlive() == true || Enemy[68].getAlive() == true)
                columnRight = Enemy[8].getPosition().X + Enemy[8].getFrame().X + 10F;
            else if (Enemy[7].getAlive() == true || Enemy[17].getAlive() == true || Enemy[27].getAlive() == true || Enemy[37].getAlive() == true || Enemy[47].getAlive() == true || Enemy[57].getAlive() == true || Enemy[67].getAlive() == true)
                columnRight = Enemy[7].getPosition().X + Enemy[7].getFrame().X + 10F;
            else if (Enemy[6].getAlive() == true || Enemy[16].getAlive() == true || Enemy[26].getAlive() == true || Enemy[36].getAlive() == true || Enemy[46].getAlive() == true || Enemy[56].getAlive() == true || Enemy[66].getAlive() == true)
                columnRight = Enemy[6].getPosition().X + Enemy[6].getFrame().X + 10F;
            else if (Enemy[5].getAlive() == true || Enemy[15].getAlive() == true || Enemy[25].getAlive() == true || Enemy[35].getAlive() == true || Enemy[45].getAlive() == true || Enemy[55].getAlive() == true || Enemy[65].getAlive() == true)
                columnRight = Enemy[5].getPosition().X + Enemy[5].getFrame().X + 10F;
            else if (Enemy[4].getAlive() == true || Enemy[14].getAlive() == true || Enemy[24].getAlive() == true || Enemy[34].getAlive() == true || Enemy[44].getAlive() == true || Enemy[54].getAlive() == true || Enemy[64].getAlive() == true)
                columnRight = Enemy[4].getPosition().X + Enemy[4].getFrame().X + 10F;
            else if (Enemy[3].getAlive() == true || Enemy[13].getAlive() == true || Enemy[23].getAlive() == true || Enemy[33].getAlive() == true || Enemy[43].getAlive() == true || Enemy[53].getAlive() == true || Enemy[63].getAlive() == true)
                columnRight = Enemy[3].getPosition().X + Enemy[3].getFrame().X + 10F;
            else if (Enemy[2].getAlive() == true || Enemy[12].getAlive() == true || Enemy[22].getAlive() == true || Enemy[32].getAlive() == true || Enemy[42].getAlive() == true || Enemy[52].getAlive() == true || Enemy[62].getAlive() == true)
                columnRight = Enemy[2].getPosition().X + Enemy[2].getFrame().X + 10F;
            else if (Enemy[1].getAlive() == true || Enemy[11].getAlive() == true || Enemy[21].getAlive() == true || Enemy[31].getAlive() == true || Enemy[41].getAlive() == true || Enemy[51].getAlive() == true || Enemy[61].getAlive() == true)
                columnRight = Enemy[1].getPosition().X + Enemy[1].getFrame().X + 10F;
            else
                columnRight = Enemy[0].getPosition().X + Enemy[0].getFrame().X + 10F;

            //Setting the leftmost column.
            if (Enemy[0].getAlive() == true || Enemy[10].getAlive() == true || Enemy[20].getAlive() == true || Enemy[30].getAlive() == true || Enemy[40].getAlive() == true || Enemy[50].getAlive() == true || Enemy[60].getAlive() == true)
                columnLeft = Enemy[0].getPosition().X - 10F;
            else if (Enemy[1].getAlive() == true || Enemy[11].getAlive() == true || Enemy[21].getAlive() == true || Enemy[31].getAlive() == true || Enemy[41].getAlive() == true || Enemy[51].getAlive() == true || Enemy[61].getAlive() == true)
                columnLeft = Enemy[1].getPosition().X - 10F;
            else if (Enemy[2].getAlive() == true || Enemy[12].getAlive() == true || Enemy[22].getAlive() == true || Enemy[32].getAlive() == true || Enemy[42].getAlive() == true || Enemy[52].getAlive() == true || Enemy[62].getAlive() == true)
                columnLeft = Enemy[2].getPosition().X - 10F;
            else if (Enemy[3].getAlive() == true || Enemy[13].getAlive() == true || Enemy[23].getAlive() == true || Enemy[33].getAlive() == true || Enemy[43].getAlive() == true || Enemy[53].getAlive() == true || Enemy[63].getAlive() == true)
                columnLeft = Enemy[3].getPosition().X - 10F;
            else if (Enemy[4].getAlive() == true || Enemy[14].getAlive() == true || Enemy[24].getAlive() == true || Enemy[34].getAlive() == true || Enemy[44].getAlive() == true || Enemy[54].getAlive() == true || Enemy[64].getAlive() == true)
                columnLeft = Enemy[4].getPosition().X - 10F;
            else if (Enemy[5].getAlive() == true || Enemy[15].getAlive() == true || Enemy[25].getAlive() == true || Enemy[35].getAlive() == true || Enemy[45].getAlive() == true || Enemy[55].getAlive() == true || Enemy[65].getAlive() == true)
                columnLeft = Enemy[5].getPosition().X - 10F;
            else if (Enemy[6].getAlive() == true || Enemy[16].getAlive() == true || Enemy[26].getAlive() == true || Enemy[36].getAlive() == true || Enemy[46].getAlive() == true || Enemy[56].getAlive() == true || Enemy[66].getAlive() == true)
                columnLeft = Enemy[6].getPosition().X - 10F;
            else if (Enemy[7].getAlive() == true || Enemy[17].getAlive() == true || Enemy[27].getAlive() == true || Enemy[37].getAlive() == true || Enemy[47].getAlive() == true || Enemy[57].getAlive() == true || Enemy[67].getAlive() == true)
                columnLeft = Enemy[7].getPosition().X - 10F;
            else if (Enemy[8].getAlive() == true || Enemy[18].getAlive() == true || Enemy[28].getAlive() == true || Enemy[38].getAlive() == true || Enemy[48].getAlive() == true || Enemy[58].getAlive() == true || Enemy[68].getAlive() == true)
                columnLeft = Enemy[8].getPosition().X - 10F;
            else
                columnLeft = Enemy[9].getPosition().X - 10F;

        }
    }
}
