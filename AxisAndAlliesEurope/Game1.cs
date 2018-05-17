using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AxisAndAlliesEurope
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MapController mapController;
        MouseState previousMouseState;
        KeyboardState previousKeyboardState;
        Player player;
            
        public enum ScreenState
        {
            StartMenu,
            Purchase,
            Map,
            MoveUnits,
            Battleboard,
            SelectCasualties,
            Victory
        }

        public ScreenState mCurrentScreen; //The current screen state

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
            graphics.PreferredBackBufferWidth = 1680;
            graphics.PreferredBackBufferHeight = 945;
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            mCurrentScreen = ScreenState.Purchase; //The current screen state

            graphics.ToggleFullScreen();

            // Change of resolution.

            mapController = new MapController(this.Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            previousMouseState = new MouseState();
            player = new Player(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            #region Exit methods
            KeyboardState keyboardState = Keyboard.GetState();
            // Allows the game to exit with escape`
            if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                this.Exit();

            #endregion

            switch (mCurrentScreen)
            {
                case ScreenState.StartMenu:
                    {
                        break;
                    }
                case ScreenState.Purchase:
                    {
                        PurchaseCombatUnit.selectListOfCombatUnitsForPurchase(this, previousKeyboardState, mapController, player, previousMouseState);
                        break;
                    }
                case ScreenState.Map:
                    {
                        mapController.moveMap(this, ref player, previousKeyboardState); // moves the map around using the arrow keys.
                        mapController.selectTerritory(this.Content, spriteBatch, this, previousMouseState, ref player); // check it user clicks on territory.
                        break;                      
                    }

                case ScreenState.MoveUnits:
                    {
                        mapController.unitSelectionScreen(this, previousKeyboardState, ref player, previousMouseState);
                        break;
                    }
                case ScreenState.Battleboard:
                    {
                        CombatControl.searchForCombat(ref mapController, this);
                        break;
                    }
                case ScreenState.SelectCasualties:
                    {
                        CombatControl.selectCasualties(this.Content, spriteBatch, this, previousMouseState, ref mapController, player);
                        break;
                    }
                case ScreenState.Victory:
                    {
                        break;
                    }
            }

            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            switch (mCurrentScreen)
            {
                case ScreenState.StartMenu:
                    {
                        break;
                    }
                case ScreenState.Purchase:
                    {
                        PurchaseCombatUnit.showListOfCombatUnitsForPurchase(spriteBatch, player, this.Content, mapController);
                        break;
                    }                       
                case ScreenState.Map:
                    {
                        mapController.Draw(spriteBatch, this.Content, ref player); // draws the map
                        break;
                    }

                case ScreenState.MoveUnits:
                    {
                        mapController.drawUnitSelectionScreen(this, this.Content, spriteBatch, player, previousMouseState); // draws select units screan.
                        break;
                    }

                case ScreenState.Battleboard:
                    {
                        // no drawing
                        break;
                    }

                case ScreenState.SelectCasualties:
                    {
                        CombatControl.drawSelectedCasualties(this.Content, ref mapController, spriteBatch, player);
                        break;
                    }
                case ScreenState.Victory:
                    {
                        Victory.drawVictory(this,ref player, spriteBatch, this.Content);
                        break;
                    }
            }
            base.Draw(gameTime);          
        }
    }
}
