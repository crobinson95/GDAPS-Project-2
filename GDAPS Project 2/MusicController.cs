using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace GDAPS_Project_2
{
    /// <summary>
    /// Class designed to "fade" a sound effect in or out.
    /// </summary>
    public class MusicController
    {
        public SoundEffectInstance Layer;

        public bool layerInLevel;

        // Keeps track of when to increment volume.
        protected Stopwatch tempTimer = new Stopwatch();

        // Determines whether a layer is faded in or faded out at the beginning of each level.
        public float layerVolume;

        /// <summary>
        /// Music controller object has a single soundeffect instance paramter which set to loop.
        /// </summary>
        /// <param name="layer"></param>
        public MusicController(SoundEffectInstance layer)
        {
            Layer = layer;
            Layer.IsLooped = true;
            layerVolume = 0.0f;
            Layer.Volume = layerVolume;
            layerInLevel = false;
        }

        /// <summary>
        /// Increments the volume by 1% every 130 milliseconds.
        /// </summary>
        /// <param name="newVolume"></param>
        public void FadeIn(float newVolume)
        {
            if (Layer.Volume < newVolume -0.01)
            {
                if (!tempTimer.IsRunning)
                {
                    tempTimer.Start();
                }
                if (tempTimer.ElapsedMilliseconds > 130)
                {
                    Layer.Volume += 0.01f;
                    tempTimer.Restart();
                }
            }
            else { tempTimer.Reset(); }
        }

        /// <summary>
        /// Decrements the volume by 1% every 130 milliseconds.
        /// </summary>
        /// <param name="newVolume"></param>
        public void FadeOut(float newVolume)
        {
            if (Layer.Volume > newVolume + 0.01)
            {
                if (!tempTimer.IsRunning)
                {
                    tempTimer.Start();
                }
                if (tempTimer.ElapsedMilliseconds > 130)
                {
                    Layer.Volume -= 0.01f;
                    tempTimer.Restart();
                }
            }
            else if (tempTimer.IsRunning) { tempTimer.Reset(); }
        }
    }
}
