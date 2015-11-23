﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class Level
    {
        int levelTime;
        public Point playerSpawn;
        public List<GameObject> objects = new List<GameObject>();
        public List<Enemy> enemies = new List<Enemy>();

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
            try
            {
                playerSpawn = new Point ( 0, 0 );
                char[] delim = { ',' };
                string identifier;
                Point p;
                s = new StreamReader(levelData);
                string line;
                string[] splitLine;
                while ((line = s.ReadLine()) != null)
                {
                    try
                    {
                        splitLine = line.Split(delim);
                        identifier = splitLine[0];
                        if (identifier.Equals("hud"))
                        {
                            hudInfo = splitLine;
                        }
                        else
                        {
                            p = new Point(int.Parse(splitLine[1]), int.Parse(splitLine[2]));
                            switch (identifier)
                            {
                                case "spawn":
                                    playerSpawn = p;
                                    break;
                                case "floor":
                                    int w = int.Parse(splitLine[3]);
                                    objects.Add(new Floor(p.X, p.Y, w, 60));
                                    break;

                                case "wall":
                                    int h = int.Parse(splitLine[3]);
                                    objects.Add(new Wall(p.X, p.Y, 60, h));
                                    break;

                                case "spike":
                                    objects.Add(new Spike(p.X, p.Y, 100, 100));
                                    break;

                                case "door":
                                    int d = int.Parse(splitLine[3]);
                                    
                                    if (splitLine.Length == 5)
                                    {
                                        string world = splitLine[4];
                                        objects.Add(new Door(p.X, p.Y, 75, 150, d, @world));
                                        // change door code to include world to go to
                                    }
                                    else
                                    {
                                        objects.Add(new Door(content, p.X, p.Y, 75, 150, d));
                                    }
                                    break;

                                case "enemy":
                                    enemies.Add(new Enemy(p.X, p.Y, 60, 60, player));
                                    break;

                                case "enemyf":
                                    enemies.Add(new EnemyF(p.X, p.Y, 60, 60, player));
                                    break;

                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("File read, unable to load objects, exiting");
                        System.Threading.Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Unable to read file, exiting");
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
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
