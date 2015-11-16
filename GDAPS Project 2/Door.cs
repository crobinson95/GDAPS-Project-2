using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class Door : GameObject
    {
        public int destination;
        public string destWorld = null;

        public Door(int x, int y, int w, int h, int d, string world)
            : base(x, y, w, h)
        {
            destination = d;
            destWorld = world;
        }


        public Door(int x, int y, int w, int h, int d)
            : base(x, y, w, h)
        {
            destination = d;
        }
    }
}
