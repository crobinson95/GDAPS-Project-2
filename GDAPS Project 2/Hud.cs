using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDAPS_Project_2
{
    class Hud : GameObject
    {
        Player playerLoc;
        SpriteBatch spriteBatch;
        string[] levelRelevantInfo;
        string worldLevel;

        Dictionary<string, bool> HudInfo = new Dictionary<string, bool>();

        public Hud(int x, int y, int w, int h, SpriteBatch s, Player p, string[] l, string wL) : base(x, y, w, h)
        {
            HudInfo.Add("time", false);
            HudInfo.Add("energy", false);
            HudInfo.Add("items", false);
            HudInfo.Add("level", false); // TODO finish the rest of the possible values found in the HUD

            spriteBatch = s;
            playerLoc = p;
            levelRelevantInfo = l;
            worldLevel = wL; // pass in the world and level (eg. "Menu - Main", "1 - 3")

            // TODO: set HUD starting location

            string[] keys = HudInfo.Keys.ToArray();

            foreach(string hudComponent in levelRelevantInfo)
            {
                foreach(string key in keys)
                {
                    if (hudComponent.Equals(key))
                    {
                        HudInfo[key] = true;
                    }
                }
            }
        }

        // TODO use player to get player location and draw HUD on the top/bottom depending on location

        public override void spriteDraw(SpriteBatch s)
        {
            foreach(string key in HudInfo.Keys.ToArray())
            {
                if (HudInfo[key])
                {
                    // TODO differentiate between drawn objects and strings as well as set xy
                }
            }
        }

    }
}
