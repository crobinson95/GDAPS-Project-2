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
        public string imageLoc;
        int energy;
        bool alive = true;

        public Player(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
            imageLoc = GameVariables.imgPlayer;
            grav = gravDirection.Down;
            gravity = GameVariables.gravity;
            xVelocity = 0;
            yVelocity = 0;
            inAir = true;
            alive = true;
        }

        public void Movement(KeyboardState k, GameTime g)
        {

            ObjPos += new Vector2(xVelocity, yVelocity);
            switch (grav)
            {
                case gravDirection.Down:
                    if (xVelocity > 0) { xVelocity -= .1f; }
                    if (xVelocity < 0) { xVelocity += .1f; }
                    if (inAir)
                    {
                        yVelocity += (float)gravity;
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
                yVelocity = 0;
                xVelocity = 0;
            }
            if (k.IsKeyDown(Keys.Down))
            {
                grav = gravDirection.Down;
                yVelocity = 0;
                xVelocity = 0;
            }
            if (k.IsKeyDown(Keys.Right))
            {
                grav = gravDirection.Right;
                yVelocity = 0;
                xVelocity = 0;
            }
            if (k.IsKeyDown(Keys.Left))
            {
                grav = gravDirection.Left;
                yVelocity = 0;
                xVelocity = 0;
            }

            ObjRectX = (int)ObjPos.X;
            ObjRectY = (int)ObjPos.Y;

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
                        switch (grav)
                        {
                            case (gravDirection.Down):
                                ObjPos.Y = obj.ObjRectY - ObjRect.Height;
                                break;
                            case (gravDirection.Up):
                                ObjPos.Y = obj.ObjRectY + obj.ObjRect.Height;
                                break;
                            case (gravDirection.Right):
                                ObjPos.X = obj.ObjRectX - ObjRect.Width;
                                break;
                            case (gravDirection.Left):
                                ObjPos.X = obj.ObjRectX + obj.ObjRect.Width;
                                break;
                        }

                        inAir = false;
                        ObjRectX = (int)ObjPos.X;
                        ObjRectY = (int)ObjPos.Y;
                        }
                }

            }
        }

        public void isDead() { }
    }
}

