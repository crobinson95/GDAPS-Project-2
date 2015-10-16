using System;
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

        Vector2 objPos;
        public Vector2 ObjPos
        {
            get { return objPos; }
            set { objPos = value; }
        }
        
        double velocity;
        public double Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public MovableGameObject(int x, int y, int w, int h) : base(x, y, w, h) { }

    }
}
