using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class Spike : GameObject
    {
        public Spike(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
            isDangerous = true;
        }
    }
}
