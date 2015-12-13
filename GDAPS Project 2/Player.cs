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

        bool maxFall;

        public World world;

        public double energy = 280;
        public Stopwatch coolDown = new Stopwatch();

        public Player(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
            grav = gravDirection.Down;
            gravity = GameVariables.gravity;
            xVelocity = 0;
            yVelocity = 0;
            inAir = true;
            alive = true;
            maxFall = false;

            FramesPerSec = 10;
            //Adds animation arrays - currenty using dummy values
            AddAnimation(16, 80, 18, 0, "Down_Right", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(16, 68, 274, 0, "Down_Left", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(16, 80, 530, 0, "Up_Right", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(16, 68, 786, 0, "Up_Left", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(16, 22, 1084, 0, "Left_Up", 255, 222, 124, new Vector2(0, 0));
            AddAnimation(16, 22, 1340, 0, "Left_Down", 255, 222, 124, new Vector2(0, 0));
            AddAnimation(16, 22, 1596, 0, "Right_Up", 255, 218, 128, new Vector2(0, 0));
            AddAnimation(16, 22, 1850, 0, "Right_Down", 255, 222, 120, new Vector2(0, 0));
            AddAnimation(1, 80, 20, 0, "Down_Idle_Right", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(1, 68, 274, 0, "Down_Idle_Left", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(1, 80, 530, 0, "Up_Idle_Right", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(1, 68, 786, 0, "Up_Idle_Left", 255, 124, 216, new Vector2(0, 0));
            AddAnimation(1, 22, 1084, 0, "Left_Idle_Up", 255, 222, 124, new Vector2(0, 0));
            AddAnimation(1, 22, 1340, 0, "Left_Idle_Down", 255, 222, 124, new Vector2(0, 0));
            AddAnimation(1, 22, 1596, 0, "Right_Idle_Up", 255, 222, 128, new Vector2(0, 0));
            AddAnimation(1, 22, 1850, 0, "Right_Idle_Down", 255, 222, 120, new Vector2(0, 0));
        }

        public void Movement(KeyboardState k, KeyboardState p, GameTime g, SoundLoop fallingLoop)
        {            ////Energy Nonsense
            //if (coolDown.ElapsedMilliseconds >= 5000) { coolDown.Reset(); coolDown.Stop(); }
            //if (grav != gravDirection.Down && !coolDown.IsRunning) { energy -= (60 * g.ElapsedGameTime.TotalSeconds); }
            //if (grav == gravDirection.Down && energy < 280 && !coolDown.IsRunning)
            //{
            //    energy += (20 * g.ElapsedGameTime.TotalSeconds);
            //    if (energy > 280)
            //    {
            //        energy = 280;
            //    }
            //}
            //if (energy <= 0)
            //{
            //    grav = gravDirection.Down;
            //    coolDown.Start();
            //}
            ////Energy nonsense


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
                        maxFall = false;
                        if (xVelocity > fric) { xVelocity -= fric; }
                        else if (xVelocity < -fric) { xVelocity += fric; }
                        else { xVelocity = 0; }
                        if (k.IsKeyDown(Keys.A))
                        {
                            currentDir = myDirection.left;
                            PlayAnimation("Down_Left");
                            spriteDirection += new Vector2(-1, 0);

                            xVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (xVelocity < -maxSpeed) { xVelocity = -maxSpeed; }
                        }
                        else if (k.IsKeyDown(Keys.D))
                        {
                            currentDir = myDirection.right;
                            PlayAnimation("Down_Right");
                            spriteDirection += new Vector2(1, 0);

                            xVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (xVelocity > maxSpeed) { xVelocity = maxSpeed; }
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
                                PlayAnimation("Down_Idle_Right");
                            }
                            else
                            {
                                PlayAnimation("Down_Idle_Left");
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
                        maxFall = false;
                        if (xVelocity > fric) { xVelocity -= fric; }
                        else if (xVelocity < -fric) { xVelocity += fric; }
                        else { xVelocity = 0; }
                        if (k.IsKeyDown(Keys.A))
                        {
                            currentDir = myDirection.left;
                            PlayAnimation("Up_Left");
                            spriteDirection += new Vector2(-1, 0);

                            xVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (xVelocity < -maxSpeed) { xVelocity = -maxSpeed; }
                        }
                        else if (k.IsKeyDown(Keys.D))
                        {
                            currentDir = myDirection.right;
                            PlayAnimation("Up_Right");
                            spriteDirection += new Vector2(1, 0);

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
                                PlayAnimation("Up_Idle_Right");
                            }
                            else
                            {
                                PlayAnimation("Up_Idle_Left");
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
                        maxFall = false;
                        if (yVelocity > fric) { yVelocity -= fric; }
                        else if (yVelocity < -fric) { yVelocity += fric; }
                        else { yVelocity = 0; }
                        if (k.IsKeyDown(Keys.W))
                        {
                            currentDir = myDirection.up;
                            PlayAnimation("Right_Up");
                            spriteDirection += new Vector2(0, -1);

                            yVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (yVelocity < -maxSpeed) { yVelocity = -maxSpeed; }
                        }
                        else if (k.IsKeyDown(Keys.S))
                        {
                            currentDir = myDirection.down;
                            PlayAnimation("Right_Down");
                            spriteDirection += new Vector2(0, 1);

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
                                PlayAnimation("Right_Idle_Up");
                            }
                            else
                            {
                                PlayAnimation("Right_Idle_Down");
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
                        maxFall = false;
                        if (yVelocity > fric) { yVelocity -= fric; }
                        else if (yVelocity < -fric) { yVelocity += fric; }
                        else { yVelocity = 0; }
                        if (k.IsKeyDown(Keys.W))
                        {
                            currentDir = myDirection.up;
                            PlayAnimation("Left_Up");
                            spriteDirection += new Vector2(0, -1);

                            yVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                            if (yVelocity < -maxSpeed) { yVelocity = -maxSpeed; }
                        }
                        else if (k.IsKeyDown(Keys.S))
                        {
                            currentDir = myDirection.down;
                            PlayAnimation("Left_Down");
                            spriteDirection += new Vector2(0, 1);

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
                                PlayAnimation("Left_Idle_Up");
                            }
                            else
                            {
                                PlayAnimation("Left_Idle_Down");
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
            if (k.IsKeyDown(Keys.Up) && grav != gravDirection.Up)
            {
                ObjRectWidth = 30;
                ObjRectHeight = 54;
                grav = gravDirection.Up;
                fallingLoop.End();
                if (GameVariables.gFX2.State != SoundState.Playing)
                {
                    GameVariables.gFX2.Play();
                }
                if (currentDir == myDirection.right)
                {
                    PlayAnimation("Up_Idle_Right");
                }
                else
                {
                    PlayAnimation("Up_Idle_Left");
            }
            }
            if (k.IsKeyDown(Keys.Down) && grav != gravDirection.Down)
            {
                ObjRectWidth = 30;
                ObjRectHeight = 54;
                grav = gravDirection.Down;
                fallingLoop.End();
                if (GameVariables.gFX2.State != SoundState.Playing)
                {
                    GameVariables.gFX2.Play();
                }
                if (currentDir == myDirection.right)
                {
                    PlayAnimation("Down_Idle_Right");
                }
                else
                {
                    PlayAnimation("Down_Idle_Left");
                }
            }
            if (k.IsKeyDown(Keys.Right) && grav != gravDirection.Right)
            {
                ObjRectWidth = 54;
                ObjRectHeight = 30;
                grav = gravDirection.Right;
                fallingLoop.End();
                if (GameVariables.gFX2.State != SoundState.Playing)
                {
                    GameVariables.gFX2.Play();
                }
                if (currentDir == myDirection.up)
                {
                    PlayAnimation("Right_Idle_Up");
                }
                else
                {
                    PlayAnimation("Right_Idle_Down");
                }
            }
            if (k.IsKeyDown(Keys.Left) && grav != gravDirection.Left)
            {
                ObjRectWidth = 54;
                ObjRectHeight = 30;
                grav = gravDirection.Left;
                fallingLoop.End();
                if (GameVariables.gFX2.State != SoundState.Playing)
                {
                    GameVariables.gFX2.Play();
                }
                if (currentDir == myDirection.up)
                {
                    PlayAnimation("Left_Idle_Up");
                }
                else
                {
                    PlayAnimation("Left_Idle_Down");
                }
            }

            ObjRectX = (int)ObjPos.X;
            ObjRectY = (int)ObjPos.Y;
            //HitboxUpdate();
        }

        //public void HitboxUpdate()
        //{
        //    topHit.ObjRectY = ObjRectY - 5;
        //    topHit.ObjRectX = ObjRectX + 5;
        //    bottHit.ObjRectY = ObjRectY + ObjRect.Height;
        //    bottHit.ObjRectX = ObjRectX + 5;
        //    rightHit.ObjRectY = ObjRectY + 5;
        //    rightHit.ObjRectX = ObjRectX + ObjRect.Width;
        //    leftHit.ObjRectY = ObjRectY + 5;
        //    leftHit.ObjRectX = ObjRectX - 5;
        //}

        public override void spriteDraw(SpriteBatch s)
        {
            if (alive)
            {
                base.Draw(s);
            }
        }

        public void FallingSound(SoundEffectInstance FallingAcceleration, SoundEffectInstance FallingLoop)
        {
            if ( !maxFall )
            {
                FallingAcceleration.Play();
                maxFall = true;
            }
            else if (FallingAcceleration.State == SoundState.Stopped)
            {
                FallingLoop.Play();
            }
        }

        public void Collisions(KeyboardState k, KeyboardState p, World w, StreamReader s, ContentManager content, SoundEffectInstance landing )
        {
            inAir = true;
            List<GameObject> objs = w.levels[w.currentLevel].objects;
            List<Enemy> enms = w.levels[w.currentLevel].enemies;
            foreach (Enemy en in enms)
            {
                if (isColliding(en))
                {
                    alive = false;
                }
            }
            foreach (GameObject obj in objs)
            {
                if ((obj is Door))
                {
                    Door temp = (Door)obj;
                    temp.Open(this);
                }
                if (isColliding(obj) && obj.isDangerous)
                {
                    alive = false;
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
                                landing.Play();
                            }
                        }
                        ObjPos.X = obj.ObjRect.Right;
                        xVelocity = 0;
                    }
                    // Right middle.
                    else if (obj.ObjRect.Contains(ObjRect.Right, ObjRect.Center.Y))
                    {
                        if (grav == gravDirection.Right)
                        {
                            inAir = false;
                            if (xVelocity > 1)
                            {
                                landing.Play();
                            }
                        }
                        ObjPos.X = obj.ObjRect.Left - ObjRect.Width;
                        xVelocity = 0;
                    }
                    // Top middle.
                    else if (obj.ObjRect.Contains(ObjRect.Center.X, ObjRect.Top))
                    {
                        if (grav == gravDirection.Up)
                        {
                            inAir = false;
                            if (yVelocity < -1)
                            {
                                landing.Play();
                            }
                        }
                        ObjPos.Y = obj.ObjRect.Bottom;
                        yVelocity = 0;
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
                                    landing.Play();
                                }
                            }
                        }
                        ObjPos.Y = obj.ObjRect.Top - ObjRect.Height;
                        yVelocity = 0;
                    }
                    // Top left corner.
                    else if (obj.ObjRect.Contains(ObjRect.X + 5, ObjRect.Y + 5))
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
                    else if (obj.ObjRect.Contains(ObjRect.Right - 5, ObjRect.Y + 5))
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
                    else if (obj.ObjRect.Contains(ObjRect.Left + 5, ObjRect.Bottom + 5))
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
                    else if (obj.ObjRect.Contains(ObjRect.Right - 5, ObjRect.Bottom - 5))
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
            spriteDirection = Vector2.Zero;
            //Movement(Keyboard.GetState(), gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            spriteDirection *= (float)GameVariables.playerMaxSpeed;

            ObjPos += (spriteDirection * deltaTime);

            base.Update(gameTime);
        }
    }
}

