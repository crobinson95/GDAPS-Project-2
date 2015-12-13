using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace GDAPS_Project_2
{
    class AnimatedTexture
    {
        Texture2D spriteSheet;

        float TotalElapsed;

        public int Frame;

        float TimePerFrame;

        int framecount;


        //Takes Content manager, string for sprite sheet png and the total number of frames and frames per second.
        public AnimatedTexture(ContentManager content, string name, int frames, float FramesPerSec)
        {
            spriteSheet = content.Load<Texture2D>(name);
            framecount = frames;
            TotalElapsed = 0;
            Frame = 1;
            TimePerFrame = 1/FramesPerSec;
        }


        //Moves to next frame
        public void UpdateFrame(float elapsed)
        {
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                Frame++;
                Frame = Frame % framecount;
                TotalElapsed -= TimePerFrame;
            }
        }

        //For reverse animations, for whatever reason
        public void UpdateFrameReverse(float elapsed)
        {
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                Frame--;
                Frame = Frame % framecount;
                TotalElapsed -= TimePerFrame;
            }
        }

        //Draws Frame
        public void DrawFrame(SpriteBatch batch, int level, Vector2 screenPos, bool multi)
        {
            DrawFrame(batch, Frame, level, screenPos, multi);
        }
        public void DrawFrame(SpriteBatch batch, int level, Vector2 screenPos, bool multi, float s)
        {
            DrawFrame(batch, Frame, level, screenPos, multi, s);
        }

        public void DrawFrame(SpriteBatch batch, int level, Vector2 screenPos, Vector2 origin, bool multi, float r)
        {
            DrawFrame(batch, Frame, level, screenPos, origin, multi, r);
        }

        //Draws Specific Frame from spritesheet
        public void DrawFrame(SpriteBatch batch, int frame, int level, Vector2 screenPos, bool multi)
        {
            int FrameWidth = spriteSheet.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0,
                FrameWidth, spriteSheet.Height);
            if (multi)
            {
                if(level == 1)
                {
                    sourcerect = new Rectangle(FrameWidth * frame, 0,
                FrameWidth, spriteSheet.Height /2);
                }
                else if(level == 2)
                {
                    sourcerect = new Rectangle(FrameWidth * frame, spriteSheet.Height / 2,
                FrameWidth, spriteSheet.Height/2);
                }
            }
            batch.Draw(spriteSheet, screenPos, sourcerect, Color.White, 0.0f, new Vector2(0,0), 1.0f, SpriteEffects.None, 0.3f);
        }
        public void DrawFrame(SpriteBatch batch, int frame, int level, Vector2 screenPos, bool multi, float s)
        {
            int FrameWidth = spriteSheet.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * frame, level,
                FrameWidth, spriteSheet.Height);
            batch.Draw(spriteSheet, screenPos, sourcerect, Color.White, 0.0f, new Vector2(0, 0), s, SpriteEffects.None, 0.3f);
        }

        public void DrawFrame(SpriteBatch batch, int frame, int level, Vector2 screenPos, Vector2 origin, bool multi, float r)
        {
            int FrameWidth = spriteSheet.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * frame, level,
                FrameWidth, spriteSheet.Height);
            if (multi)
            {
                if (level == 1)
                {
                    sourcerect = new Rectangle(FrameWidth * frame, 0,
                FrameWidth, spriteSheet.Height / 2);
                }
                else if (level == 2)
                {
                    sourcerect = new Rectangle(FrameWidth * frame, spriteSheet.Height / 2,
                FrameWidth, spriteSheet.Height / 2);
                }
            }
            batch.Draw(spriteSheet, screenPos, sourcerect, Color.White, r, origin, 1.0f, SpriteEffects.None, 0.3f);
        }
    }
}
