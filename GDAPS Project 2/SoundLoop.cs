using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace GDAPS_Project_2
{
    class SoundLoop
    {
        Stopwatch tempTimer;
        SoundEffectInstance one;
        SoundEffectInstance two;
        SoundEffectInstance introSound;
        float initialVolume;
        float delay;
        float delayOne;
        float delayTwo;
        float introDelay;
        bool tracker;
        bool play;
        bool intro;

        public SoundLoop(SoundEffectInstance first, float d1, SoundEffectInstance second, float d2, float volume)
        {
            tempTimer = new Stopwatch();
            delay = d1;
            delayOne = d1;
            delayTwo = d2;
            one = first;
            two = second;
            one.Volume = volume;
            two.Volume = volume;
            tracker = false;
            play = false;
            intro = false;
        }

        public SoundLoop( SoundEffectInstance first, float d1, SoundEffectInstance second, float d2, SoundEffectInstance introduction, float iDelay, float volume)
        {
            tempTimer = new Stopwatch();
            delay = introDelay;
            delayOne = d1;
            delayTwo = d2;
            introDelay = iDelay;
            introSound = introduction;
            one = first;
            two = second;
            initialVolume = volume;
            introSound.Volume = volume;
            one.Volume = volume;
            two.Volume = volume;
            tracker = false;
            play = false;
            intro = true;
        }

        public void Loop()
        {
            one.Volume = initialVolume * GameVariables.gameVolume;
            two.Volume = initialVolume * GameVariables.gameVolume;
            if (introSound != null)
            {
                introSound.Volume = initialVolume * GameVariables.gameVolume;
            }
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
            }
            else if (tempTimer.ElapsedMilliseconds >= delay - 10)
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
