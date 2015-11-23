using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace GDAPS_Project_2
{
    class World
    {
        List<Level> levels = new List<Level>();
        public List<Level> Levels { get { return levels; } }

        public bool changeWorldBool = false;

        string[] levelFiles;
        public int currentLevel;

        public World(string world, StreamReader s, Player p, ContentManager content) // string world is a directory
        {
            currentLevel = 0;
            levelFiles = Directory.GetFiles(world); // need to start organizing worlds by directory
            foreach (string level in levelFiles)
            {
                Level newLevel = new Level(level, s, p, content);
                levels.Add(newLevel);
            }

        }
    }
}
