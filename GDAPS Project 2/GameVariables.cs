using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using System.Text;

/// TODO: Set game variables

namespace GDAPS_Project_2
{
    /// <summary>
    /// Various game variables to allow easy editing to them. Also loads and initializes most content files.
    /// </summary>
    public static class GameVariables
    {
        // Some editable game variables.
        public static int gameWidth;
        public static int gameHeight;
        public static int innerWidth;
        public static int innerHeight;
        public static float gameVolume = 1.0f;
        public static double playerAcceleration = 150f;
        public static double playerMaxSpeed = 6f;
        public static double playerAirControl = 6f;
        public static double maxAirSpeed = 30f;
        public static double gravity = 0.25f;
        public static double friction = 2.0;
        public static double jump = 5.5f;
        public static string imgHUD = @"ContentFiles/Images/Sprites/hud";
        public static string imgPlayer = @"ContentFiles/Images/Sprites/imgPlayer";
        public static string imgDoor = @"ContentFiles/Images/Sprites/imgDoor";
        public static string imgWall = @"ContentFiles/Images/Sprites/grey";
        public static string imgEnemy = @"ContentFiles/Images/Sprites/imgEnemy";
        public static string gFont = @"ContentFiles/Images/Sprites/Font";
        public static string gFont2 = @"ContentFiles/Images/Sprites/Font2";
        public static string backgroundStars = @"ContentFiles/Images/Sprites/Background/stars";
        public static string bgMenus;
        public static string bgLevels;
        public static string menuWorld = @"menu"; // set root directory?

        // Most of the texture in the game.
        public static Texture2D starsBackground;
        public static Texture2D blockBlank;
        public static Texture2D blockInsideBottom;
        public static Texture2D blockInsideLeft;
        public static Texture2D blockInsideRight;
        public static Texture2D blockInsideTop;
        public static Texture2D blockOutsideBottom;
        public static Texture2D blockOutsideLeft;
        public static Texture2D blockOutsideRight;
        public static Texture2D blockOutsideTop;
        public static Texture2D blockWiresBottom;
        public static Texture2D blockWiresLeft;
        public static Texture2D blockWiresRight;
        public static Texture2D blockWiresTop;
        public static Texture2D blockInsideCornerTopLeft;
        public static Texture2D blockInsideCornerTopRight;
        public static Texture2D blockInsideCornerBottomLeft;
        public static Texture2D blockInsideCornerBottomRight;
        public static Texture2D blockOutsideCornerTopLeft;
        public static Texture2D blockOutsideCornerTopRight;
        public static Texture2D blockOutsideCornerBottomLeft;
        public static Texture2D blockOutsideCornerBottomRight;
        public static Texture2D panel;
        public static Texture2D panelDark;
        public static Texture2D hazardWiresBottom;
        public static Texture2D hazardWiresLeft;
        public static Texture2D hazardWiresRight;
        public static Texture2D hazardWiresTop;

        // All soundeffects to be added here.
        static SoundEffect menuMusicEffect;
        static SoundEffect deathScreenEffect;
        static SoundEffect layer1Effect;
        static SoundEffect layer2Effect;
        static SoundEffect layer3Effect;
        static SoundEffect layer4Effect;
        static SoundEffect layer5Effect;
        static SoundEffect layer6Effect;
        static SoundEffect landing;
        static SoundEffect fallingAcceleration;
        static SoundEffect fallingLoop;
        static SoundEffect gravityFX1;
        static SoundEffect gravityFX2;
        static SoundEffect gravityFX3;
        static SoundEffect Footstep1;
        static SoundEffect Footstep2;
        static SoundEffect Footstep3;
        static SoundEffect Footstep4;
        static SoundEffect Footstep5;
        static SoundEffect Footstep6;
        static SoundEffect Footstep7;
        static SoundEffect WireFX;
        static SoundEffect DoorOpen;
        static SoundEffect DoorClose;
        static SoundEffect Robot;
        static SoundEffect DeathSound;
        static SoundEffect InterfacePauseEffect;
        static SoundEffect InterfaceStartEffect;

        // Any sound effect instance should be added here.
        public static SoundEffectInstance menuMusicInstance;
        public static SoundEffectInstance deathMusicInstance;
        public static SoundEffectInstance landingInstance;
        public static SoundEffectInstance fallingAccelerationInstance;
        public static SoundEffectInstance fallingLoopInstance1;
        public static SoundEffectInstance fallingLoopInstance2;
        public static SoundEffectInstance doorOpenInstance;
        public static SoundEffectInstance doorCloseInstance;
        public static SoundEffectInstance robotSoundInstance1;
        public static SoundEffectInstance robotSoundInstance2;
        public static SoundEffectInstance wireFXInstance;
        public static SoundEffectInstance deathSoundInstance;
        public static SoundEffectInstance interfacePauseInstance;
        public static SoundEffectInstance interfaceStartInstance;
        public static SoundEffectInstance layer1Instance;
        public static SoundEffectInstance layer2Instance;
        public static SoundEffectInstance layer3Instance;
        public static SoundEffectInstance layer4Instance;
        public static SoundEffectInstance layer5Instance;
        public static SoundEffectInstance layer6Instance;

