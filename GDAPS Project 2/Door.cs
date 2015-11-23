using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace GDAPS_Project_2
{
    class Door : GameObject
    {
        public int destination;
        public string destWorld = null;

        public AnimatedTexture sprite;

        public State state;

        Stopwatch time;

        public enum State
        {
            closed,
            opening,
            closing,
            open
        }

        public Door(int x, int y, int w, int h, int d, string world)
            : base(x, y, w, h)
        {
            destination = d;
            destWorld = world;
        }


        public Door(ContentManager content, int x, int y, int w, int h, int d)
            : base(x, y, w, h)
        {
            destination = d;
            state = State.closed;
            time = new Stopwatch();
            sprite = new AnimatedTexture(content, @"Images/Sprites/door_spir", 10, 1);
        }

        public void Open(Player p)
        {
            if (p.isColliding(this))
            {
                if (state == State.closed)
                {
                    time.Reset();
                    state = State.opening;
                }
            }
            else
            {
                if (state == State.open)
                {
                    time.Reset();
                    state = State.closing;
                }
            }
        }

        public override void spriteDraw(SpriteBatch s)
        {
            if (state == State.closed)
            {
                base.spriteDraw(s);
            }
            else if (state == State.opening)
            {
                time.Start();
                sprite.DrawFrame(s, 0, new Microsoft.Xna.Framework.Vector2(ObjRectX, ObjRectY));
                float elapsed = time.ElapsedMilliseconds;
                sprite.UpdateFrame(elapsed / 1000);
                if (sprite.Frame == 9)
                {
                    state = State.open;
                }
            }
            else if (state == State.closing)
            {
                time.Start();
                sprite.DrawFrame(s, 0, new Microsoft.Xna.Framework.Vector2(ObjRectX, ObjRectY));
                float elapsed = time.ElapsedMilliseconds;
                sprite.UpdateFrameReverse(elapsed / 1000);
                if (sprite.Frame == 1)
                {
                    state = State.closed;
                }
            }
            else if (state == State.open)
            {
                sprite.DrawFrame(s, 9, new Microsoft.Xna.Framework.Vector2(ObjRectX, ObjRectY));
            }
            else
            {
                base.spriteDraw(s);
            }
        }
    }
}
