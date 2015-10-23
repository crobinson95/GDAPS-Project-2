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
        int energy;
        public HitBox topHit;
        public HitBox bottHit;
        public HitBox rightHit;
        public HitBox leftHit;

        public Player(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
            grav = gravDirection.Down;
            gravity = GameVariables.gravity;
            xVelocity = 0;
            yVelocity = 0;
            inAir = true;
            alive = true;
            topHit = new HitBox((int)ObjPos.X + 5, ObjRect.Y + 5, ObjRect.Width - 10, 5);
            bottHit = new HitBox((int)ObjPos.X + 5, ObjRect.Height, ObjRect.Width - 10, 5);
            rightHit = new HitBox((int)ObjPos.X, ObjRect.Y, 5, ObjRect.Height - 10);
            leftHit = new HitBox((int)ObjPos.X, ObjRect.Y, 5, ObjRect.Height - 10);
        }

        public void Movement(KeyboardState k, GameTime g)
        {

            ObjPos += new Vector2(xVelocity, yVelocity);
            switch (grav)
            {
                case gravDirection.Down:
                    if (inAir)
                    {
                        yVelocity += (float)gravity;
                    }
                    else
                    {
                        if (xVelocity > 0) { xVelocity -= .1f; }
                        if (xVelocity < 0) { xVelocity += .1f; }
                    }

                    if (k.IsKeyDown(Keys.W))
                    {

                    }
                    if (k.IsKeyDown(Keys.S))
                    {

                    }
                    if (k.IsKeyDown(Keys.A))
                    {
                        ObjPos.X -= (float)GameVariables.playerSpeed * (float)g.ElapsedGameTime.TotalSeconds;
                        xVelocity -= .5f;
                        if (xVelocity < -5f) { xVelocity = -5f; }
                    }
                    if (k.IsKeyDown(Keys.D))
                    {
                        ObjPos.X += (float)GameVariables.playerSpeed * (float)g.ElapsedGameTime.TotalSeconds;
                        xVelocity += .5f;
                        if (xVelocity > 5f) { xVelocity = 5f; }

                    }
                    if (k.IsKeyDown(Keys.Space))
                    {
                        if (inAir == false)
                        {
                            ObjPos.Y -= 20;
                            yVelocity = -4f;
                            inAir = true;
                        }
                    }
                    break;

                case gravDirection.Up:
                    if (inAir)
                    {
                        yVelocity -= (float)gravity;
                    }
                    else
                    {
                        if (xVelocity > 0) { xVelocity -= .1f; }
                        if (xVelocity < 0) { xVelocity += .1f; }
                    }

                    if (k.IsKeyDown(Keys.W))
                    {

                    }
                    if (k.IsKeyDown(Keys.S))
                    {

                    }
                    if (k.IsKeyDown(Keys.A))
                    {
                        ObjPos.X -= (float)GameVariables.playerSpeed * (float)g.ElapsedGameTime.TotalSeconds;
                    }
                    if (k.IsKeyDown(Keys.D))
                    {
                        ObjPos.X += (float)GameVariables.playerSpeed * (float)g.ElapsedGameTime.TotalSeconds;
                    }
                    if (k.IsKeyDown(Keys.Space))
                    {
                        if (inAir == false)
                        {
                            ObjPos.Y += 20;
                            yVelocity = 4f;
                            inAir = true;
                        }
                    }
                    break;


                case gravDirection.Right:
                    if (inAir)
                    {
                        xVelocity += (float)gravity;
                    }
                    else
                    {
                        if (yVelocity > 0) { yVelocity -= .1f; }
                        if (yVelocity < 0) { yVelocity += .1f; }
                    }
                    if (k.IsKeyDown(Keys.W))
                    {

                    }
                    if (k.IsKeyDown(Keys.S))
                    {

                    }
                    if (k.IsKeyDown(Keys.A))
                    {
                        ObjPos.Y += (float)GameVariables.playerSpeed * (float)g.ElapsedGameTime.TotalSeconds;
                    }
                    if (k.IsKeyDown(Keys.D))
                    {
                        ObjPos.Y -= (float)GameVariables.playerSpeed * (float)g.ElapsedGameTime.TotalSeconds;
                    }
                    if (k.IsKeyDown(Keys.Space))
                    {
                        if (inAir == false)
                        {
                            ObjPos.X -= 20;
                            xVelocity = -4f;
                            inAir = true;
                        }
                    }
                    break;


                case gravDirection.Left:
                    if (inAir)
                    {
                        xVelocity -= (float)gravity;
                    }
                    else
                    {
                        if (yVelocity > 0) { yVelocity -= .1f; }
                        if (yVelocity < 0) { yVelocity += .1f; }
                    }
                    if (k.IsKeyDown(Keys.W))
                    {

                    }
                    if (k.IsKeyDown(Keys.S))
                    {

                    }
                    if (k.IsKeyDown(Keys.A))
                    {
                        ObjPos.Y -= (float)GameVariables.playerSpeed * (float)g.ElapsedGameTime.TotalSeconds;
                    }
                    if (k.IsKeyDown(Keys.D))
                    {
                        ObjPos.Y += (float)GameVariables.playerSpeed * (float)g.ElapsedGameTime.TotalSeconds;
                    }
                    if (k.IsKeyDown(Keys.Space))
                    {
                        if (inAir == false)
                        {
                            ObjPos.X += 20;
                            xVelocity = 4f;
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
            HitboxUpdate();
        }

        public void HitboxUpdate()
        {
            topHit.ObjRectY = ObjRectY - 5;
            topHit.ObjRectX = ObjRectX + 5;
            bottHit.ObjRectY = ObjRectY + ObjRect.Height;
            bottHit.ObjRectX = ObjRectX + 5;
            rightHit.ObjRectY = ObjRectY + 5;
            rightHit.ObjRectX = ObjRectX + ObjRect.Width;
            leftHit.ObjRectY = ObjRectY + 5;
            leftHit.ObjRectX = ObjRectX - 5;

        }

        public override void spriteDraw(SpriteBatch s)
        {
            if (alive)
            {
                base.spriteDraw(s);
            }
        }

        public void Collisions(List<GameObject> objs, KeyboardState k, KeyboardState p, World w)
        {
            foreach (GameObject obj in objs)
            {
                if (isColliding(obj) && obj.isDangerous)
                {
                    alive = false;
                    isDead();
                }
                else if (isColliding(obj))
                {
                    if(obj is Door)
                    {
                        Door temp = (Door)obj;
                        if (Game1.SingleKeyPress(Keys.E, k, p))
                        {
                            w.currentLevel = temp.destination;
                            ObjRectX = w.Levels[w.currentLevel].objects[0].ObjRectX;
                            ObjRectY = w.Levels[w.currentLevel].objects[0].ObjRectY;
                        }

                    }
                    else
                    {
                        if (topHit.isColliding(obj))
                        {
                            if (obj is Player)
                            { }
                            else
                            {
                                ObjPos.Y = obj.ObjRectY + obj.ObjRect.Height;
                                yVelocity = 0;
                                if (grav == gravDirection.Up) { inAir = false; }
                            }
                        }
                        if (bottHit.isColliding(obj))
                        {
                            if (obj is Player)
                            { }
                            else
                            {
                                ObjPos.Y = obj.ObjRectY - ObjRect.Height;
                                yVelocity = 0;
                                if (grav == gravDirection.Down) { inAir = false; }
                            }
                        }
                        if (rightHit.isColliding(obj))
                        {
                            if (obj is Player)
                            { }
                            else
                            {
                                ObjPos.X = obj.ObjRectX - ObjRect.Width;
                                xVelocity = 0;
                                if (grav == gravDirection.Right) { inAir = false; }
                            }
                        }
                        if (leftHit.isColliding(obj))
                        {
                            if (obj is Player)
                            { }
                            else
                            {
                                ObjPos.X = obj.ObjRectX + obj.ObjRect.Width;
                                xVelocity = 0;
                                if (grav == gravDirection.Left) { inAir = false; }
                            }
                        }
                        ObjRectX = (int)ObjPos.X;
                        ObjRectY = (int)ObjPos.Y;
                        }
                }

            }
        }

        public void isDead() { }
    }
}

