using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS_Project_2
{
    abstract class MovableGameObject : GameObject
    {
        protected Texture2D sTexture;
        private Rectangle[] playerRects;
        private int frameIndex = 0;
        private double timeElapsed;
        private double timeToUpdate;
        public int FramesPerSec { set { timeToUpdate = 1f / value; } }


        public enum gravDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public gravDirection grav;

        public bool inAir;

        public bool alive;

        public double gravity;

        public Vector2 ObjPos;

        float xvelocity;
        public float xVelocity
        {
            get { return xvelocity; }
            set { xvelocity = value; }
        }

        float yvelocity;
        public float yVelocity
        {
            get { return yvelocity; }
            set { yvelocity = value; }
        }
        public virtual bool isColliding(GameObject obj)
        {
            if (ObjRect.Intersects(obj.ObjRect)) { return true; }
            else { return false; }
        }

        public MovableGameObject(int x, int y, int w, int h) : base(x, y, w, h)
        {
            ObjPos = new Vector2(x, y);
            ObjRectX = (int)ObjPos.X;
            ObjRectY = (int)ObjPos.Y;
        }

        public void AddAnimation(int frames)
        {
            int width = sTexture.Width / frames;
            playerRects = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                playerRects[i] = new Rectangle(i * width, 0, width, sTexture.Height);
            }
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (frameIndex < playerRects.Length - 1)
                {
                    frameIndex++;
                }
                else { frameIndex = 0; }


            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sTexture, ObjPos, playerRects[frameIndex], Color.White);
        }
    }
}
