using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace GDAPS_Project_2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        World world;
        double time;
        StreamReader s;
        GameState g;
        GameState prevState;
        Menus m;
        int width;
        int height;
        Hud gameHUD;
        bool paused;

        Camera moveCamera;
        KeyboardState kbState;
        KeyboardState previousKbState;

        public enum GameState
        {
            Menu,
            Level,
            Pause,
            Minigame
        }
        public enum Menus
        {
            Start,
            Help,
            Options,
            LevelSelect,
            AboutUs
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player(100, 100, 256, 246); // TODO: give player actual rectangle values

            g = GameState.Menu;
            m = Menus.Start;
            paused = false;

            world = new World(GameVariables.menuWorld, s); // Menu "world"

            // x y width height are temporary filler values
            // gameHUD = new Hud(50, 20, 700, 180, spriteBatch, player, world.Levels[0].HudInfo, (GameVariables.menuWorld + " - " + (world.currentLevel + 1).ToString()));

            player.ObjPos.X = world.Levels[0].playerSpawn.X;
            player.ObjPos.Y = world.Levels[0].playerSpawn.Y;

            moveCamera = new Camera(player, GraphicsDevice);

            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            // TODO: use this.Content to load your game content here
            player.ObjImage = Content.Load<Texture2D>(GameVariables.imgPlayer);
            //player.bottHit.ObjImage = Content.Load<Texture2D>(@"Images/Sprites/imgHitbox");
            //player.topHit.ObjImage = Content.Load<Texture2D>(@"Images/Sprites/imgHitbox");
            //player.leftHit.ObjImage = Content.Load<Texture2D>(@"Images/Sprites/imgHitbox");
            //player.rightHit.ObjImage = Content.Load<Texture2D>(@"Images/Sprites/imgHitbox");
            Texture2D floorTexture = Content.Load<Texture2D>(GameVariables.imgFloor);
            Texture2D wallTexture = Content.Load<Texture2D>(GameVariables.imgWall);
            Texture2D spikeTexture = Content.Load<Texture2D>(GameVariables.imgSpike);
            Texture2D doorTexture = Content.Load<Texture2D>(GameVariables.imgDoor);
            //Texture2D hud = Content.Load<Texture2D>(GameVariables.imgHUD);
            foreach (Level loadLevel in world.Levels)
            {
                foreach (GameObject item in loadLevel.objects)
                {
                    string itemType = item.ToString();
                    switch (itemType)
                    {
                        case "GDAPS_Project_2.Floor":
                            item.ObjImage = floorTexture;
                            break;

                        case "GDAPS_Project_2.Wall":
                            item.ObjImage = wallTexture;
                            break;

                        case "GDAPS_Project_2.Spike":
                            item.ObjImage = spikeTexture;
                            break;

                        case "GDAPS_Project_2.Door":
                            item.ObjImage = doorTexture;
                            break;
                    }
                }
                // gameHUD.ObjImage = hud;
            }
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        public static bool SingleKeyPress(Keys k, KeyboardState current, KeyboardState previous)
        {
            return (current.IsKeyDown(k) && previous.IsKeyUp(k));
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            previousKbState = kbState;
            kbState = Keyboard.GetState();
            if (SingleKeyPress(Keys.P, kbState, previousKbState) && g != GameState.Menu)
            {
                if (!paused)
                {
                    prevState = g;
                    g = GameState.Pause;
                    paused = true;
                }
                else
                {
                    g = prevState;
                    paused = false;
                }
            }
            if (g != GameState.Pause)
            {
                player.Movement(kbState, gameTime);
                player.Collisions(kbState, previousKbState, ref world, s);
                moveCamera.viewMatrix = moveCamera.GetTransform(player, width, height);

                player.Update(gameTime);
            }
            if(g == GameState.Level && world.Levels[world.currentLevel].HudInfo != null)
            {
                //gameHUD.checkPlayerY();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, moveCamera.viewMatrix);

            player.Draw(spriteBatch);   //Player draw method

            foreach (GameObject item in world.Levels[world.currentLevel].objects)
            {
                item.spriteDraw(spriteBatch);
            }
            //player.bottHit.spriteDraw(spriteBatch);
            //player.topHit.spriteDraw(spriteBatch);
            //player.leftHit.spriteDraw(spriteBatch);
            //player.rightHit.spriteDraw(spriteBatch);
            player.spriteDraw(spriteBatch);
            // if (world.Levels[world.currentLevel].HudInfo != null)
            //{
            //    gameHUD.spriteDraw(spriteBatch);
            //}
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
