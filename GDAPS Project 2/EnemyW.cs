using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace GDAPS_Project_2
{
    class EnemyW: Enemy
    {
        Rectangle vision;
        bool Attacking;
        AnimatedTexture Move;
        Stopwatch atime;

        public EnemyW(ContentManager Content, int x, int y, int w, int h, Player p)
            : base(Content, x, y, w, h, p)
        {
            grav = gravDirection.Down;
            gravity = GameVariables.gravity;
            currentDir = Direction.right;
            xVelocity = 0;
            yVelocity = 0;
            inAir = true;
            alive = true;
            isDangerous = true;
            origin = new Point(x, y);
            Move = new AnimatedTexture(Content, @"ContentFiles/Images/Sprites/enemyw_sri", 4, .1f);
            vision = new Rectangle(0, 0, 100, 500);
            atime = new Stopwatch();
            player = p;

        }


        public override void Movement(GameTime g)
        {
            VisionUpdate();
            grav = player.grav;
            ObjPos += new Vector2(xVelocity, yVelocity);
            if (grav == gravDirection.Down | grav == gravDirection.Up)
            {
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
                    //yVelocity += (float)gravity;

                    //if (!inAir)
                    //{
                    //    yVelocity -= (float)gravity;
                    //    if (xVelocity > fric) { xVelocity -= fric; }
                    //    else if (xVelocity < -fric) { xVelocity += fric; }
                    //    else { xVelocity = 0; }
                    //}
                    if(ObjRect.Y > origin.Y)
                    {
                        yVelocity -= (float)gravity;
                    }
                    if (ObjRect.Y < origin.Y)
                    {
                        //yVelocity += (float)gravity;
                    }
                    if (ObjRect.Y <= origin.Y)
                    {
                        Attacking = false;
                        yVelocity = 0;
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
                    //yVelocity -= (float)gravity;

                    //if (!inAir)
                    //{
                    //    yVelocity += (float)gravity;
                    //    if (xVelocity > fric) { xVelocity -= fric; }
                    //    else if (xVelocity < -fric) { xVelocity += fric; }
                    //    else { xVelocity = 0; }
                    //}
                    if (ObjRect.Y < origin.Y)
                    {
                        yVelocity -= (float)gravity;
                    }
                    if (ObjRect.Y == origin.Y)
                    {
                        yVelocity = 0;
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
                    //xVelocity += (float)gravity;

                    //if (!inAir)
                    //{
                    //    xVelocity -= (float)gravity;
                    //    if (yVelocity > fric) { yVelocity -= fric; }
                    //    else if (yVelocity < -fric) { yVelocity += fric; }
                    //    else { yVelocity = 0; }
                    //}
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
                    //xVelocity -= (float)gravity;
                    //if (!inAir)
                    //{
                    //    xVelocity += (float)gravity;
                    //    if (yVelocity > fric) { yVelocity -= fric; }
                    //    else if (yVelocity < -fric) { yVelocity += fric; }
                    //    else { yVelocity = 0; }
                    //}
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
        }


        public void VisionUpdate()
        {
            if(grav == gravDirection.Down)
            {
                if(currentDir == Direction.left | currentDir == Direction.right)
                {
                    vision.X = ObjRect.X - 10;
                    vision.Y = ObjRect.Y + ObjRectHeight;
                }
            }

            if (grav == gravDirection.Up)
            {
                if (currentDir == Direction.left | currentDir == Direction.right)
                {
                    vision.X = ObjRect.X - 10;
                    vision.Y = ObjRect.Y - vision.Height;
                }
            }

            if (grav == gravDirection.Right)
            {
                if (currentDir == Direction.up | currentDir == Direction.down)
                {
                    vision.X = ObjRect.X + ObjRectWidth;
                    vision.Y = ObjRect.Y - 10;
                }
            }

            if (grav == gravDirection.Left)
            {
                if (currentDir == Direction.up | currentDir == Direction.down)
                {
                    vision.X = ObjRect.X - vision.Height;
                    vision.Y = ObjRect.Y - 10;
                }
            }

        }

        public override void Collisions(List<GameObject> objs, KeyboardState k, KeyboardState p, World w)
        {
            inAir = true;
            foreach (GameObject obj in objs)
            {
                if (obj is Enemy | obj is Door | obj is Panel)
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

                //Vision Colisions
                if (vision.Intersects(player.ObjRect))
                {
                        if (Attacking == false)
                        {
                            Attacking = true;
                            yVelocity += (float)GameVariables.jump * 1.5f;
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
                    if (currentDir == Direction.left | currentDir == Direction.right)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 1, new Vector2(ObjRectX, ObjRectY), false);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                }

                if (grav == gravDirection.Up)
                {
                    if (currentDir == Direction.left | currentDir == Direction.right)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 1, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, ObjRectWidth * 1.5f), false, MathHelper.Pi);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                }

                if (grav == gravDirection.Left)
                {
                    if (currentDir == Direction.up | currentDir == Direction.down)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 1, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, ObjRectWidth), false, MathHelper.Pi / 2);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                }

                if (grav == gravDirection.Right)
                {
                    if (currentDir == Direction.up | currentDir == Direction.down)
                    {
                        if (atime.ElapsedMilliseconds > 1000) { atime.Reset(); }

                        atime.Start();
                        Move.DrawFrame(s, 1, new Vector2(ObjRectX, ObjRectY), new Vector2(ObjRectHeight, 0), false, -MathHelper.Pi / 2);
                        float elapsed = atime.ElapsedMilliseconds;
                        Move.UpdateFrame(elapsed / 1000);
                    }
                }
            }
        }
    }
}
