using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GDAPS_Project_2
{
    class EnemyF : Enemy
    {
        Rectangle vision;

        //float fric = (float)GameVariables.friction;
        //float jump = (float)GameVariables.jump;
        //float accel = (float)GameVariables.playerAcceleration;
        //float maxSpeed = (float)GameVariables.playerMaxSpeed;


        public EnemyF(ContentManager Content, int x, int y, int w, int h, Player p)
            : base(Content, x, y, w, h, p)
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
            vision = new Rectangle(0, 0, 100, 100);
            player = p;
        }


        public override void Movement(GameTime g)
        {
            if (alive)
            {
                VisionUpdate();
                grav = player.grav;
                ObjPos += new Vector2(xVelocity, yVelocity);
                if (grav == gravDirection.Down | grav == gravDirection.Up)
                {
                    if (player.ObjPos.X < ObjPos.X - 50)
                    {
                        currentDir = Direction.left;
                    }
                    if (player.ObjPos.X > ObjPos.X + 50)
                    {
                        currentDir = Direction.right;
                    }
                }
                if (grav == gravDirection.Left | grav == gravDirection.Right)
                {
                    if (player.ObjPos.Y < ObjPos.Y - 50)
                    {
                        currentDir = Direction.up;
                    }
                    if (player.ObjPos.Y > ObjPos.Y + 50)
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
        }


        public void VisionUpdate()
        {
            if (grav == gravDirection.Down | grav == gravDirection.Up)
            {
                if (currentDir == Direction.right)
                {
                    vision.X = ObjRectX + ObjRect.Width;
                    vision.Y = ObjRectY;
                }
                if (currentDir == Direction.left)
                {
                    vision.X = ObjRectX - vision.Width;
                    vision.Y = ObjRectY;
                }
            }
            if (grav == gravDirection.Left | grav == gravDirection.Right)
            {
                if (currentDir == Direction.up)
                {
                    vision.X = ObjRectX;
                    vision.Y = ObjRectY - vision.Height;
                }
                if (currentDir == Direction.down)
                {
                    vision.X = ObjRectX;
                    vision.Y = ObjRectY + ObjRect.Height;
                }
            }

        }

        public override void Collisions(List<GameObject> objs, KeyboardState k, KeyboardState p, World w)
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
                else if (obj.ObjRect.Contains(ObjRect.X + 10, ObjRect.Y + 10))
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
                else if (obj.ObjRect.Contains(ObjRect.Right - 10, ObjRect.Y + 10))
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
                else if (obj.ObjRect.Contains(ObjRect.Left + 10, ObjRect.Bottom - 10))
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
                else if (obj.ObjRect.Contains(ObjRect.Right - 10, ObjRect.Bottom - 10))
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
                if (vision.Intersects(obj.ObjRect))
                {
                    if (obj is Hazard)
                    {
                        switch (grav)
                        {
                            case gravDirection.Down:
                                if (inAir == false)
                                {
                                    ObjPos.Y -= 20;
                                    yVelocity = -jump;
                                    inAir = true;
                                }
                                break;
                            case gravDirection.Up:
                                if (inAir == false)
                                {
                                    ObjPos.Y += 20;
                                    yVelocity = jump;
                                    inAir = true;
                                }
                                break;
                            case gravDirection.Left:
                                if (inAir == false)
                                {
                                    ObjPos.X += 20;
                                    xVelocity = jump;
                                    inAir = true;
                                }
                                break;
                            case gravDirection.Right:
                                if (inAir == false)
                                {
                                    ObjPos.X -= 20;
                                    xVelocity = -jump;
                                    inAir = true;
                                }
                                break;
                        }
                    }
                }
                ObjRectX = (int)ObjPos.X;
                ObjRectY = (int)ObjPos.Y;
            }
        }

    }
}
