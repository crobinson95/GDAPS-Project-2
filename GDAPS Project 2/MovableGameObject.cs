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
        public int frameIndex = 0;
        private double timeElapsed;
        private double timeToUpdate;
        public int FramesPerSec { set { timeToUpdate = 1f / value; } }
        //protected Vector2 spriteDirection = Vector2.Zero;
        private string currentAnimation;
        public enum myDirection { none, left, right, up, down };

        protected myDirection currentDir = myDirection.none;
        
        public enum gravDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public gravDirection grav;

        protected bool inAir;

        public bool alive;

        protected double gravity;

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

        private Dictionary<string, Rectangle[]> sAnimations = new Dictionary<string, Rectangle[]>();
        private Dictionary<string, Vector2> sOffset = new Dictionary<string, Vector2>();

        public MovableGameObject(int x, int y, int w, int h) : base(x, y, w, h)
        {
            ObjPos = new Vector2(x, y);
            ObjRectX = (int)ObjPos.X;
            ObjRectY = (int)ObjPos.Y;

            currentAnimation = "Down_Idle_Left";
            currentDir = myDirection.right;
        }

        public MovableGameObject(int x, int y, int w, int h, string ItemType) : base(x, y, w, h, ItemType)
        {
            ObjPos = new Vector2(x, y);
            ObjRectX = (int)ObjPos.X;
            ObjRectY = (int)ObjPos.Y;

            currentAnimation = "Down_Idle_Left";
            currentDir = myDirection.right;
        }

        //Cuts up sprite sheet into usable pieces
        public void AddAnimation(int frames, int xPos, int yPos, int xStartFrame, string name, int frame_width, int width, int height, Vector2 offset)
        {

            Rectangle[] myRects = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                myRects[i] = new Rectangle(((i + xStartFrame) * frame_width) + xPos, yPos, width, height);
            }
            sAnimations.Add(name, myRects);
            sOffset.Add(name, offset);
        }

        //Plays current animation
        public void PlayAnimation(string name)
        {
            if (currentAnimation != name)
            {
                currentAnimation = name;
                frameIndex = 0;
            }
        }

        //Updates the player's info and loops through animation frames
        public virtual void Update(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (frameIndex < sAnimations[currentAnimation].Length - 1)
                {
                    frameIndex++;
                }
                else
                {
                    //AnimationDone(currentAnimation);
                    frameIndex = 0;
                }
            }
        }

        //Draws the current frame of the current animation
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sTexture, ObjPos + sOffset[currentAnimation], sAnimations[currentAnimation][frameIndex], Color.White, 0.0f, new Vector2(0,0), 0.25f, SpriteEffects.None,0.2f);
        }

        //Possibly needed later to prevent animation loops
       // public abstract void AnimationDone(string animation);
    }
}
