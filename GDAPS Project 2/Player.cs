using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS_Project_2
{
    class Player : MovableGameObject
    {
        float fric = (float)GameVariables.friction;
        float jump = (float)GameVariables.jump;
        float accel = (float)GameVariables.playerAcceleration;
        float maxSpeed = (float)GameVariables.playerMaxSpeed;

        int energy;
        //public HitBox topHit;
        //public HitBox bottHit;
        //public HitBox rightHit;
        //public HitBox leftHit;

        public Player(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
            grav = gravDirection.Down;
            gravity = GameVariables.gravity;
            xVelocity = 0;
            yVelocity = 0;
            inAir = true;
            alive = true;
            //topHit = new HitBox((int)ObjPos.X + 5, ObjRect.Y + 5, ObjRect.Width - 10, 5);
            //bottHit = new HitBox((int)ObjPos.X + 5, ObjRect.Height, ObjRect.Width - 10, 5);
            //rightHit = new HitBox((int)ObjPos.X, ObjRect.Y, 5, ObjRect.Height - 10);
            //leftHit = new HitBox((int)ObjPos.X, ObjRect.Y, 5, ObjRect.Height - 10);
        }

        public void Movement(KeyboardState k, GameTime g)
        {

            ObjPos += new Vector2(xVelocity, yVelocity);
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

                    if (k.IsKeyDown(Keys.W))
                    {

                    }
                    if (k.IsKeyDown(Keys.S))
                    {

                    }
                    if (k.IsKeyDown(Keys.A))
                    {
                        xVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity < -maxSpeed) { xVelocity = -maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.D))
                    {
                        xVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity > maxSpeed) { xVelocity = maxSpeed; }

                    }
                    if (k.IsKeyDown(Keys.Space))
                    {
                        if (inAir == false)
                        {
                            ObjPos.Y -= 5;
                            yVelocity = -jump;
                            inAir = true;
                        }
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

                    if (k.IsKeyDown(Keys.W))
                    {

                    }
                    if (k.IsKeyDown(Keys.S))
                    {

                    }
                    if (k.IsKeyDown(Keys.A))
                    {
                        xVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity < -maxSpeed) { xVelocity = -maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.D))
                    {
                        xVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (xVelocity > maxSpeed) { xVelocity = maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.Space))
                    {
                        if (inAir == false)
                        {
                            ObjPos.Y += 5;
                            yVelocity = jump;
                            inAir = true;
                        }
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
                    if (k.IsKeyDown(Keys.W))
                    {
                        yVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity < -maxSpeed) { yVelocity = -maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.S))
                    {
                        yVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity > maxSpeed) { yVelocity = maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.A))
                    {

                    }
                    if (k.IsKeyDown(Keys.D))
                    {

                    }
                    if (k.IsKeyDown(Keys.Space))
                    {
                        if (inAir == false)
                        {
                            ObjPos.X -= 5;
                            xVelocity = -jump;
                            inAir = true;
                        }
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
                    if (k.IsKeyDown(Keys.W))
                    {
                        yVelocity -= accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity < -maxSpeed) { yVelocity = -maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.S))
                    {
                        yVelocity += accel * (float)g.ElapsedGameTime.TotalSeconds;
                        if (yVelocity > maxSpeed) { yVelocity = maxSpeed; }
                    }
                    if (k.IsKeyDown(Keys.A))
                    {

                    }
                    if (k.IsKeyDown(Keys.D))
                    {

                    }
                    if (k.IsKeyDown(Keys.Space))
                    {
                        if (inAir == false)
                        {
                            ObjPos.X += 5;
                            xVelocity = jump;
                            inAir = true;
                        }
                    }
                    break;
            }
            if (k.IsKeyDown(Keys.Up))
            {
                grav = gravDirection.Up;
            }
            if (k.IsKeyDown(Keys.Down))
            {
                grav = gravDirection.Down;
            }
            if (k.IsKeyDown(Keys.Right))
            {
                grav = gravDirection.Right;
            }
            if (k.IsKeyDown(Keys.Left))
            {
                grav = gravDirection.Left;
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
                base.spriteDraw(s);
            }
        }

        public void Collisions(List<GameObject> objs, KeyboardState k, KeyboardState p, World w)
        {
            inAir = true;
            foreach (GameObject obj in objs)
            {
                if (isColliding(obj) && obj.isDangerous)
                {
                    alive = false;
                    isDead();
                }
                else if (isColliding(obj))
                {
                    if (obj is Door)
                    {
                        Door temp = (Door)obj;
                        if (Game1.SingleKeyPress(Keys.E, k, p))
                        {
                            w.currentLevel = temp.destination;
                            ObjPos.X = w.Levels[w.currentLevel].playerSpawn.X;
                            ObjPos.Y = w.Levels[w.currentLevel].playerSpawn.Y;
                            xVelocity = 0.0f;
                            yVelocity = 0.0f;
                        }
                    }
                    // Left middle.
                    else if (obj.ObjRect.Contains(ObjRect.Left, ObjRect.Center.Y))
                    {
                        if (grav == gravDirection.Left)
                        {
                            inAir = false;
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
                        }
                        ObjPos.Y = obj.ObjRect.Bottom;
                        yVelocity = 0;
                    }
                    // Bottom middle.
                    else if (obj.ObjRect.Contains(ObjRect.Center.X, ObjRect.Bottom))
                    {
                        if (grav == gravDirection.Down)
                        {
                            inAir = false;
                        }
                        ObjPos.Y = obj.ObjRect.Top - ObjRect.Height;
                        yVelocity = 0;
                    }
                    // Top left corner.
                    else if (obj.ObjRect.Contains(ObjRect.X, ObjRect.Y))
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
                    else if (obj.ObjRect.Contains(ObjRect.Right, ObjRect.Y))
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
                    else if (obj.ObjRect.Contains(ObjRect.Left, ObjRect.Bottom))
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
                    else if (obj.ObjRect.Contains(ObjRect.Right, ObjRect.Bottom))
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
                    //}
                    //else
                    //{
                    //    if (topHit.isColliding(obj))
                    //    {
                    //        if (obj is Player)
                    //        { }
                    //        {
                    //            if (grav == gravDirection.Up)
                    //            {
                    //                ObjPos.Y = obj.ObjRectY + obj.ObjRect.Height;
                    //                yVelocity = 0;
                    //                inAir = false;
                    //            }
                    //        }
                    //    }
                    //    else if (bottHit.isColliding(obj))
                    //    {
                    //        if (obj is Player)
                    //        { }
                    //        else
                    //        {
                    //            ObjPos.Y = obj.ObjRectY - ObjRect.Height;
                    //            yVelocity = 0;
                    //            if (grav == gravDirection.Down) { inAir = false; }
                    //        }
                    //    }
                    //    else if (rightHit.isColliding(obj))
                    //    {
                    //        if (obj is Player)
                    //        { }
                    //        else
                    //        {
                    //            ObjPos.X = obj.ObjRectX - ObjRect.Width;
                    //            xVelocity = 0;
                    //            if (grav == gravDirection.Right) { inAir = false; }
                    //        }
                    //    }
                    //    else if (leftHit.isColliding(obj))
                    //    {
                    //        if (obj is Player)
                    //        { }
                    //        else
                    //        {
                    //            ObjPos.X = obj.ObjRectX + obj.ObjRect.Width;
                    //            xVelocity = 0;
                    //            if (grav == gravDirection.Left) { inAir = false; }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        inAir = true;
                    //    }
                    ObjRectX = (int)ObjPos.X;
                    ObjRectY = (int)ObjPos.Y; 
                }
            }
        }

        public void isDead() { }
    }
}

