using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace GDAPS_Project_2
{
    class Hazard : GameObject
    {
        public AudioEmitter wireEmitter = null;
        public SoundEffectInstance wireSound = null;

        public Hazard(int x, int y, int w, int h, string ItemType)
            : base(x, y, w, h, ItemType)
        {
            isDangerous = true;
            if (itemType.Contains("wire"))
            {
                wireEmitter = new AudioEmitter();
                wireEmitter.Position = new Vector3(x, y, 0.0f);
                wireSound = GameVariables.wireFXInstance;
                wireSound.Volume = 0.2f;
                GameVariables.GameSounds.Add(wireSound);
            }
        }
    }
}
