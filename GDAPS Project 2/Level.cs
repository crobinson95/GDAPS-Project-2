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
        StreamReader s;
        public Level(int t, string levelData)
        {
            levelTime = t;
            try
            {
                char delim = ',';
                string identifier;
                Point p;
                s = new StreamReader(levelData);
                string line;
                string[] splitLine;
                while((line = s.ReadLine()) != null)
                {
                    try
                    {
                        splitLine = line.Split(delim);
                        identifier = splitLine[0];
                        p = new Point(int.Parse(splitLine[1]), int.Parse(splitLine[2])); // TODO check up and normalize string format for levels
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
                if(s != null)
                {
                    s = null;
                }
            }
        }
    }
}
