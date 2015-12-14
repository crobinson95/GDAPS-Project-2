using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using System.IO;

namespace GDAPS_Project_2
{
    class Player : MovableGameObject
    {

        float fric = (float)GameVariables.friction;
        float jump = (float)GameVariables.jump;
        float accel = (float)GameVariables.playerAcceleration;
        float maxSpeed = (float)GameVariables.playerMaxSpeed;
        float airSpeed = (float)GameVariables.maxAirSpeed;
        float airControl = (float)GameVariables.playerAirControl;

        public bool victory = false;
        public World world;

        public double energy = 280;
        public Stopwatch coolDown = new Stopwatch();

        public AudioListener listener;

        public Player(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
            grav = gravDirection.Down;
            gravity = GameVariables.gravity;
            xVelocity = 0;
            yVelocity = 0;
            inAir = true;
            alive = true;

            listener = new AudioListener();
            listener.Position = new Vector3(x, y, 0.0f);

            FramesPerSec = 10;
            //Adds animation arrays - currenty using dummy values
            AddAnimation(15, 80, 40, 0, "Down_Right", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(15, 68, 294, 0, "Down_Left", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(15, 80, 548, 0, "Up_Right", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(15, 68, 804, 0, "Up_Left", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(15, 22, 1110, 0, "Left_Up", 255, 222, 124, new Vector2(0, 0));
            AddAnimation(15, 22, 1360, 0, "Left_Down", 255, 222, 124, new Vector2(0, 0));
            AddAnimation(15, 10, 1616, 0, "Right_Up", 255, 218, 128, new Vector2(0, 0));
            AddAnimation(15, 22, 1868, 0, "Right_Down", 255, 222, 120, new Vector2(0, 0));
            AddAnimation(1, 80, 40, 0, "Down_Idle_Right", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(1, 68, 294, 0, "Down_Idle_Left", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(1, 80, 548, 0, "Up_Idle_Right", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(1, 68, 804, 0, "Up_Idle_Left", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(1, 22, 1110, 0, "Left_Idle_Up", 255, 222, 124, new Vector2(0, 0));
            AddAnimation(1, 22, 1360, 0, "Left_Idle_Down", 255, 222, 124, new Vector2(0, 0));
            AddAnimation(1, 22, 1616, 0, "Right_Idle_Up", 255, 222, 128, new Vector2(0, 0));
            AddAnimation(1, 22, 1868, 0, "Right_Idle_Down", 255, 222, 120, new Vector2(0, 0));
            AddAnimation(1, 68, 2018, 0, "Down_Jump_Right", 255, 120, 220, new Vector2(0, 0));
            AddAnimation(1, 286, 2022, 0, "Down_Jump_Left", 255, 120, 220, new Vector2(0, 0));
            AddAnimation(1, 284, 2304, 0, "Up_Jump_Right", 255, 120, 220, new Vector2(0, 0));
            AddAnimation(1, 68, 2304, 0, "Up_Jump_Left", 255, 120, 220, new Vector2(0, 0));
            AddAnimation(1, 284, 2590, 0, "Right_Jump_Up", 255, 220, 120, new Vector2(0, 0));
            AddAnimation(1, 36, 2608, 0, "Right_Jump_Down", 255, 220, 120, new Vector2(0, 0));
            AddAnimation(1, 266, 2826, 0, "Left_Jump_Up", 255, 220, 120, new Vector2(0, 0));
            AddAnimation(1, 24, 2826, 0, "Left_Jump_Down", 255, 220, 120, new Vector2(0, 0));

        }

        public void Movement(KeyboardState k, KeyboardState p, GameTime g, SoundLoop fallingLoop)
        {
            ////Energy
            if (grav != gravDirection.Down && !coolDown.IsRunning) { energy -= (60 * g.ElapsedGameTime.TotalSeconds); }
            if (grav == gravDirection.Down && energy < 280)
            {
                energy += (20 * g.ElapsedGameTime.TotalSeconds);
                if (energy > 280)
                {
                    energy = 280;
                }
            }
            if (energy <= 0)
            {
                if (grav == gravDirection.Left || grav == gravDirection.Right)
                {
                    ObjPos.Y -= 25;
                }
                ObjRectWidth = 30;
                ObjRectHeight = 54;
                grav = gravDirection.Down;
                fallingLoop.End();
                Game1.PlayRandomSound(GameVariables.GravitySounds);
                if (currentDir == myDirection.right)
                {
                    PlayAnimation("Down_Idle_Right");
                }
                else
                {
                    PlayAnimation("Down_Idle_Left");
                }
            }


            ObjPos += new Vector2(xVelocity, yVelocity);
            switch (grav)
            {
                case gravDirection.Down:
                    yVelocity += (float)gravity;
                    if (yVelocity > airSpeed) { yVelocity = airSpeed; }
                    if (yVelocity > 1) { fallingLoop.Loop(); }

                    if (!inAir)
                    {
                        yVelocity -= (float)gravity;
                        fallingLoop.End();
                        if (xVelocity > fric) { xVelocity -= fric; }
                        else if (xVelocity < -fric) { xVelocity += fric; }
                        else { xVelocity = 0; }
                        if (k.IsKeyDown(Keys.A))
                        {
                            currentDir = myDirection.left;
                            PlayAnimation("Down_Left");

                            xVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (xVelocity < -maxSpeed) { xVelocity = -maxSpeed; }
                            if (frameIndex == 3 | frameIndex == 7 | frameIndex == 11 | frameIndex == 14)
                            {
                                Game1.PlayRandomSound(GameVariables.FootstepSounds);
                            }
                        }
                        else if (k.IsKeyDown(Keys.D))
                        {
                            currentDir = myDirection.right;
                            PlayAnimation("Down_Right");

                            xVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (xVelocity > maxSpeed) { xVelocity = maxSpeed; }
                            if (frameIndex == 3 | frameIndex == 7 | frameIndex == 11 | frameIndex == 14)
                            {
                                Game1.PlayRandomSound(GameVariables.FootstepSounds);
                            }
                        }
                        else if (currentDir == myDirection.right)
                        {
                            PlayAnimation("Down_Idle_Right");
                        }

                        else if (currentDir == myDirection.left)
                        {
                            PlayAnimation("Down_Idle_Left");
                        }
                        if (k.IsKeyDown(Keys.Space))
                        {
                            ObjPos.Y -= 5;
                            yVelocity = -jump;
                            inAir = true;
                            if (currentDir == myDirection.right)
                            {
                                PlayAnimation("Down_Jump_Right");
                            }
                            else
                            {
                                PlayAnimation("Down_Jump_Left");
                            }
                        }
                    }
                    if (k.IsKeyDown(Keys.A))
                    {
                        currentDir = myDirection.left;
                        xVelocity -= airControl * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity < -airSpeed) { xVelocity = -airSpeed; }
                    }
                    else if (k.IsKeyDown(Keys.D))
                    {
                        currentDir = myDirection.right;
                        xVelocity += airControl * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity > airSpeed) { xVelocity = airSpeed; }
                    }
                    break;

                case gravDirection.Up:
                    yVelocity -= (float)gravity;
                    if (yVelocity <= -airSpeed) { yVelocity = -airSpeed; }
                    if (yVelocity < -1) { fallingLoop.Loop(); }

                    if (!inAir)
                    {
                        yVelocity += (float)gravity;
                        fallingLoop.End();
                        if (xVelocity > fric) { xVelocity -= fric; }
                        else if (xVelocity < -fric) { xVelocity += fric; }
                        else { xVelocity = 0; }
                        if (k.IsKeyDown(Keys.A))
                        {
                            currentDir = myDirection.left;
                            PlayAnimation("Up_Left");
                            xVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (xVelocity < -maxSpeed) { xVelocity = -maxSpeed; }
                        }
                        else if (k.IsKeyDown(Keys.D))
                        {
                            currentDir = myDirection.right;
                            PlayAnimation("Up_Right");
                            xVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (xVelocity > maxSpeed) { xVelocity = maxSpeed; }
                        }
                        else if (currentDir == myDirection.right)
                        {
                            PlayAnimation("Up_Idle_Right");
                        }

                        else if (currentDir == myDirection.left)
                        {
                            PlayAnimation("Up_Idle_Left");
                        }
                        if (k.IsKeyDown(Keys.Space))
                        {
                            ObjPos.Y += 5;
                            yVelocity = jump;
                            inAir = true;
                            if (currentDir == myDirection.right)
                            {
                                PlayAnimation("Up_Jump_Right");
                            }
                            else
                            {
                                PlayAnimation("Up_Jump_Left");
                            }
                        }
                    }
                    if (k.IsKeyDown(Keys.A))
                    {
                        xVelocity -= airControl * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity < -airSpeed) { xVelocity = -maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.D))
                    {
                        xVelocity += airControl * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity > airSpeed) { xVelocity = maxSpeed; }
                    }
                    break;

                case gravDirection.Right:
                    xVelocity += (float)gravity;
                    if (xVelocity >= airSpeed) { xVelocity = airSpeed; }
                    if (xVelocity > 1) { fallingLoop.Loop(); }

                    if (!inAir)
                    {
                        xVelocity -= (float)gravity;
                        fallingLoop.End();
                        if (yVelocity > fric) { yVelocity -= fric; }
                        else if (yVelocity < -fric) { yVelocity += fric; }
                        else { yVelocity = 0; }
                        if (k.IsKeyDown(Keys.W))
                        {
                            currentDir = myDirection.up;
                            PlayAnimation("Right_Up");

                            yVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (yVelocity < -maxSpeed) { yVelocity = -maxSpeed; }
                        }
                        else if (k.IsKeyDown(Keys.S))
                        {
                            currentDir = myDirection.down;
                            PlayAnimation("Right_Down");

                            yVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (yVelocity > maxSpeed) { yVelocity = maxSpeed; }
                        }
                        else if (currentDir == myDirection.up)
                        {
                            PlayAnimation("Right_Idle_Up");
                        }

                        else if (currentDir == myDirection.down)
                        {
                            PlayAnimation("Right_Idle_Down");
                        }
                        if (k.IsKeyDown(Keys.Space))
                        {
                            ObjPos.X -= 5;
                            xVelocity = -jump;
                            inAir = true;
                            if (currentDir == myDirection.up)
                            {
                                PlayAnimation("Right_Jump_Up");
                            }
                            else
                            {
                                PlayAnimation("Right_Jump_Down");
                            }
                        }
                    }
                    if (k.IsKeyDown(Keys.W))
                    {
                        yVelocity -= airControl * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity < -airSpeed) { yVelocity = -maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.S))
                    {
                        yVelocity += airControl * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity > airSpeed) { yVelocity = maxSpeed; }
                    }
                    break;

                case gravDirection.Left:
                    xVelocity -= (float)gravity;
                    if (xVelocity <= -airSpeed) { xVelocity = -airSpeed; }
                    if (xVelocity < -1) { fallingLoop.Loop(); }

                    if (!inAir)
                    {
                        xVelocity += (float)gravity;
                        fallingLoop.End();
                        if (yVelocity > fric) { yVelocity -= fric; }
                        else if (yVelocity < -fric) { yVelocity += fric; }
                        else { yVelocity = 0; }
                        if (k.IsKeyDown(Keys.W))
                        {
                            currentDir = myDirection.up;
                            PlayAnimation("Left_Up");

                            yVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (yVelocity < -maxSpeed) { yVelocity = -maxSpeed; }
                        }
                        else if (k.IsKeyDown(Keys.S))
                        {
                            currentDir = myDirection.down;
                            PlayAnimation("Left_Down");

                            yVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (yVelocity > maxSpeed) { yVelocity = maxSpeed; }
                        }
                        else if (currentDir == myDirection.up)
                        {
                            PlayAnimation("Left_Idle_Up");
                        }

                        else if (currentDir == myDirection.down)
                        {
                            PlayAnimation("Left_Idle_Down");
                        }
                        if (k.IsKeyDown(Keys.Space))
                        {
                            ObjPos.X += 5;
                            xVelocity = jump;
                            inAir = true;
                            if (currentDir == myDirection.up)
                            {
                                PlayAnimation("Left_Jump_Up");
                            }
                            else
                            {
                                PlayAnimation("Left_Jump_Down");
                            }
                        }
                    }
                    if (k.IsKeyDown(Keys.W))
                    {
                        yVelocity -= airControl * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity < -airSpeed) { yVelocity = -maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.S))
                    {
                        yVelocity += airControl * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity > airSpeed) { yVelocity = maxSpeed; }
                    }
                    break;
            }
            if (energy >= 40)
            {
                if (Game1.SingleKeyPress(Keys.Up, k, p) && grav != gravDirection.Up)
                {
                    ObjRectWidth = 30;
                    ObjRectHeight = 54;
                    grav = gravDirection.Up;
                    fallingLoop.End();
                    Game1.PlayRandomSound(GameVariables.GravitySounds);
                    if (currentDir == myDirection.right)
                    {
                        PlayAnimation("Up_Idle_Right");
                    }
                    else
                    {
                        PlayAnimation("Up_Idle_Left");
                    }
                }
                if (Game1.SingleKeyPress(Keys.Down, k, p) && grav != gravDirection.Down)
                {
                    if (grav == gravDirection.Left || grav == gravDirection.Right)
                    {
                        ObjPos.Y -= 25;
                    }
                    ObjRectWidth = 30;
                    ObjRectHeight = 54;
                    grav = gravDirection.Down;
                    fallingLoop.End();
                    Game1.PlayRandomSound(GameVariables.GravitySounds);
                    if (currentDir == myDirection.right)
                    {
                        PlayAnimation("Down_Idle_Right");
                    }
                    else
                    {
                        PlayAnimation("Down_Idle_Left");
                    }
                }
                if (Game1.SingleKeyPress(Keys.Right, k, p) && grav != gravDirection.Right)
                {
                    ObjRectWidth = 54;
                    ObjRectHeight = 30;
                    grav = gravDirection.Right;
                    fallingLoop.End();
                    Game1.PlayRandomSound(GameVariables.GravitySounds);
                    if (currentDir == myDirection.up)
                    {
                        PlayAnimation("Right_Idle_Up");
                    }
                    else
                    {
                        PlayAnimation("Right_Idle_Down");
                    }
                }
                if (Game1.SingleKeyPress(Keys.Left, k, p) && grav != gravDirection.Left)
                {
                    ObjRectWidth = 54;
                    ObjRectHeight = 30;
                    grav = gravDirection.Left;
                    fallingLoop.End();
                    Game1.PlayRandomSound(GameVariables.GravitySounds);
                    if (currentDir == myDirection.up)
                    {
                        PlayAnimation("Left_Idle_Up");
                    }
                    else
                    {
                        PlayAnimation("Left_Idle_Down");
                    }
                }
            }

            ObjRectX = (int)ObjPos.X;
            ObjRectY = (int)ObjPos.Y;
            listener.Position = new Vector3(ObjPos.X, ObjPos.Y, 0.0f);
        }

