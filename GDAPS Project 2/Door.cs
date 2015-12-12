using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace GDAPS_Project_2
{
    class Door : GameObject
    {
        public string destination;
        public string destWorld = null;

        public AnimatedTexture sprite;
        public AnimatedTexture frameSprite;

        public State state;

        Stopwatch time;
        Stopwatch frameTime;

        public enum State
        {
            closed,
            opening,
            closing,
            open
        }

        public Door(ContentManager content, int x, int y, int w, int h, string d, string world)
            : base(x, y, w, h)
        {
            destination = d;
            destWorld = world;
            state = State.closed;
            time = new Stopwatch();
            frameTime = new Stopwatch();
            frameTime.Start();
            sprite = new AnimatedTexture(content, @"ContentFiles/Images/Sprites/DoorSliding", 7, 1);
            frameSprite = new AnimatedTexture(content, @"ContentFiles/Images/Sprites/DoorFrame", 11, 1);
        }


        public Door(ContentManager content, int x, int y, int w, int h, string d)
            : base(x, y, w, h)
        {
            destination = d;
            state = State.closed;
            time = new Stopwatch();
            frameTime = new Stopwatch();
            frameTime.Start();
            sprite = new AnimatedTexture(content, @"ContentFiles/Images/Sprites/DoorSliding", 7, 1);
            frameSprite = new AnimatedTexture(content, @"ContentFiles/Images/Sprites/DoorFrame", 11, 1);
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
            frameSprite.DrawFrame(s, 0, new Vector2(ObjRectX, ObjRectY), false);
            frameSprite.UpdateFrame(frameTime.ElapsedMilliseconds / 1000);
            if (frameTime.ElapsedMilliseconds > 10000)
            {
                frameTime.Reset();
                frameTime.Start();
            }
            if (state == State.closed)
            {
                sprite.DrawFrame(s, 0, new Vector2(ObjRectX, ObjRectY), false);
            }
            else if (state == State.opening)
            {
                time.Start();
                sprite.DrawFrame(s, 0, new Vector2(ObjRectX, ObjRectY), false);
                float elapsed = time.ElapsedMilliseconds;
                sprite.UpdateFrame(elapsed / 1000);
                if (sprite.Frame >= 6)
                {
                    state = State.open;
                }
            }
            else if (state == State.closing)
            {
                time.Start();
                sprite.DrawFrame(s, 0, new Vector2(ObjRectX, ObjRectY), false);
                float elapsed = time.ElapsedMilliseconds;
                sprite.UpdateFrameReverse(elapsed / 1000);
                if (sprite.Frame <= 0)
                {
                    state = State.closed;
                }
            }
            else if (state == State.open)
            {
                sprite.DrawFrame(s, 6, new Vector2(ObjRectX, ObjRectY), false);
            }
        }
    }
}
