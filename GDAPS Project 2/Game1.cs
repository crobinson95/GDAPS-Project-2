using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;

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
        Stopwatch time;
        static World world;
        GameState g;
        GameState prevState;
        // Menus m;
        int width;
        int height;
        Hud gameHUD;
        bool paused;
        StreamReader s = null;

        SoundLoop falling;

        Texture2D pauseBack;
        SpriteFont gameFont;
        SpriteFont gameFont2;

        Texture2D deathScreen;
        Texture2D victoryScreen;

        AnimatedTexture menu;

        Camera moveCamera;
        KeyboardState kbState;
        KeyboardState previousKbState;

        public enum GameState
        {
            Menu,
            Level,
            Pause,
            Dead,
            Victory
            //Minigame
        }
        /*public enum Menus
        {
            Start,
            Help,
            Options,
            LevelSelect,
            AboutUs
        }*/

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 540;
            graphics.PreferredBackBufferWidth = 960;
            time = new Stopwatch();
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
            player = new Player(100, 100, 30, 54);

            g = GameState.Menu;
            // m = Menus.Start;
            paused = false;

            SoundEffect.DistanceScale = 75f;

            world = new World(GameVariables.menuWorld, s, player, Content); // Menu "world"
            world.LoadWorld();

            // x y width height are temporary filler values
            // gameHUD = new Hud(50, 20, 700, 180, spriteBatch, player, world.Levels[0].HudInfo, (GameVariables.menuWorld + " - " + (world.currentLevel + 1).ToString()));

            player.ObjPos.X = world.levels["main.txt"].playerSpawn.X;
            player.ObjPos.Y = world.levels["main.txt"].playerSpawn.Y;
            world.currentLevel = "main.txt";

            moveCamera = new Camera(player, GraphicsDevice);
            gameHUD = new Hud((int)moveCamera.camX + 20, (int)moveCamera.camY + 20, 300, 45, spriteBatch, player, moveCamera, world.levels[world.currentLevel]);
            //MediaPlayer.IsRepeating = true;

            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            GameVariables.LoadContentFiles(Content);

            // TODO: use this.Content to load your game content here

            pauseBack = Content.Load<Texture2D>(GameVariables.imgWall);
            gameFont = Content.Load<SpriteFont>(GameVariables.gFont);
            gameFont2 = Content.Load<SpriteFont>(GameVariables.gFont2);
            deathScreen = Content.Load<Texture2D>(@"ContentFiles/Images/Sprites/death");
            victoryScreen = Content.Load<Texture2D>(@"ContentFiles/Images/Sprites/Victory");
            menu = new AnimatedTexture(Content, @"ContentFiles/Images/Sprites/fronta", 5, .05f);
            world.LoadWorld();

            //Texture2D hud = Content.Load<Texture2D>(GameVariables.imgHUD);

            GameVariables.l1.IsLooped = true;
            GameVariables.l2.IsLooped = true;
            GameVariables.l3.IsLooped = true;
            GameVariables.l1.Volume = 0.4f * GameVariables.gameVolume;
            GameVariables.l2.Volume = 0.4f * GameVariables.gameVolume;
            GameVariables.l3.Volume = 0.4f * GameVariables.gameVolume;
            GameVariables.mainX.Volume = 1 * GameVariables.gameVolume;
            GameVariables.deadX.Volume = 1 * GameVariables.gameVolume;

            falling = new SoundLoop(GameVariables.fallingLoop.CreateInstance(), 700, GameVariables.fallingLoop.CreateInstance(), 700, GameVariables.fallingAcceleration.CreateInstance(), 850, 0.6f * GameVariables.gameVolume);
            gameHUD.backt = Content.Load<Texture2D>(@"ContentFiles/Images/Sprites/back");
            gameHUD.undert = Content.Load<Texture2D>(@"ContentFiles/Images/Sprites/grey");
            gameHUD.energyt = Content.Load<Texture2D>(@"ContentFiles/Images/Sprites/energy");
            //gameHUD.keyt = Content.Load<Texture2D>(@"ContentFiles/Images/Sprites/ki");
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        /// 
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        public static bool SingleKeyPress(Keys k, KeyboardState current, KeyboardState previous)
        {
            return (current.IsKeyDown(k) && previous.IsKeyUp(k));
        }

        public static void PlayRandomSound(List<SoundEffectInstance> soundList)
        {
            Random random = new Random();
            soundList[random.Next(soundList.Count)].Volume = 1.0f * GameVariables.gameVolume;
            soundList[random.Next(soundList.Count)].Play();
        }

        private void ResetLevel()
        {
            foreach (Enemy e in world.levels[world.currentLevel].enemies)
            {
                e.ObjPos.X = e.origin.X;
                e.ObjPos.Y = e.origin.Y;
                e.grav = MovableGameObject.gravDirection.Down;
                e.xVelocity = 0;
                e.yVelocity = 0;
                e.alive = true;
                if (e is Enemy | e is EnemyF)
                {
                    e.ObjRectHeight = 70;
                    e.ObjRectWidth = 50;
                }
            }
            player.ObjPos.X = world.levels[world.currentLevel].playerSpawn.X;
            player.ObjPos.Y = world.levels[world.currentLevel].playerSpawn.Y;
            player.grav = MovableGameObject.gravDirection.Down;
            player.xVelocity = 0;
            player.yVelocity = 0;
            player.PlayAnimation("Down_Idle_Right");
            player.ObjRectWidth = 30;
            player.ObjRectHeight = 54;
            player.energy = 280;
            world.levels[world.currentLevel].levelTimer.Reset();
            world.levels[world.currentLevel].levelTimer.Start();
        }

        private void Mute()
        {
            if (SingleKeyPress(Keys.M, kbState, previousKbState))
            {
                if (GameVariables.gameVolume == 1.0f)
                {
                    GameVariables.gameVolume = 0.0f;
                }
                else { GameVariables.gameVolume = 1.0f; }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            previousKbState = kbState;
            kbState = Keyboard.GetState();
            GameVariables.l1.Volume = 0.4f * GameVariables.gameVolume;
            GameVariables.l2.Volume = 0.4f * GameVariables.gameVolume;
            GameVariables.l3.Volume = 0.4f * GameVariables.gameVolume;
            GameVariables.mainX.Volume = 0.4f * GameVariables.gameVolume;
            GameVariables.deadX.Volume = 1 * GameVariables.gameVolume;

            if (g == GameState.Menu)
            {

                GameVariables.mainX.Play();

                moveCamera.viewMatrix = moveCamera.GetTransform(player, width, height);
                if (SingleKeyPress(Keys.Enter, kbState, previousKbState))
                {
                    GameVariables.mainX.Stop();
                    GameVariables.l1.Play();
                    GameVariables.l2.Play();
                    GameVariables.l3.Play();
                    g = GameState.Level;
                    world.levels[world.currentLevel].levelTimer.Start();
                }
                Mute();
                if (SingleKeyPress(Keys.F, kbState, previousKbState))
                {
                    graphics.ToggleFullScreen();
                }
            }

            if (g == GameState.Pause)
            {
                if (SingleKeyPress(Keys.R, kbState, previousKbState))
                {
                    GameVariables.deadX.Stop();
                    ResetLevel();
                    g = prevState;
                    paused = false;
                }
                if (SingleKeyPress(Keys.F, kbState, previousKbState))
                {
                    graphics.ToggleFullScreen();
                }

                Mute();

                if (SingleKeyPress(Keys.Q, kbState, previousKbState))
                {
                    Exit();
                }
            }

            if (g == GameState.Dead)
            {
                GameVariables.l1.Stop();
                GameVariables.l2.Stop();
                GameVariables.l3.Stop();
                GameVariables.deadX.Play();
                if (SingleKeyPress(Keys.R, kbState, previousKbState))
                {
                    ResetLevel();
                    g = GameState.Level;
                    GameVariables.deadX.Stop();
                    GameVariables.l1.Play();
                    GameVariables.l2.Play();
                    GameVariables.l3.Play();
                }
            }

            if (g == GameState.Victory)
            {
                world.levels[world.currentLevel].levelTimer.Stop();
                if (SingleKeyPress(Keys.Enter, kbState, previousKbState))
                {
                    g = GameState.Level;
                    player.victory = false;
                    world.levels[world.currentLevel].levelTimer.Start();
                }
                if (SingleKeyPress(Keys.F, kbState, previousKbState))
                {
                    graphics.ToggleFullScreen();
                }
                Mute();
            }

            if (g == GameState.Level)
            {
                player.Movement(kbState, previousKbState, gameTime, falling);
                player.Collisions(kbState, previousKbState, world, s, Content);
                foreach (Enemy enemy in world.levels[world.currentLevel].enemies)
                {            
                    if (enemy.alive)
                    {                         
                        enemy.Movement(gameTime);
                        enemy.Collisions(world.levels[world.currentLevel].objects, kbState, previousKbState, world);
                    }
                }
                if (player.IsDead())
                {
                    g = GameState.Dead;
                }

                if (player.world != null && player.world != world)
                {
                    world = player.world;
                    world.LoadWorld();
                    world.levels[world.currentLevel].levelTimer.Start();
                }

                if (player.victory)
                {
                    g = GameState.Victory;
                }

                moveCamera.viewMatrix = moveCamera.GetTransform(player, width, height);
                gameHUD.Check();
                player.Update(gameTime);
            }

            if (SingleKeyPress(Keys.P, kbState, previousKbState) && g != GameState.Menu && g != GameState.Victory && g != GameState.Dead)
            {
                if (!paused)
                {
                    prevState = g;
                    world.levels[world.currentLevel].levelTimer.Stop();
                    g = GameState.Pause;
                    paused = true;
                }
                else
                {
                    g = prevState;
                    world.levels[world.currentLevel].levelTimer.Start();
                    paused = false;
                }
            }

            //if(g == GameState.Level && world.levels[world.currentLevel].HudInfo != null)
            //{
            //    gameHUD.checkPlayerY();
            //}

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

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null, null, moveCamera.viewMatrix);

            if (g == GameState.Menu)
            {
                if (time.ElapsedMilliseconds > 1000) { time.Reset(); }

                time.Start();
                menu.DrawFrame(spriteBatch, 1, new Vector2(-moveCamera.camX, -moveCamera.camY), false, 0.75f);
                float elapsed = time.ElapsedMilliseconds;
                menu.UpdateFrame(elapsed / 1000);

                spriteBatch.DrawString(gameFont, "   Project\n   Inversion\n\n\n  Enter to Start!?", new Vector2(-moveCamera.camX + 400, -moveCamera.camY + 100), Color.White);
            }

            if (g == GameState.Level || g == GameState.Pause)
            {
                spriteBatch.Draw(GameVariables.starsBackground, new Rectangle(-(int)moveCamera.position.X, -(int)moveCamera.position.Y, width, height), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);

                foreach (GameObject item in world.levels[world.currentLevel].objects)
                {
                    item.spriteDraw(spriteBatch);
                }
                foreach (Enemy enemy in world.levels[world.currentLevel].enemies)
                {
                    enemy.spriteDraw(spriteBatch);
                }
                player.Draw(spriteBatch);   //Player draw method
                gameHUD.spriteDraw(spriteBatch);
                spriteBatch.DrawString(gameFont, "Energy", new Vector2(-moveCamera.camX + 25, -moveCamera.camY + 45), Color.Black, 0.0f, new Vector2(0, 0), 0.30f, SpriteEffects.None, 0.1f);
                if (paused)
                {
                    spriteBatch.Draw(pauseBack, new Rectangle(-(int)moveCamera.camX + 20, -(int)moveCamera.camY + 80, graphics.PreferredBackBufferWidth - 60, graphics.PreferredBackBufferHeight - 160), null, Color.White * 0.8f, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
                    spriteBatch.DrawString(gameFont, "R to reset\nQ to quit\nCurrent Level: " + world.currentLevel, new Vector2(-moveCamera.camX + 40, -moveCamera.camY + 100), Color.Black, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
                }
            }

            if (g == GameState.Victory)
            {
                spriteBatch.Draw(victoryScreen, new Rectangle(-(int)moveCamera.camX, -(int)moveCamera.camY, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(gameFont2, "Nice job!\n\n\nPress 'Enter' \nto continue", new Vector2(-moveCamera.camX + 40, -moveCamera.camY + 100), Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(gameFont2, "Time: " + world.levels[world.currentLevel].levelTimer.Elapsed.Seconds + "\nDeaths: " + world.levels[world.currentLevel].deathCount, new Vector2(-moveCamera.camX + 40, -moveCamera.camY + 185), Color.White, 0.0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0.0f);
            }

            if (g == GameState.Dead)
            {
                spriteBatch.Draw(deathScreen, new Rectangle(-(int)moveCamera.camX, -(int)moveCamera.camY, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(gameFont, "You died.\n\nPress 'R' \nto restart", new Vector2(-moveCamera.camX + 40, -moveCamera.camY + 100), Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            }

            // if (world.Levels[world.currentLevel].HudInfo != null)
            //{
            //    gameHUD.spriteDraw(spriteBatch);
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
