using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class HitBox : MovableGameObject 
    {
        public bool active;

        public HitBox(int x, int y, int width, int height)
            : base(x, y, width, height)
        { }
    }
}
