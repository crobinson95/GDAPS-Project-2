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

        public Hud(int x, int y, int w, int h, SpriteBatch s, Player p, string[] l) : base(x, y, w, h)
        {
            spriteBatch = s;
            playerLoc = p;
            levelRelevantInfo = l;
            // TODO: set HUD starting location, use the sprite batch to write/draw relevant information
            // use player to get player location and draw HUD on the top/bottom depending on location
            // add a string to each level that provides relevant information that needs to be displayed
        }

    }
}
