﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class Level
    {
        public int levelTime;
        public int deathCount;
        public Stopwatch levelTimer;
        public Point playerSpawn;
        public List<GameObject> objects = new List<GameObject>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<MusicController> levelMusic;

        string[] hudInfo;
        public string[] HudInfo { get { return hudInfo; } }
        
        /// <summary>
        /// Reads text files with info on each line.
        /// Format for Hud line:
        /// hud, strings (strings for information that needs to be displayed)
        /// Format for each line:
        /// identifier,x_position,y_position,(extra attributes depending on objects).
        /// </summary>
        public Level(string levelData, StreamReader s, Player player, ContentManager content)
        {
            levelTimer = new Stopwatch();
            levelTime = 0;
            deathCount = 0;
            levelMusic = new List<MusicController>();
            try
            {
                playerSpawn = new Point ( 0, 0 );
                char[] delim = { ',' };
                string identifier;
                Point p;
                s = new StreamReader("Content/" + levelData);
                string line;
                string[] splitLine;
                while ((line = s.ReadLine()) != null)
                {
                    try
                    {
                        splitLine = line.Split(delim);
                        identifier = splitLine[0].ToLower();
                        if (identifier.Equals("hud"))
                        {
                            hudInfo = splitLine;
                        }
                        else if (identifier.Equals("music"))
                        {
                            int i = 0;
                            string[] musicInfo = splitLine;
                            foreach (MusicController layer in GameVariables.layerList)
                            {
                                if (i < musicInfo.Length - 1)
                                {
                                    layer.layerInLevel = true;
                                }
                                else
                                {
                                    layer.layerInLevel = false;
                                }
                                i++;        
                            }
                        }
                        else
                        {
                            p = new Point(int.Parse(splitLine[1]), int.Parse(splitLine[2]));
                            if (identifier.Contains("block"))
                            {
                                objects.Add(new Block(p.X, p.Y, 40, 40, identifier));
                            }
                            else if (identifier.Contains("panel"))
                            {
                                objects.Add(new Panel(p.X, p.Y, 80, 80, identifier));
                            }
                            else if (identifier.Contains("hazard"))
                            {
                                objects.Add(new Hazard(p.X + 4, p.Y, 32, 32, identifier));
                            }
                            else
                            {
                                switch (identifier)
                                {
                                    case "spawn":
                                        playerSpawn = p;
                                        break;
                                    case "door":
                                        string d = splitLine[4];
                                        string vict = splitLine[3];
                                        if (splitLine.Length == 6)
                                        {
                                            string world = splitLine[5];
                                            objects.Add(new Door(content, p.X, p.Y, 43, 70, vict, d, @world));
                                        }
                                        else
                                        {
                                            objects.Add(new Door(content, p.X, p.Y, 43, 70, vict, d));
                                        }
                                        break;
                                    case "enemy":
                                        enemies.Add(new Enemy(content, p.X, p.Y, 50, 70, player));
                                        break;

                                    case "enemyf":
                                        enemies.Add(new EnemyF(content, p.X, p.Y, 50, 70, player));
                                        break;

                                    case "enemyw":
                                        enemies.Add(new EnemyW(content, p.X, p.Y, 45, 40, player));
                                        break;
                                }
                            }
                        }
                    }
                    catch
                    {
                        //Console.WriteLine("File read, unable to load objects, exiting");
                        //System.Threading.Thread.Sleep(2000);
                        //Environment.Exit(0);
                    }
                }
            }
            catch
            {
                //Console.WriteLine("Unable to read file, exiting");
                //System.Threading.Thread.Sleep(2000);
                //Environment.Exit(0);
            }
            finally
            {
                if (s != null)
                {
                    s = null;
                }
            }
        }
    }
}
