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
            gravDirection grav = gravDirection.Down;

        }

        public override bool isColliding()
        {
            return false;
        }
        
        public void Jump()
        {

        }
    }
}