        // Lists of instances. Called in a random sound method.
        public static List<SoundEffectInstance> FootstepSounds;
        public static List<SoundEffectInstance> GravitySounds;

        // List of all instances to keep track of and change the volume of each instance.
        public static List<SoundEffectInstance> GameSounds;

        // MusicController objects.
        public static MusicController layer1;
        public static MusicController layer2;
        public static MusicController layer3;
        public static MusicController layer4;
        public static MusicController layer5;
        public static MusicController layer6;
        public static List<MusicController> layerList = new List<MusicController>();

        /// <summary>
        /// Loads all content. To be called in Game1.Initialize. Also initializes sound instances.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContentFiles(ContentManager content)
        {
            starsBackground = content.Load<Texture2D>(GameVariables.backgroundStars);

            blockBlank = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockBlank");
            blockInsideBottom = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideBottom");
            blockInsideLeft = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideLeft");
            blockInsideRight = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideRight");
            blockInsideTop = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideTop");
            blockOutsideBottom = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockOutsideBottom");
            blockOutsideLeft = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockOutsideLeft");
            blockOutsideRight = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockOutsideRight");
            blockOutsideTop = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockOutsideTop");
            blockInsideTop = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideTop");
            blockWiresBottom = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockWiresBottom");
            blockWiresLeft = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockWiresLeft");
            blockWiresRight = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockWiresRight");
            blockWiresTop = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockWiresTop");
            blockInsideCornerTopLeft = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideCornerTopLeft");
            blockInsideCornerTopRight = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideCornerTopRight");
            blockInsideCornerBottomLeft = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideCornerBottomLeft");
            blockInsideCornerBottomRight = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockInsideCornerBottomRight");
            blockOutsideCornerTopLeft = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockOutsideCornerTopLeft");
            blockOutsideCornerTopRight = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockOutsideCornerTopRight");
            blockOutsideCornerBottomLeft = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockOutsideCornerBottomLeft");
            blockOutsideCornerBottomRight = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Block/blockOutsideCornerBottomRight");
            panel = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Background/panel");
            panelDark = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Background/panelDark");

            hazardWiresBottom = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Hazards/hazardWiresBottom");
            hazardWiresLeft = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Hazards/hazardWiresLeft");
            hazardWiresRight = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Hazards/hazardWiresRight");
            hazardWiresTop = content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Hazards/hazardWiresTop");

            menuMusicEffect = content.Load<SoundEffect>(@"ContentFiles/Sound/Music/MenuMusic");
            deathScreenEffect = content.Load<SoundEffect>(@"ContentFiles/Sound/Music/DeathScreen");

            layer1Effect = content.Load<SoundEffect>(@"ContentFiles/Sound/Layers/Arppegiating");
            layer2Effect = content.Load<SoundEffect>(@"ContentFiles/Sound/Layers/Pad");
            layer3Effect = content.Load<SoundEffect>(@"ContentFiles/Sound/Layers/Melody");
            layer4Effect = content.Load<SoundEffect>(@"ContentFiles/Sound/Layers/Rythm");
            layer5Effect = content.Load<SoundEffect>(@"ContentFiles/Sound/Layers/Drum");
            layer6Effect = content.Load<SoundEffect>(@"ContentFiles/Sound/Layers/Guitar");

            landing = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Landing");
            fallingAcceleration = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/FallingAccelerating");
            fallingLoop = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/FallingMaxLoop");
            gravityFX1 = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/GravityFX1");
            gravityFX2 = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/GravityFX2");
            gravityFX3 = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/GravityFX3");
            Footstep1 = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Footstep1");
            Footstep2 = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Footstep2");
            Footstep3 = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Footstep3");
            Footstep4 = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Footstep4");
            Footstep5 = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Footstep5");
            Footstep6 = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Footstep6");
            Footstep7 = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Footstep7");
            WireFX = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/WireFX");
            DoorOpen = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/DoorOpen");
            DoorClose = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/DoorClose");
            InterfacePauseEffect = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/InterfacePause");
            InterfaceStartEffect = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/InterfaceStart");
            Robot = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Robot");
            DeathSound = content.Load<SoundEffect>(@"ContentFiles/Sound/FX/DeathSound");

            menuMusicInstance = menuMusicEffect.CreateInstance();
            deathMusicInstance = deathScreenEffect.CreateInstance();
            menuMusicInstance.IsLooped = true;
            deathMusicInstance.IsLooped = true;

            layer1Instance = layer1Effect.CreateInstance();
            layer2Instance = layer2Effect.CreateInstance();
            layer3Instance = layer3Effect.CreateInstance();
            layer4Instance = layer4Effect.CreateInstance();
            layer5Instance = layer5Effect.CreateInstance();
            layer6Instance = layer6Effect.CreateInstance();

            landingInstance = landing.CreateInstance();
            doorOpenInstance = DoorOpen.CreateInstance();
            doorCloseInstance = DoorClose.CreateInstance();
            robotSoundInstance1 = Robot.CreateInstance();
            robotSoundInstance2 = Robot.CreateInstance();
            wireFXInstance = WireFX.CreateInstance();
            fallingAccelerationInstance = fallingAcceleration.CreateInstance();
            fallingLoopInstance1 = fallingLoop.CreateInstance();
            fallingLoopInstance2 = fallingLoop.CreateInstance();
            interfacePauseInstance = InterfacePauseEffect.CreateInstance();
            interfaceStartInstance = InterfaceStartEffect.CreateInstance();
            deathSoundInstance = DeathSound.CreateInstance();

            interfaceStartInstance.Volume = 0.25f;

            FootstepSounds = new List<SoundEffectInstance> {
                Footstep1.CreateInstance(),
                Footstep2.CreateInstance(),
                Footstep3.CreateInstance(),
                Footstep4.CreateInstance(),
                Footstep5.CreateInstance(),
                Footstep6.CreateInstance(),
                Footstep7.CreateInstance() };

            GravitySounds = new List<SoundEffectInstance> {
                gravityFX1.CreateInstance(),
                gravityFX2.CreateInstance(),
                gravityFX3.CreateInstance() };

            GameSounds = new List<SoundEffectInstance>
            {
                menuMusicInstance,
                deathMusicInstance,
                landingInstance,
                doorOpenInstance,
                robotSoundInstance1,
                robotSoundInstance2,
                wireFXInstance,
                fallingAccelerationInstance,
                fallingLoopInstance1,
                fallingLoopInstance2,
                interfacePauseInstance,
                interfaceStartInstance,
                deathSoundInstance
            };
            foreach (SoundEffectInstance temp in FootstepSounds)
            {
                GameSounds.Add(temp);
            }
            foreach (SoundEffectInstance temp in GravitySounds)
            {
                GameSounds.Add(temp);
            }
            layerList.Add(new MusicController(layer1Instance));
            layerList.Add(new MusicController(layer2Instance));
            layerList.Add(new MusicController(layer3Instance));
            layerList.Add(new MusicController(layer4Instance));
            layerList.Add(new MusicController(layer5Instance));
            layerList.Add(new MusicController(layer6Instance));
        }

