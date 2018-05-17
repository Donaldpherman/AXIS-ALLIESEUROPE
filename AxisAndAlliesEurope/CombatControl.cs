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
    public static class CombatControl
    {
        static int alliedCasualties;
        static int axisCasualties;
        static Random _r = new Random();
        static int indexOfTerritoryWhereBattle;
        static Texture2D sprite;
        static int indexOfSelectedAdjacentTerritory;

        public static void searchForCombat(ref MapController mapController, Game1 game)
        {
            for (int i = 0; i < mapController.getArrayOfTerritories().Length; ++i)
            {
                if (mapController.getArrayOfTerritories()[i] != null)
                    if (checkForCombatInTerritory(i, ref mapController, game.Content))
                    {
                        if (alliedCasualties > 0 || axisCasualties > 0)
                        {
                            game.mCurrentScreen = Game1.ScreenState.SelectCasualties;
                            return;
                        }
                        return;
                    }
            }
            game.mCurrentScreen = Game1.ScreenState.Map;
            mapController.movement = MapController.Movement.NonCombat;
        }

        private static bool checkForCombatInTerritory(int IndexOfTerritory, ref MapController mapController, ContentManager theContent)
        {
            foreach (Unit unit in mapController.getArrayOfTerritories()[IndexOfTerritory].getArrayListOfUnits())
            {
                if ( (string.Compare(unit.getWorldPower(), "Soviet Union", true)) == 0)
                {
                    foreach (Unit unitToCompare in mapController.getArrayOfTerritories()[IndexOfTerritory].getArrayListOfUnits())
                    {
                        if (string.Compare(unitToCompare.getWorldPower(), "Germany", true) == 0)
                        {
                            indexOfTerritoryWhereBattle = IndexOfTerritory;
                            battleBoard(ref mapController);
                            return true;
                        }
                    }
                }

                if ((string.Compare(unit.getWorldPower(), "Great Britain", true)) == 0)
                {
                    foreach (Unit unitToCompare in mapController.getArrayOfTerritories()[IndexOfTerritory].getArrayListOfUnits())
                    {
                        if (string.Compare(unitToCompare.getWorldPower(), "Germany", true) == 0)
                        {
                            indexOfTerritoryWhereBattle = IndexOfTerritory;
                            battleBoard(ref mapController);
                            return true;
                        }
                    }
                }

                if ((string.Compare((unit.getWorldPower()), "United States", true)) == 0)
                {
                    foreach (Unit unitToCompare in mapController.getArrayOfTerritories()[IndexOfTerritory].getArrayListOfUnits())
                    {
                        if (string.Compare(unitToCompare.getWorldPower(), "Germany", true) == 0)
                        {
                            indexOfTerritoryWhereBattle = IndexOfTerritory;
                            battleBoard(ref mapController);
                            return true;
                        }
                    }
                }

                if ((string.Compare((unit.getWorldPower()), "Germany", true)) == 0)
                {
                    foreach (Unit unitToCompare in mapController.getArrayOfTerritories()[IndexOfTerritory].getArrayListOfUnits())
                    {
                        if ((string.Compare(unitToCompare.getWorldPower(), "Soviet Union", true)) == 0)
                        {
                            indexOfTerritoryWhereBattle = IndexOfTerritory;
                            battleBoard(ref mapController);
                            return true;
                        }

                        if ((string.Compare((unitToCompare.getWorldPower()), "United States", true)) == 0)
                        {
                            indexOfTerritoryWhereBattle = IndexOfTerritory;
                            battleBoard(ref mapController);
                            return true;
                        }

                        if ((string.Compare((unitToCompare.getWorldPower()), "Great Britain", true)) == 0)
                        {
                            indexOfTerritoryWhereBattle = IndexOfTerritory;
                            battleBoard(ref mapController);
                            return true;
                        }

                    }
                }

                if ((string.Compare((unit.getWorldPower()), (mapController.getArrayOfTerritories()[IndexOfTerritory].getWhoControlsTerritory()), true)) != 0)
                {
                    mapController.setWhoControlsTerritory(unit.getWorldPower(), IndexOfTerritory, theContent);
                }
            }
            return false;
        }

        private static void battleBoard(ref MapController mapController)
        {
            // reset casualties for next battle round.
            alliedCasualties = 0;
            axisCasualties = 0;
            foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
            {
                if (unit.hit(mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getWhoControlsTerritory(), mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
                {
                    if (String.Compare(unit.getWorldPower(), "Germany", true) == 0)
                    {
                        alliedCasualties++;
                    }
                    else
                        axisCasualties++;
                }
            }
        }

        public static void drawSelectedCasualties(ContentManager theContent, ref MapController mapController, SpriteBatch spriteBatch, Player player)
        {
            Territory[] arrayOfTerritories = new Territory[100];
            arrayOfTerritories = mapController.getArrayOfTerritories();
            int numberOfInfanty = 0;
            int numberOfInfantyAxis = 0;
            foreach (Unit unit in ((arrayOfTerritories[indexOfTerritoryWhereBattle]).getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Infantry", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfInfantyAxis++;
                    else
                        numberOfInfanty++;
                }
            }



            int numberOfArmor = 0;
            int numberOfArmorAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Armor", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfArmorAxis++;
                    else
                        numberOfArmor++;
                }
            }

            int numberOfArtillery = 0;
            int numberOfArtilleryAxis =  0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Artillery", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfArtilleryAxis++;
                    else
                        numberOfArtillery++;
                }
            }

            int numberOfAntiAircraftGun = 0;
            int numberOfAntiAircraftGunAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "AntiAircraftGun", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfAntiAircraftGunAxis++;
                    else
                        numberOfAntiAircraftGun++;
                }
            }

            int numberOfFighter = 0;
            int numberOfFighterAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Fighter", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfFighterAxis++;
                    else
                        numberOfFighter++;
                }
            }

            int numberOfBomber = 0;
            int numberOfBomberAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Bomber", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfBomberAxis++;
                    else
                        numberOfBomber++;
                }
            }

            int numberOfBattleship = 0;
            int numberOfBattleshipAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Battleship", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfBattleshipAxis++;
                    else
                        numberOfBattleship++;
                }
            }

            int numberOfAirCraftCarrier = 0;
            int numberOfAirCraftCarrierAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "AirCraftCarrier", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfAirCraftCarrierAxis++;
                    else
                        numberOfAirCraftCarrier++;
                }
            }

            int numberOfTransport = 0;
            int numberOfTransportAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Transport", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfTransportAxis++;
                    else
                        numberOfTransport++;
                }
            }

            int numberOfDestroyer = 0;
            int numberOfDestroyerAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Destroyer", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberOfDestroyerAxis++;
                    else
                        numberOfDestroyer++;
                }
            }

            int numberofSubmarine = 0;
            int numberofSubmarineAxis = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfTerritoryWhereBattle].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Submarine", true)) == 0)
                {
                    if ((string.Compare(unit.getWorldPower(), "Germany", true)) == 0)
                        numberofSubmarineAxis++;
                    else
                        numberofSubmarine++;
                }
            }

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
            SpriteSortMode.Immediate, SaveStateMode.None);

            sprite = theContent.Load<Texture2D>("UnitSelection\\casualty Selection Screen");

            spriteBatch.Draw(sprite, (new Vector2(0, 0)), Color.White);

            SpriteFont font;
            SpriteFont fontHighLighted;
            int highLightedUnits = 0;

            font = theContent.Load<SpriteFont>("Fonts\\GameFont");
            fontHighLighted = theContent.Load<SpriteFont>("Fonts\\GameFontHighLighted");

            spriteBatch.DrawString(font,
            arrayOfTerritories[indexOfTerritoryWhereBattle].getNameOfTerritory(), (
            new Vector2(510, 25)),
            Color.Black);

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

            // Axis Side

            spriteBatch.DrawString(font,
                "" + numberOfInfantyAxis,
                (new Vector2(163 + 816, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfBomberAxis,
                (new Vector2(163 + 816, 398)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfTransportAxis,
                (new Vector2(163 + 816, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
               "" + numberOfArtilleryAxis,
               (new Vector2(322 + 816, 255)),
               Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfBattleshipAxis,
                (new Vector2(322 + 816, 398)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberofSubmarineAxis,
                (new Vector2(322 + 816, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
            "" + numberOfArmorAxis,
            (new Vector2(503 + 816, 255)),
            Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfAntiAircraftGunAxis,
                (new Vector2(503 + 816, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
               "" + numberOfAirCraftCarrierAxis,
               (new Vector2(503 + 816, 398)),
               Color.Black);

            spriteBatch.DrawString(font,
            "" + numberOfFighterAxis,
            (new Vector2(684 + 816, 255)),
            Color.Black);

            spriteBatch.DrawString(font,
            "" + numberOfDestroyerAxis,
            (new Vector2(684 + 816, 398)),
            Color.Black);

            spriteBatch.DrawString(font,
            "Current Territory      " + arrayOfTerritories[indexOfTerritoryWhereBattle].getNameOfTerritory(), (
            new Vector2(665, 687)),
            Color.Black);

            spriteBatch.DrawString(font,
            "Allied Casualties      " + alliedCasualties, (
            new Vector2(0, 687)),
            Color.Black);

            spriteBatch.DrawString(font,
            "Axis Casualties        " + axisCasualties, (
            new Vector2(1200, 687)),
            Color.Black);

            if (indexOfTerritoryWhereBattle > - 1)
            {
                    spriteBatch.DrawString(font,
                    "Territory to retreat to:   " +
                    (mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getDictionaryOfAdjacentTerritories()
                    .ElementAt(indexOfSelectedAdjacentTerritory).Key),
                    (new Vector2(665, 787)),
                    Color.Black);
            }

            if (indexOfTerritoryWhereBattle == -1)
            {
                spriteBatch.DrawString(font,
                    "Stand and Die",
                    (new Vector2(665, 787)),
                     Color.Black);
            }

            Vector2 pos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            spriteBatch.Draw(mapController.getMousePointerSprite(ref player), pos, Color.White);

            spriteBatch.End();
        }
        /// <summary>
        /// User selects casualties to remove from Territories ArrayList
        /// </summary>
        /// <param name="theContent"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="game"></param>
        /// <param name="previousMouseState"></param>
        /// <param name="mapController"></param>
        public static void selectCasualties(ContentManager theContent, SpriteBatch spriteBatch, Game1 game, MouseState previousMouseState, ref MapController mapController, Player player)
        {

            // remove Allied Infantry

            if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed)
                && previousMouseState.LeftButton.Equals(ButtonState.Released))
            {
                if (alliedCasualties > 0)
                {
                    // remove Allied Infantry
                    if (new Rectangle(54, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Infantry", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Allied Bomber
                    if (new Rectangle(54, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                            foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {                               
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Bomber", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Allied Transport
                    if (new Rectangle(54, 426, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Transport", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Allied Artillery
                    if (new Rectangle(230, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Artillery", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Allied Battleship
                    if (new Rectangle(230, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Battleship", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Allied Submarine
                    if (new Rectangle(230, 426, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Submarine", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Allied Armor
                    if (new Rectangle(417, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Armor", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }
                        }
                    }

                    // remove Allied Carrier
                    if (new Rectangle(417, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "AirCraftCarrier", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }
                        }
                    }

                    // remove AntiAircraftGun can't destroyed.

                    // remove Allied Fighter
                    if (new Rectangle(599, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Fighter", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }
                        }
                    }

                    // remove Allied Destroyer
                    if (new Rectangle(599, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 1) &&
                                (string.Compare(unit.getType(), "Destroyer", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                alliedCasualties--;
                                break;
                            }
                        }
                    } 
                }

                if (axisCasualties > 0)
                {
                    // remove Axis Infantry
                    if (new Rectangle(54 + 816, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Infantry", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Axis Bomber
                    if (new Rectangle(54 + 816, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Bomber", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Axis Transport
                    if (new Rectangle(54 + 816, 426, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Transport", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Axis Artillery
                    if (new Rectangle(230 + 816, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Artillery", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Axis Battleship
                    if (new Rectangle(230 + 816, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Battleship", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Axis Submarine
                    if (new Rectangle(230 + 816, 426, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Submarine", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }

                        }
                    }

                    // remove Axis Armor
                    if (new Rectangle(417 + 816, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Armor", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }
                        }
                    }

                    // remove Axis Carrier
                    if (new Rectangle(417 + 816, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "AirCraftCarrier", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }
                        }
                    }

                    // remove AntiAircraftGun can't destroyed.

                    // remove Axis Fighter
                    if (new Rectangle(599 + 816, 141, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Fighter", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }
                        }
                    }

                    // remove Axis Destroyer
                    if (new Rectangle(599 + 816, 285, 171, 141).Intersects(mapController.getRectangleOfMousePointerSprite(ref player)))
                    {
                        foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
                        {
                            if (
                                (string.Compare(unit.getWorldPower(), "Germany", true) == 0) &&
                                (string.Compare(unit.getType(), "Destroyer", true) == 0)
                                )
                            {
                                mapController.removeUnitFromSelectedTerritory(unit, indexOfTerritoryWhereBattle);
                                axisCasualties--;
                                break;
                            }
                        }
                    }
                }
            }

            // checking for end of battle
            bool axisUnitLeft = false;
            bool alliedUnitLeft = false;
            foreach (Unit unit in mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits())
            {
                if ((string.Compare(unit.getWorldPower(), "Germany", true) == 0))
                {
                    if (unit.getType() == "AntiAircraftGun")
                        break;
                    axisUnitLeft = true;
                }

                    if ((string.Compare(unit.getWorldPower(), "Germany", true) == 1))
                {
                    if (unit.getType() == "AntiAircraftGun")
                        break;
                    alliedUnitLeft = true;
                }
            }

            if (!axisUnitLeft)
            {
                axisCasualties = 0;
                mapController.setWhoControlsTerritory(
                    ((Unit)(mapController.getArrayOfTerritories()[indexOfTerritoryWhereBattle].getArrayListOfUnits()[0])).getWorldPower(),
                    indexOfTerritoryWhereBattle, theContent);
            }
            if (!alliedUnitLeft)
            {
                alliedCasualties = 0;
                mapController.setWhoControlsTerritory("Germany", indexOfTerritoryWhereBattle, theContent);
            }

            if ((alliedCasualties == 0) && (axisCasualties == 0))
                game.mCurrentScreen = Game1.ScreenState.Battleboard;


        }
        public static int rollDice()
        {
            return _r.Next(1, 7);
        }
    }
}
    