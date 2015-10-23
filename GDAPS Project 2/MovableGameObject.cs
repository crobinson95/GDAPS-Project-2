﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS_Project_2
{
    abstract class MovableGameObject : GameObject
    {
        public enum gravDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public gravDirection grav;

        public bool inAir;

        public bool alive;

        public double gravity;

        public Vector2 ObjPos;
        
        float xvelocity;
        public float xVelocity
        {
            get { return xvelocity; }
            set { xvelocity = value; }
        }

        float yvelocity;
        public float yVelocity
        {
            get { return yvelocity; }
            set { yvelocity = value; }
        }
        public virtual bool isColliding(GameObject obj)
        {
            if (ObjRect.Intersects(obj.ObjRect)) { return true; }
            else { return false; }
        }

        public MovableGameObject(int x, int y, int w, int h) : base(x, y, w, h)
        {
            ObjPos = new Vector2(x, y);
            ObjRectX = (int)ObjPos.X;
            ObjRectY = (int)ObjPos.Y;
        }

    }
}
