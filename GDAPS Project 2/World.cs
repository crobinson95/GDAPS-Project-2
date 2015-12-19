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
        public bool changeWorldBool = false;

        public Dictionary<string, Level> levels;

        public string currentLevel;

        /// <summary>
        /// Creates a dictionary of levels for the world. Currently hardcoded for each world.
        /// </summary>
        public World(string world, StreamReader s, Player p, ContentManager content)
        {
            string[] tempLevelList = new string[] { "" };
            levels = new Dictionary<string, Level>();
            if (world == "menu")
            {
                tempLevelList[0] = "main.txt";
            }
            else if (world == "world1")
            {
                tempLevelList[0] = "1-1.txt";
            }
            else if (world == "world2")
            {
                tempLevelList[0] = "2-1.txt";
            }
            foreach ( string current in tempLevelList)
            {
                levels.Add(current, new Level(world + "/" + current, s, p, content));
            }            
            //levelFiles.Add = Directory.GetFiles(world); // need to start organizing worlds by directory
            //foreach (string level in levelFiles)
            //{
            //    Level newLevel = new Level(level, s, p, content);
            //    levels.Add(newLevel);
            //}
        }
        /// <summary>
        /// Loads the levels in a world
        /// </summary>
        public void LoadWorld()
        {
            foreach (Level loadLevel in this.levels.Values)
            {
                foreach (GameObject item in loadLevel.objects)
                {
                    string itemType = item.ToString();
                    if (item.itemType != null)
                    {
                        item.ObjImage = GameVariables.ItemImage(item.itemType);
                    }
                }
            }
        }
    }
}
