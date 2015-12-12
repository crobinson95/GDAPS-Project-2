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
        //List<Level> levels = new List<Level>();
        //public List<Level> Levels { get { return levels; } }

        public bool changeWorldBool = false;

        public Dictionary<string, Level> levels;

        public string currentLevel;

        public World(string world, StreamReader s, Player p, ContentManager content) // string world is a directory
        {
            levels = new Dictionary<string, Level>();
            string[] tempLevelList = Directory.GetFiles(world);
            foreach ( string current in tempLevelList)
            {
                string newCurrent = current.Replace(world+ "\\", "");
                levels.Add(newCurrent, new Level(current, s, p, content));
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
                    else
                    {
                        switch (itemType)
                        {
                            case "GDAPS_Project_2.Floor":
                                item.ObjImage = GameVariables.floorTexture;
                                break;
                            case "GDAPS_Project_2.Wall":
                                item.ObjImage = GameVariables.wallTexture;
                                break;
                            case "GDAPS_Project_2.Door":
                                item.ObjImage = GameVariables.doorTexture;
                                break;
                        }
                    }
                    foreach (Enemy enemy in loadLevel.enemies)
                    {
                        enemy.ObjImage = GameVariables.enemyTexture;
                    }
                }
                // gameHUD.ObjImage = hud;
            }
        }
    }
}
