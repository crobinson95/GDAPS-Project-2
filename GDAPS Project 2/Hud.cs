using Microsoft.Xna.Framework;
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
        bool top;

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
            worldLevel = wL;
            top = true;

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

        public void checkPlayerY()
        {
            if(playerLoc.ObjRectY < GameVariables.innerHeight / 3 && top)
            {
                // move hud to bottom
                top = false;
            }
            if(playerLoc.ObjRectY > GameVariables.innerHeight * 2 / 3 && !top)
            {
                // move hud to top
                top = true;
            }
        }
        

        public override void spriteDraw(SpriteBatch s)
        {
            if (top)
            {
                s.Draw(this.ObjImage, new Rectangle(50, 20, 700, 180), Color.White);
                foreach (string key in HudInfo.Keys.ToArray())
                {
                    if (HudInfo[key])
                    {
                        // TODO differentiate between drawn objects and strings as well as set xy
                    }
                }
            }
            else
            {
                s.Draw(this.ObjImage, new Rectangle(50, GameVariables.gameHeight - 200, 700, 180), Color.White);
                foreach (string key in HudInfo.Keys.ToArray())
                {
                    if (HudInfo[key])
                    {
                        // TODO differentiate between drawn objects and strings as well as set xy
                    }
                }
            }
        }

    }
}