        public static Texture2D ItemImage(string ImageName)
        {
            switch (ImageName)
            {
                case "blockblank": return blockBlank;
                case "blockinsidebottom": return blockInsideBottom;
                case "blockinsideleft": return blockInsideLeft;
                case "blockinsideright": return blockInsideRight;
                case "blockinsidetop": return blockInsideTop;
                case "blockoutsidebottom":return blockOutsideBottom;
                case "blockoutsideleft": return blockOutsideLeft;
                case "blockoutsideright": return blockOutsideRight;
                case "blockoutsidetop": return blockOutsideTop;
                case "blockwiresbottom": return blockWiresBottom;
                case "blockwiresleft": return blockWiresLeft;
                case "blockwiresright": return blockWiresRight;
                case "blockwirestop": return blockWiresTop;
                case "blockinsidecornertopleft": return blockInsideCornerTopLeft;
                case "blockinsidecornertopright": return blockInsideCornerTopRight;
                case "blockinsidecornerbottomleft": return blockInsideCornerBottomLeft;
                case "blockinsidecornerbottomright": return blockInsideCornerBottomRight;
                case "blockoutsidecornertopleft": return blockOutsideCornerTopLeft;
                case "blockoutsidecornertopright": return blockOutsideCornerTopRight;
                case "blockoutsidecornerbottomleft": return blockOutsideCornerBottomLeft;
                case "blockoutsidecornerbottomright": return blockOutsideCornerBottomRight;
                case "panel": return panel;
                case "paneldark": return panelDark;

                case "hazardwiresbottom": return hazardWiresBottom;
                case "hazardwiresleft": return hazardWiresLeft;
                case "hazardwiresright": return hazardWiresRight;
                case "hazardwirestop": return hazardWiresTop;
            }
            return null;
        }
    }
}
