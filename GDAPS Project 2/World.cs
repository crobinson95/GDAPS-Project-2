using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class World
    {
        List<Level> levels = new List<Level>();
        string[] levelFiles;

        public World(string world, StreamReader s)
        {
            try {
                levelFiles = Directory.GetFiles(world);
                foreach(string level in levelFiles)
                {
                    Level newLevel = new Level(level, s);
                    levels.Add(newLevel);
                }
            }
            catch
            {

            }
        }
    }
}
