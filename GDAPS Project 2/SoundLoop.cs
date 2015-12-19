using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace GDAPS_Project_2
{
    /// <summary>
    /// This class is an attempt to make a more seemless audio loop than Monogame's isLooped method (not 100% succesful...).
    /// </summary>
    class SoundLoop
    {
        // Keeps track of when to player he next instance of a sound.
        Stopwatch tempTimer;
        SoundEffectInstance one;
        SoundEffectInstance two;
        SoundEffectInstance introSound;

        // Time wait after sound effect is played.
        float delay;
        float delayOne;
        float delayTwo;
        float introDelay;

        // Keeps track of which soundeffect was last played.
        bool tracker;
        // Keeps track of whether or not the loop is playing.
        bool play;
        // Keeps track of whether or not an intro exists.
        bool intro;

        // Create a loop between two sound effects with no intro sound effect.
        public SoundLoop(SoundEffectInstance first, float d1, SoundEffectInstance second, float d2)
        {
            tempTimer = new Stopwatch();
            delay = d1;
            delayOne = d1;
            delayTwo = d2;
            one = first;
            two = second;
            tracker = false;
            play = false;
            intro = false;
        }

        // Create a loop between two sound effects with no intro sound effect.
        public SoundLoop( SoundEffectInstance first, float d1, SoundEffectInstance second, float d2, SoundEffectInstance introduction, float iDelay)
        {
            tempTimer = new Stopwatch();
            delay = introDelay;
            delayOne = d1;
            delayTwo = d2;
            introDelay = iDelay;
            introSound = introduction;
            one = first;
            two = second;
            tracker = false;
            play = false;
            intro = true;
        }

        // Loop method to be called in update when loop should be played.
        public void Loop()
        {
            if (intro)
            {
                introSound.Play();
                tempTimer.Start();
                delay = introDelay;
                intro = false;
                play = true;
            }
            else if (!play)
            {
                one.Play();
                play = true;
                tempTimer.Start();
                delay = delayOne;
                tracker = true;
            }
            else if (tempTimer.ElapsedMilliseconds >= delay)
            {
                if (tracker)
                {
                    two.Play();
                    delay = delayTwo;
                    tracker = false;
                    tempTimer.Reset();
                    tempTimer.Start();
                }
                else if (!tracker)
                {
                    one.Play();
                    delay = delayOne;
                    tracker = true;
                    tempTimer.Reset();
                    tempTimer.Start();
                }
            }
        }

        // End method to be called when the loop stops to reset it. Specifically made to reset the intro of the loop.
        public void End()
        {
            one.Stop();
            two.Stop();
            introSound.Stop();
            tempTimer.Reset();
            play = false;
            if (introSound != null)
            {
                intro = true;
            }
        }
    }
}
