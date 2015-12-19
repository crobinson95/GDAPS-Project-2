using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{ 
    class Panel : GameObject
    {
        public Panel(int x, int y, int w, int h, string ItemType)
            : base(x, y, w, h, ItemType)
        {
        }
    }
}
