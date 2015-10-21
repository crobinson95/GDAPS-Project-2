using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class World
    {
        public List<Level> levels = new List<Level>();

        public int currentLevel;

        public World(int world)
        {
            switch(world) { // TODO set levels depending on which world is selected using cases
                default:
                    break;
            }
        }
    }
}
