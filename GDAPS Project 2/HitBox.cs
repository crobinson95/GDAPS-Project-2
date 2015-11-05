using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class HitBox : MovableGameObject 
    {
        public bool active;     //Why is this a class? Isn't the hit box the rectangle the player is assigned?

        public HitBox(int x, int y, int width, int height)
            : base(x, y, width, height)
        { }

        public override void AnimationDone(string animation)
        {
            throw new NotImplementedException();
        }
    }
}
