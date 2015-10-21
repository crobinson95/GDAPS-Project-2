using Microsoft.Xna.Framework;
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
        string debug1;
        string debug2;
        string debug3;
        public List<GameObject> objects = new List<GameObject>();
        
        /// <summary>
        /// Reads text files with info on each line.
        /// Format for each line:
        /// identifier,x_position,y_position,(extra attributes depending on objects).
        /// </summary>
        public Level(string levelData, StreamReader s)
        {
            try
            {
                char[] delim = { ',' };
                string identifier;
                Point p;
                s = new StreamReader(levelData);
                debug1 = levelData;
                string line;
                string[] splitLine;
                while ((line = s.ReadLine()) != null)
                {
                    try
                    {
                        debug2 = line;
                        splitLine = line.Split(delim);
                        identifier = splitLine[0];
                        debug3 = identifier;
                        p = new Point(int.Parse(splitLine[1]), int.Parse(splitLine[2]));
                        switch (identifier)
                        {
                            case "floor":
                                int w = int.Parse(splitLine[3]);
                                objects.Add(new Floor(p.X, p.Y, w, 30));
                                break;

                            case "wall":
                                int h = int.Parse(splitLine[3]);
                                objects.Add(new Wall(p.X, p.Y, 30, h));
                                break;

                            case "spike":
                                objects.Add(new Spike(p.X, p.Y, 100, 100));
                                break;

                            case "door":
                                int d = int.Parse(splitLine[3]);
                                objects.Add(new Door(p.X, p.Y, 75, 150, d));
                                break;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

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
