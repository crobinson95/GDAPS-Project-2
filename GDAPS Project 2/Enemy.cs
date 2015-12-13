using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace GDAPS_Project_2
{
    class Enemy : MovableGameObject
    {
        protected float fric = (float)GameVariables.friction;
        protected float jump = (float)GameVariables.jump;
        protected float accel = (float)GameVariables.playerAcceleration;
        protected float maxSpeed = (float)GameVariables.playerMaxSpeed / 2;

        Stopwatch atime;

        AnimatedTexture Move;

        public Point origin;

        protected Player player;

        

        public enum Direction
        {
            left,
            right,
            up,
            down
        }

        protected Direction currentDir = new Direction();

        public AudioEmitter enemyEmitter;
        public SoundEffectInstance enemySound = GameVariables.Robot.CreateInstance();

        public Enemy(ContentManager Content, int x, int y, int w, int h, Player p)
            : base(x, y, w, h)
        {
            grav = gravDirection.Down;
            gravity = GameVariables.gravity;
            currentDir = Direction.right;
            xVelocity = 0;
            yVelocity = 0;
            inAir = true;
            alive = true;
            origin = new Point(x, y);
            isDangerous = true;
            player = p;
            Move = new AnimatedTexture(Content, @"ContentFiles/Images/Sprites/enemy_sri", 10, 1f);
            atime = new Stopwatch();
            enemyEmitter = new AudioEmitter();
            enemyEmitter.Position = new Vector3(x, y, 0.0f);            
        }



        public virtual void Movement(GameTime g)
        {

            grav = player.grav;
            ObjPos += new Vector2(xVelocity, yVelocity);
            if (grav == gravDirection.Down | grav == gravDirection.Up)
            {
                ObjRectHeight = 70;
                ObjRectWidth = 50;

                if (ObjPos.X > origin.X + 100)
                {
                    currentDir = Direction.left;
                }
                if (ObjPos.X < origin.X - 100)
                {
                    currentDir = Direction.right;
                }
            }
            if (grav == gravDirection.Left | grav == gravDirection.Right)
            {
                ObjRectHeight = 50;
                ObjRectWidth = 70;

                if (ObjPos.Y > origin.Y + 100)
                {
                    currentDir = Direction.up;
                }
                if (ObjPos.Y < origin.Y - 100)
                {
                    currentDir = Direction.down;
                }
            }
            switch (grav)
            {
                case gravDirection.Down:
                    yVelocity += (float)gravity;

                    if (!inAir)
                    {
                        yVelocity -= (float)gravity;
                        if (xVelocity > fric) { xVelocity -= fric; }
                        else if (xVelocity < -fric) { xVelocity += fric; }
                        else { xVelocity = 0; }
                    }
                    if (currentDir == Direction.left)    //Move left
                    {

                        xVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity < -maxSpeed) { xVelocity = -maxSpeed; }
                    }
                    if (currentDir == Direction.right)    //Move right
                    {

                        xVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity > maxSpeed) { xVelocity = maxSpeed; }

                    }
                    break;

                case gravDirection.Up:
                    yVelocity -= (float)gravity;

                    if (!inAir)
                    {
                        yVelocity += (float)gravity;
                        if (xVelocity > fric) { xVelocity -= fric; }
                        else if (xVelocity < -fric) { xVelocity += fric; }
                        else { xVelocity = 0; }
                    }
                    if (currentDir == Direction.left)
                    {
                        xVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity < -maxSpeed) { xVelocity = -maxSpeed; }
                    }
                    if (currentDir == Direction.right)
                    {
                        xVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity > maxSpeed) { xVelocity = maxSpeed; }
                    }
                    break;


                case gravDirection.Right:
                    xVelocity += (float)gravity;

                    if (!inAir)
                    {
                        xVelocity -= (float)gravity;
                        if (yVelocity > fric) { yVelocity -= fric; }
                        else if (yVelocity < -fric) { yVelocity += fric; }
                        else { yVelocity = 0; }
                    }
                    if (currentDir == Direction.up)
                    {
                        yVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity < -maxSpeed) { yVelocity = -maxSpeed; }
                    }
                    if (currentDir == Direction.down)
                    {
                        yVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity > maxSpeed) { yVelocity = maxSpeed; }
                    }

                    break;


                case gravDirection.Left:
                    xVelocity -= (float)gravity;
                    if (!inAir)
                    {
                        xVelocity += (float)gravity;
                        if (yVelocity > fric) { yVelocity -= fric; }
                        else if (yVelocity < -fric) { yVelocity += fric; }
                        else { yVelocity = 0; }
                    }
                    if (currentDir == Direction.up)
                    {
                        yVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity < -maxSpeed) { yVelocity = -maxSpeed; }
                    }
                    if (currentDir == Direction.down)
                    {
                        yVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity > maxSpeed) { yVelocity = maxSpeed; }
                    }

                    break;
            }

            ObjRectX = (int)ObjPos.X;
            ObjRectY = (int)ObjPos.Y;
            enemyEmitter.Position = new Vector3(ObjPos.X, ObjPos.Y, 0.0f);
        }




        public virtual void Collisions(List<GameObject> objs, KeyboardState k, KeyboardState p, World w)
        {
            inAir = true;
            foreach (GameObject obj in objs)
            {
                if (isColliding(obj) && obj.isDangerous)
                {
                    alive = false;
                }

                else if (obj is Enemy | obj is Door | obj is Panel)
                {
                }
                // Left middle.
                else if (obj.ObjRect.Contains(ObjRect.Left, ObjRect.Center.Y))
                {
                    if (grav == gravDirection.Left)
                    {
                        inAir = false;
                    }
                    if (grav == gravDirection.Down | grav == gravDirection.Up)
                    {
                        currentDir = Direction.right;
                    }
                    ObjPos.X = obj.ObjRect.Right;
                    if (xVelocity < 0) { xVelocity = 0; }
                }
                // Right middle.
                else if (obj.ObjRect.Contains(ObjRect.Right, ObjRect.Center.Y))
                {
                    if (grav == gravDirection.Right)
                    {
                        inAir = false;
                    }
                    if (grav == gravDirection.Down | grav == gravDirection.Up)
                    {
                        currentDir = Direction.left;
                    }
                    ObjPos.X = obj.ObjRect.Left - ObjRect.Width;
                    if (xVelocity > 0) { xVelocity = 0; }
                }
                // Top middle.
                else if (obj.ObjRect.Contains(ObjRect.Center.X, ObjRect.Top))
                {
                    if (grav == gravDirection.Up)
                    {
                        inAir = false;
                    }
                    if (grav == gravDirection.Left | grav == gravDirection.Right)
                    {
                        currentDir = Direction.down;
                    }
                    ObjPos.Y = obj.ObjRect.Bottom;
                    if (yVelocity < 0) { yVelocity = 0; }
                }
                // Bottom middle.
                else if (obj.ObjRect.Contains(ObjRect.Center.X, ObjRect.Bottom))
                {
                    if (grav == gravDirection.Down)
                    {
                        inAir = false;
                    }
                    if (grav == gravDirection.Left | grav == gravDirection.Right)
                    {
                        currentDir = Direction.up;
                    }
                    ObjPos.Y = obj.ObjRect.Top - ObjRect.Height;
                    if (yVelocity > 0) { yVelocity = 0; }
                }
                // Top left corner.
                else if (obj.ObjRect.Contains(ObjRect.X, ObjRect.Y))
                {
                    if (grav == gravDirection.Up)
                    {
                        inAir = false;
                        ObjPos.Y = obj.ObjRect.Bottom;
                        if (yVelocity < 0) { yVelocity = 0; }
                    }
                    else if (grav == gravDirection.Left)
                    {
                        inAir = false;
                        ObjPos.X = obj.ObjRect.Right;
                        if (xVelocity < 0) { xVelocity = 0; }
                    }
                    else
                    {
                        if (xVelocity < 0) { xVelocity = 0; ObjPos.X = obj.ObjRect.Right; }
                        else if (yVelocity < 0) { yVelocity = 0; ObjPos.Y = obj.ObjRect.Bottom; }
                    }
                }
                // Top right corner.
                else if (obj.ObjRect.Contains(ObjRect.Right, ObjRect.Y))
                {
                    if (grav == gravDirection.Up)
                    {
                        inAir = false;
                        ObjPos.Y = obj.ObjRect.Bottom;
                        if (yVelocity < 0) { yVelocity = 0; }
                    }
                    else if (grav == gravDirection.Right)
                    {
                        inAir = false;
                        ObjPos.X = obj.ObjRect.Left - ObjRect.Width;
                        if (xVelocity > 0) { xVelocity = 0; }
                    }
                    else
                    {
                        if (xVelocity > 0) { xVelocity = 0; ObjPos.X = obj.ObjRect.Left - ObjRect.Width; }
                        else if (yVelocity < 0) { yVelocity = 0; ObjPos.Y = obj.ObjRect.Bottom; }
                    }
                }
                // Bottom left corner.
                else if (obj.ObjRect.Contains(ObjRect.Left, ObjRect.Bottom))
                {
                    if (grav == gravDirection.Down)
                    {
                        inAir = false;
                        ObjPos.Y = obj.ObjRect.Top - ObjRect.Height;
                        if (yVelocity > 0) { yVelocity = 0; }
                    }
                    else if (grav == gravDirection.Left)
                    {
                        inAir = false;
                        ObjPos.X = obj.ObjRect.Right;
                        if (xVelocity < 0) { xVelocity = 0; }
                    }
                    else
                    {
                        if (xVelocity < 0) { xVelocity = 0; ObjPos.X = obj.ObjRect.Right; }
                        else if (yVelocity > 0) { yVelocity = 0; ObjPos.Y = obj.ObjRect.Top - ObjRect.Height; }
                    }
                }
                // Bottom right corner.
                else if (obj.ObjRect.Contains(ObjRect.Right, ObjRect.Bottom))
                {
                    if (grav == gravDirection.Down)
                    {
                        inAir = false;
                        ObjPos.Y = obj.ObjRect.Top - ObjRect.Height;
                        if (yVelocity > 0) { yVelocity = 0; }
                    }
                    else if (grav == gravDirection.Right)
                    {
                        inAir = false;
                        ObjPos.X = obj.ObjRect.Left - ObjRect.Width;
                        if (xVelocity > 0) { xVelocity = 0; }
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

        public override void spriteDraw(SpriteBatch s)
        {
            if (alive)
            {
                if (grav == gravDirection.Down)
                {
                    if (currentDir == Direction.left)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 1, new Vector2(ObjRectX, ObjRectY), true);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                    if (currentDir == Direction.right)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 2, new Vector2(ObjRectX, ObjRectY), true);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                }

                if (grav == gravDirection.Up)
                {
                    if (currentDir == Direction.left)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 2, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, ObjRectWidth * 1.5f), true, MathHelper.Pi);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                    if (currentDir == Direction.right)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 1, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, ObjRectWidth * 1.5f), true, MathHelper.Pi);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                }

                if (grav == gravDirection.Left)
                {
                    if (currentDir == Direction.up)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 1, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, ObjRectWidth), true, MathHelper.Pi /2);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                    if (currentDir == Direction.down)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 2, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, ObjRectWidth), true, MathHelper.Pi /2);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                }

                if (grav == gravDirection.Right)
                {
                    if (currentDir == Direction.up)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 2, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, 0), true, -MathHelper.Pi/2);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                    if (currentDir == Direction.down)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 1, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, 0), true, -MathHelper.Pi/2);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                }
            }
        }
    }
}