        public override void spriteDraw(SpriteBatch s)
        {
            if (alive)
            {
                base.Draw(s);
            }
        }

        public void Collisions(KeyboardState k, KeyboardState p, World w, StreamReader s, ContentManager content)
        {
            GameVariables.landingInstance.Volume = 1.0f * GameVariables.gameVolume;
            inAir = true;
            List<GameObject> objs = w.levels[w.currentLevel].objects;
            List<Enemy> enms = w.levels[w.currentLevel].enemies;
            foreach (Enemy en in enms)
            {
                if (isColliding(en) && en.alive)
                {
                    alive = false;
                }
                if (en.alive)
                {
                    en.enemySound.Volume = 1.0f * GameVariables.gameVolume;
                    en.enemySound.Apply3D(listener, en.enemyEmitter);
                    en.enemySound.Play();
                }
            }
            foreach (GameObject obj in objs)
            {
                if (obj is Hazard)
                {
                    Hazard temp = (Hazard)obj;
                    if (temp.wireEmitter != null)
                    {
                        temp.wireSound.Volume = 0.2f * GameVariables.gameVolume;
                        temp.wireSound.Apply3D(listener, temp.wireEmitter);
                        temp.wireSound.Play();
                    }
                }
                if ((obj is Door))
                {
                    Door temp = (Door)obj;
                    temp.Open(this);
                }
                if (isColliding(obj) && obj.isDangerous)
                {
                    alive = false;
                    ObjPos.X = w.levels[w.currentLevel].deathCount ++;
                }
                else if (isColliding(obj))
                {
                    if (obj is Door)
                    {
                        Door temp = (Door)obj;
                        if (Game1.SingleKeyPress(Keys.E, k, p) && temp.destWorld == null)
                        {
                            w.currentLevel = temp.destination;

                            ObjPos.X = w.levels[w.currentLevel].playerSpawn.X;
                            ObjPos.Y = w.levels[w.currentLevel].playerSpawn.Y;
                            xVelocity = 0.0f;
                            yVelocity = 0.0f;
                            ObjRectWidth = 30;
                            ObjRectHeight = 54;
                            grav = gravDirection.Down;
                            PlayAnimation("Down_Idle_Right");
                        }
                        else if (Game1.SingleKeyPress(Keys.E, k, p))
                        {
                            world = new World(temp.destWorld, s, this, content);
                            world.changeWorldBool = true;
                            world.currentLevel = temp.destination + ".txt";
                            ObjPos.X = world.levels[world.currentLevel].playerSpawn.X;
                            ObjPos.Y = world.levels[world.currentLevel].playerSpawn.Y;
                            xVelocity = 0.0f;
                            yVelocity = 0.0f;
                            ObjRectWidth = 30;
                            ObjRectHeight = 54;
                            grav = gravDirection.Down;
                            if (temp.victory)
                            {
                                this.victory = true;
                            }
                            PlayAnimation("Down_Idle_Right");
                        }
                    }
                    // Background Case
                    else if (obj is Panel) { }

                    // Left middle.
                    else if (obj.ObjRect.Contains(ObjRect.Left, ObjRect.Center.Y))
                    {
                        if (grav == gravDirection.Left)
                        {
                            inAir = false;
                            if (xVelocity < -1)
                            {
                                GameVariables.landing.Play();
                            }
                            xVelocity = 0;
                        }
                        else
                        {
                            xVelocity = +1;
                        }
                        ObjPos.X = obj.ObjRect.Right;
                    }
                    // Right middle.
                    else if (obj.ObjRect.Contains(ObjRect.Right, ObjRect.Center.Y))
                    {
                        if (grav == gravDirection.Right)
                        {
                            inAir = false;
                            if (xVelocity > 1)
                            {
                                GameVariables.landing.Play();
                            }
                            xVelocity = 0;
                        }
                        else
                        {
                            xVelocity = -1;
                        }
                        ObjPos.X = obj.ObjRect.Left - ObjRect.Width + 1;
                    }
                    // Top middle.
                    else if (obj.ObjRect.Contains(ObjRect.Center.X, ObjRect.Top))
                    {
                        if (grav == gravDirection.Up)
                        {
                            inAir = false;
                            if (yVelocity < -1)
                            {
                                GameVariables.landing.Play();
                            }
                            yVelocity = 0;
                        }
                        else
                        {
                            yVelocity = +1;
                        }
                        ObjPos.Y = obj.ObjRect.Bottom;
                    }

                    // Bottom middle.
                    else if (obj.ObjRect.Contains(ObjRect.Center.X, ObjRect.Bottom))
                    {
                        if (grav == gravDirection.Down)
                        {
                            if (inAir == true)
                            {
                                inAir = false;
                                if (yVelocity > 1)
                                {
                                    GameVariables.landing.Play();
                                }
                            }
                            yVelocity = 0;
                        }
                        else
                        {
                            yVelocity = -1;
                        }

                        ObjPos.Y = obj.ObjRect.Top - ObjRect.Height + 1;

                    }
                    // Top left corner.
                    else if (obj.ObjRect.Contains(ObjRect.X + 12, ObjRect.Y + 12))
                    {
                        if (grav == gravDirection.Up)
                        {
                            inAir = false;
                            ObjPos.Y = obj.ObjRect.Bottom;
                            yVelocity = 0;
                        }
                        else if (grav == gravDirection.Left)
                        {
                            inAir = false;
                            ObjPos.X = obj.ObjRect.Right;
                            xVelocity = 0;
                        }
                        else
                        {
                            if (xVelocity < 0) { xVelocity = 0; ObjPos.X = obj.ObjRect.Right; }
                            else if (yVelocity < 0) { yVelocity = 0; ObjPos.Y = obj.ObjRect.Bottom; }
                        }
                    }
                    // Top right corner.
                    else if (obj.ObjRect.Contains(ObjRect.Right - 12, ObjRect.Y + 12))
                    {
                        if (grav == gravDirection.Up)
                        {
                            inAir = false;
                            ObjPos.Y = obj.ObjRect.Bottom;
                            yVelocity = 0;
                        }
                        else if (grav == gravDirection.Right)
                        {
                            inAir = false;
                            ObjPos.X = obj.ObjRect.Left - ObjRect.Width;
                            xVelocity = 0;
                        }
                        else
                        {
                            if (xVelocity > 0) { xVelocity = 0; ObjPos.X = obj.ObjRect.Left - ObjRect.Width; }
                            else if (yVelocity < 0) { yVelocity = 0; ObjPos.Y = obj.ObjRect.Bottom; }
                        }
                    }
                    // Bottom left corner.
                    else if (obj.ObjRect.Contains(ObjRect.Left + 12, ObjRect.Bottom - 12))
                    {
                        if (grav == gravDirection.Down)
                        {
                            inAir = false;
                            ObjPos.Y = obj.ObjRect.Top - ObjRect.Height;
                            yVelocity = 0;
                        }
                        else if (grav == gravDirection.Left)
                        {
                            inAir = false;
                            ObjPos.X = obj.ObjRect.Right;
                            xVelocity = 0;
                        }
                        else
                        {
                            if (xVelocity < 0) { xVelocity = 0; ObjPos.X = obj.ObjRect.Right; }
                            else if (yVelocity > 0) { yVelocity = 0; ObjPos.Y = obj.ObjRect.Top - ObjRect.Height; }
                        }
                    }
                    // Bottom right corner.
                    else if (obj.ObjRect.Contains(ObjRect.Right - 12, ObjRect.Bottom - 12))
                    {
                        if (grav == gravDirection.Down)
                        {
                            inAir = false;
                            ObjPos.Y = obj.ObjRect.Top - ObjRect.Height;
                            yVelocity = 0;
                        }
                        else if (grav == gravDirection.Right)
                        {
                            inAir = false;
                            ObjPos.X = obj.ObjRect.Left - ObjRect.Width;
                            xVelocity = 0;
                        }
                        else
                        {
                            if (xVelocity > 0) { xVelocity = 0; ObjPos.X = obj.ObjRect.Left - ObjRect.Width; }
                            else if (yVelocity > 0) { yVelocity = 0; ObjPos.Y = obj.ObjRect.Top - ObjRect.Height; }
                        }
                    }
                    ObjRectX = (int)ObjPos.X;
                    ObjRectY = (int)ObjPos.Y;
                }
            }
        }

        public bool IsDead()
        {
            if (alive)
            {
                return false;
            }
            else
            {
                alive = true;
                return true;
            }
        }

        //Grabs the sprite sheet - not currently in pipe line
        public void LoadContent(ContentManager content)
        {
            sTexture = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/sprite_sheet");
        }

        //Updates position of character sprite
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            base.Update(gameTime);
        }
    }
}

