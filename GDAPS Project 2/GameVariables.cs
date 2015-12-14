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
    public static class GameVariables
    {

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
        public static string imgWall = @"ContentFiles/Images/Sprites/imgWall";
        public static string imgFloor = @"ContentFiles/Images/Sprites/imgFloor";
        public static string imgSpike = @"ContentFiles/Images/Sprites/imgSpike";
        public static string imgDoor = @"ContentFiles/Images/Sprites/imgDoor";
        public static string imgEnemy = @"ContentFiles/Images/Sprites/imgEnemy";
        public static string gFont = @"ContentFiles/Images/Sprites/Font";
        public static string backgroundStars = @"ContentFiles/Images/Sprites/Background/stars";
        public static string bgMenus;
        public static string bgLevels;
        public static string menuWorld = @"menu"; // set root directory?

        public static Texture2D floorTexture;
        public static Texture2D wallTexture;
        public static Texture2D spikeTexture;
        public static Texture2D doorTexture;
        public static Texture2D enemyTexture;
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

        public static SoundEffect mainMenuX;

        public static SoundEffect finalLayer1;
        public static SoundEffect finalLayer2;
        public static SoundEffect finalLayer3;
        public static SoundEffect landing;
        public static SoundEffect fallingAcceleration;
        public static SoundEffect fallingLoop;
        public static SoundEffect gravityFX1;
        public static SoundEffect gravityFX2;
        public static SoundEffect gravityFX3;
        public static SoundEffect Footstep1;
        public static SoundEffect Footstep2;
        public static SoundEffect Footstep3;
        public static SoundEffect Footstep4;
        public static SoundEffect Footstep5;
        public static SoundEffect Footstep6;
        public static SoundEffect Footstep7;
        public static SoundEffect WireFX;
        public static SoundEffect DoorOpen;
        public static SoundEffect DoorClose;
        public static SoundEffect Robot;

        public static SoundEffectInstance mainX;

        public static SoundEffectInstance l1;
        public static SoundEffectInstance l2;
        public static SoundEffectInstance l3;
        public static SoundEffectInstance landingInstance;
        public static SoundEffectInstance fAcceleration;
        public static SoundEffectInstance fLoop;
        public static SoundEffectInstance doorOpen;
        public static SoundEffectInstance doorClose;
        public static SoundEffectInstance robotSound;
        public static SoundEffectInstance wireSound;

        public static List<SoundEffectInstance> FootstepSounds;
        public static List<SoundEffectInstance> GravitySounds;
        public static List<SoundEffectInstance> GameSounds;

        public static void LoadContentFiles(ContentManager content)
        {
            floorTexture = content.Load<Texture2D>(GameVariables.imgFloor);
            wallTexture = content.Load<Texture2D>(GameVariables.imgWall);
            spikeTexture = content.Load<Texture2D>(GameVariables.imgSpike);
            doorTexture = content.Load<Texture2D>(GameVariables.imgDoor);
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

            mainMenuX = content.Load<SoundEffect>(@"ContentFiles/Sound/FFX_To_Zanarkand");

            finalLayer1 = content.Load<SoundEffect>(@"ContentFiles/Sound/TestLoops/FinalLayer1");
            finalLayer2 = content.Load<SoundEffect>(@"ContentFiles/Sound/TestLoops/FinalLayer2");
            finalLayer3 = content.Load<SoundEffect>(@"ContentFiles/Sound/TestLoops/FinalLayer3");
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
            Robot = content.Load<SoundEffect>(@"ContentFiles/Sound/Foley/Robot");

            mainX = mainMenuX.CreateInstance();

            l1 = finalLayer1.CreateInstance();
            l2 = finalLayer2.CreateInstance();
            l3 = finalLayer3.CreateInstance();
            landingInstance = landing.CreateInstance();
            doorOpen = DoorOpen.CreateInstance();
            doorClose = DoorClose.CreateInstance();
            robotSound = Robot.CreateInstance();
            wireSound = WireFX.CreateInstance();

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

            GameSounds = new List<SoundEffectInstance>();
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
