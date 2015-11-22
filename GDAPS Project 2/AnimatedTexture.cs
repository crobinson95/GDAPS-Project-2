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
        public AnimatedTexture(ContentManager content, string name, int frames, int FramesPerSec)
        {
            spriteSheet = content.Load<Texture2D>(name);
            framecount = frames;
            TotalElapsed = 0;
            Frame = 1;
            TimePerFrame = FramesPerSec;
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
        public void DrawFrame(SpriteBatch batch, int level, Vector2 screenPos)
        {
            DrawFrame(batch, Frame, level, screenPos);
        }

        //Draws Specific Frame from spritesheet
        public void DrawFrame(SpriteBatch batch, int frame, int level, Vector2 screenPos)
        {
            int FrameWidth = spriteSheet.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * frame, level,
                FrameWidth, spriteSheet.Height);
            batch.Draw(spriteSheet, screenPos, sourcerect, Color.White);
        }

    }
}
