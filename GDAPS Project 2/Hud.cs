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
        Player player;

        SpriteBatch sprite;

        Camera view;

        bool hasKey;

        public Rectangle back = new Rectangle();
        public Texture2D backt;
        public Rectangle under = new Rectangle();
        public Texture2D undert;
        public Rectangle energy = new Rectangle();
        public Texture2D energyt;
        public Rectangle key = new Rectangle();
        public Texture2D keyt;


        public Hud(int x, int y, int w, int h, SpriteBatch s, Player p, Camera g, Level level) : base(x, y, w, h)
        {
            sprite = s;
            player = p;
            view = g;
            Update(level);
            Check();

        }

        public override void spriteDraw(SpriteBatch s)
        {
            energy.Width = ((int)player.energy);
            
            s.Draw(backt, back, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.13f);
            s.Draw(undert, under, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.12f);
            s.Draw(energyt, energy, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.11f);
            if (hasKey)
            {
                s.Draw(keyt, key, Color.White);
            }

        }

        public void Update(Level l)
        {
            //if (false)
            //{
            //    hasKey = true;
            //}
            //else
            //{
                hasKey = false;
            //}
        }

        public void Check()
        {
            ObjRectX = -(int)view.camX + 20;
            ObjRectY = -(int)view.camY + 20;

            back.Width = ObjRect.Width;
            back.Height = ObjRect.Height;
            back.X = ObjRectX;
            back.Y = ObjRectY;

            under.Width = back.Width - 20;
            under.Height = back.Height / 3;
            under.X = back.X + 10;
            under.Y = back.Y + 10;

            energy.Width = under.Width;
            energy.Height = under.Height;
            energy.X = under.X;
            energy.Y = under.Y;

            key.Width = 10;
            key.Height = 10;
            key.X = under.X;
            key.Y = under.Y + 15;

        }
        //Player playerLoc;
        //SpriteBatch spriteBatch;
        //string[] levelRelevantInfo;
        //string worldLevel;
        //bool top;
        //Point infoDraw; // used to determine position of objects on HUD based on how many are visible


        //Dictionary<string, bool> HudInfo = new Dictionary<string, bool>();

        //public Hud(int x, int y, int w, int h, SpriteBatch s, Player p, string[] l, string wL) : base(x, y, w, h)
        //{
        //    HudInfo.Add("time", false);
        //    HudInfo.Add("energy", false);
        //    HudInfo.Add("items", false);
        //    HudInfo.Add("level", false); // TODO finish the rest of the possible values found in the HUD

        //    spriteBatch = s;
        //    playerLoc = p;
        //    levelRelevantInfo = l;
        //    worldLevel = wL;
        //    top = true;

        //    string[] keys = HudInfo.Keys.ToArray();
        //    foreach(string hudComponent in levelRelevantInfo)
        //    {
        //        foreach(string key in keys)
        //        {
        //            if (hudComponent.Equals(key))
        //            {
        //                HudInfo[key] = true;
        //            }
        //        }
        //    }

        //    infoDraw = new Point(x + 5, y + 5);
        //}

        //public void checkPlayerY()
        //{
        //    if(playerLoc.ObjRectY < GameVariables.innerHeight / 3 && top)
        //    {
        //        // move hud to bottom
        //        top = false;
        //    }
        //    if(playerLoc.ObjRectY > GameVariables.innerHeight * 2 / 3 && !top)
        //    {
        //        // move hud to top
        //        top = true;
        //    }
        //}


        //public override void spriteDraw(SpriteBatch s)
        //{
        //    if (top)
        //    {
        //        s.Draw(this.ObjImage, new Rectangle(50, 20, 700, 180), Color.White);
        //        foreach (string key in HudInfo.Keys.ToArray())
        //        {
        //            if (HudInfo[key])
        //            {
        //                // TODO differentiate between drawn objects and strings as well as set xy
        //            }
        //        }
        //    }
        //    else
        //    {
        //        s.Draw(this.ObjImage, new Rectangle(50, GameVariables.gameHeight - 200, 700, 180), Color.White);
        //        foreach (string key in HudInfo.Keys.ToArray())
        //        {
        //            if (HudInfo[key])
        //            {
        //                // TODO differentiate between drawn objects and strings as well as set xy
        //            }
        //        }
        //    }
        //}

    }
}
