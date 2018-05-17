using System;
using System.IO;
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
using System.Collections;

namespace AxisAndAlliesEurope
{
    public static class PurchaseCombatUnit
    {
        static Texture2D sprite;

        /// <summary>
        /// Used to draw the Unit Purchase Menu
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="theContent"></param>
        public static void showListOfCombatUnitsForPurchase(SpriteBatch spriteBatch, Player player, ContentManager theContent, MapController mapController)
        {
            int numberOfInfanty = 0;
            int numberOfArmor = 0;
            int numberOfArtillery = 0;
            int numberOfAntiAircraftGun = 0;
            int numberOfFighter = 0;
            int numberOfBomber = 0;
            int numberOfBattleship = 0;
            int numberOfAirCraftCarrier = 0;
            int numberOfTransport = 0;
            int numberOfDestroyer = 0;
            int numberofSubmarine = 0;

            if (!(player.getListOfPurchasedUnits() == null))
                foreach (Unit unit in player.getListOfPurchasedUnits())
                {
                    if ((string.Compare(unit.getType(), "Infantry", true)) == 0)
                        numberOfInfanty++;
                    if ((string.Compare(unit.getType(), "Armor", true)) == 0)
                        numberOfArmor++;
                    if ((string.Compare(unit.getType(), "Artillery", true)) == 0)
                        numberOfArtillery++;
                    if ((string.Compare(unit.getType(), "AntiAircraftGun", true)) == 0)
                        numberOfAntiAircraftGun++;
                    if ((string.Compare(unit.getType(), "Fighter", true)) == 0)
                        numberOfFighter++;
                    if ((string.Compare(unit.getType(), "Bomber", true)) == 0)
                        numberOfBomber++;
                    if ((string.Compare(unit.getType(), "Battleship", true)) == 0)
                        numberOfBattleship++;
                    if ((string.Compare(unit.getType(), "AirCraftCarrier", true)) == 0)
                        numberOfAirCraftCarrier++;
                    if ((string.Compare(unit.getType(), "Transport", true)) == 0)
                        numberOfTransport++;
                    if ((string.Compare(unit.getType(), "Destroyer", true)) == 0)
                        numberOfDestroyer++;
                    if ((string.Compare(unit.getType(), "Submarine", true)) == 0)
                        numberofSubmarine++;
                }

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
            SpriteSortMode.Immediate, SaveStateMode.None);

            sprite = theContent.Load<Texture2D>("UnitSelection\\PurchaseUnitSelectionScreen");

            spriteBatch.Draw(sprite, (new Vector2(0, 0)), Color.White);

            SpriteFont font;
            SpriteFont fontHighLighted;
            int highLightedUnits = 0;

            font = theContent.Load<SpriteFont>("Fonts\\GameFont");
            fontHighLighted = theContent.Load<SpriteFont>("Fonts\\GameFontHighLighted");

            spriteBatch.DrawString(font,
                "" + numberOfInfanty,
                (new Vector2(163, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfBomber,
                (new Vector2(163, 398)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfTransport,
                (new Vector2(163, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfArtillery,
                (new Vector2(322, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfBattleship,
                (new Vector2(322, 398)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberofSubmarine,
                (new Vector2(322, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfArmor,
                (new Vector2(503, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfAntiAircraftGun,
                (new Vector2(503, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
               "" + numberOfAirCraftCarrier,
               (new Vector2(503, 398)),
               Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfFighter,
                (new Vector2(684, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfDestroyer,
                (new Vector2(684, 398)),
                Color.Black);

            spriteBatch.DrawString(font,
            "" + player.getWhosTurn(), (
            new Vector2(665, 687)),
            Color.Black);

            spriteBatch.DrawString(font,
            "Number of IPC: " + player.getIPC(), (
            new Vector2(665, 787)),
            Color.Black);

            spriteBatch.DrawString(font,
            "Hit Enter when done", (
            new Vector2(665, 887)),
            Color.Black);

            Vector2 pos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            spriteBatch.Draw(mapController.getMousePointerSprite(ref player), new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);

            spriteBatch.End();
        }

        public static void selectListOfCombatUnitsForPurchase(Game1 game, KeyboardState previousKeyboardState, MapController mapController, Player player,
            MouseState previousMouseState)
        {
            if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed)
                && previousMouseState.LeftButton.Equals(ButtonState.Released))
            {

                // purchase Infantry
                if (new Rectangle(54, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Infantry(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Infantry(player.getWhosTurn()));
                        player.reduceIPC((new Infantry(player.getWhosTurn()).getCost()));
                    }
                }

                // purchase Bomber
                if (new Rectangle(54, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Bomber(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Bomber(player.getWhosTurn()));
                        player.reduceIPC(new Bomber(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Transport
                if (new Rectangle(54, 426, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Transport(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Transport(player.getWhosTurn()));
                        player.reduceIPC(new Transport(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Artillery
                if (new Rectangle(230, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Artillery(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Artillery(player.getWhosTurn()));
                        player.reduceIPC(new Artillery(player.getWhosTurn()).getCost());
                    }
                }

                // purhcase Battleship
                if (new Rectangle(230, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Battleship(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Battleship(player.getWhosTurn()));
                        player.reduceIPC(new Battleship(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Submarine
                if (new Rectangle(230, 426, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Submarine(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Submarine(player.getWhosTurn()));
                        player.reduceIPC(new Submarine(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Armor
                if (new Rectangle(417, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Armor(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Armor(player.getWhosTurn()));
                        player.reduceIPC(new Armor(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Carrier
                if (new Rectangle(417, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new AirCraftCarrier(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new AirCraftCarrier(player.getWhosTurn()));
                        player.reduceIPC(new AirCraftCarrier(player.getWhosTurn()).getCost());
                    }
                }

                    // purchase AntiAircraftGun.
                if (new Rectangle(417, 426, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new AntiAircraftGun(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new AntiAircraftGun(player.getWhosTurn()));
                        player.reduceIPC(new AntiAircraftGun(player.getWhosTurn()).getCost());
                    }
                }

                    // purchase Fighter
                if (new Rectangle(599, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Fighter(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Fighter(player.getWhosTurn()));
                        player.reduceIPC(new Fighter(player.getWhosTurn()).getCost());
                    }
                }

                    // purchase Destroyer
                if (new Rectangle(599, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Destroyer(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Destroyer(player.getWhosTurn()));
                        player.reduceIPC(new Destroyer(player.getWhosTurn()).getCost());
                    }

                }
            }

                KeyboardState keyboardState = Keyboard.GetState();
                if ((keyboardState.IsKeyDown(Keys.Enter)) && previousKeyboardState.IsKeyUp(Keys.Enter))
                {
                    game.mCurrentScreen = Game1.ScreenState.Map;
                }          
        }
    }
}
