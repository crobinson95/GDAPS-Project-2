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
        Vector2 ObjPos
        {
            get { return objPos; }
            set { objPos = value; }
        }
        
        double time;
        double Time
        {
            get { return Time; }
            set { time = value; }
        }
        double velocity;
        double Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        

    }
}
