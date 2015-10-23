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

        int width;
        int height;

        Camera moveCamera;
        KeyboardState kbState;
        KeyboardState previousKbState;

        public enum GameState
        {
            Menu,
            Level,
            Quit
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
            player = new Player(100,100,45,115); // TODO: give player actual rectangle values

            world = new World(@"world", s); // TODO: get path to world directory

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

            // TODO: use this.Content to load your game content here
            player.ObjImage = Content.Load<Texture2D>(player.imageLoc);

            Texture2D floorTexture = Content.Load<Texture2D>(GameVariables.imgFloor);
            Texture2D wallTexture = Content.Load<Texture2D>(GameVariables.imgWall);
            Texture2D spikeTexture = Content.Load<Texture2D>(GameVariables.imgSpike);
            Texture2D doorTexture = Content.Load<Texture2D>(GameVariables.imgDoor);
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
            player.Movement(kbState, gameTime);
            player.Collisions(world.Levels[world.currentLevel].objects, kbState, previousKbState, world);
            moveCamera.viewMatrix = moveCamera.GetTranform(player, width, height);

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

            foreach (GameObject item in world.Levels[world.currentLevel].objects)
            {
                spriteBatch.Draw(item.ObjImage, item.ObjRect, Color.White);
            }

            spriteBatch.Draw(player.ObjImage, player.ObjRect, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
