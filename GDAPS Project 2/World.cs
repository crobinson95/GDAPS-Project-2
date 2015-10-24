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
        public List<Level> Levels { get { return levels; } }

        string[] levelFiles;
        public int currentLevel;

        public World(string world, StreamReader s) // string world is a directory
        {
            currentLevel = 0;
            levelFiles = Directory.GetFiles(world);
            foreach (string level in levelFiles)
            {
                Level newLevel = new Level(level, s);
                levels.Add(newLevel);
            }

        }
    }
}
