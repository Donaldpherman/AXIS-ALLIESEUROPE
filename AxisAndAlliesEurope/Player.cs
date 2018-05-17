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
    public class Player
    {
        private WorldPower germany;
        private WorldPower sovietUnion;
        private WorldPower greatBritain;
        private WorldPower unitedStates;

        private enum WhosTurn
        {
            Germany,
            SovietUnion,
            GreatBritain,
            UnitedStates
        }
        private WhosTurn currentPlayer; //The current screen state
        public Player(ContentManager theContent)
        {
            germany = new WorldPower("Germany", 12, 40, "mousePointers\\200px-National_flag_of_Germany_1933-1935.svg", theContent);
            sovietUnion = new WorldPower("Soviet Union", 3, 24, "mousePointers\\soviet_flag", theContent);
            greatBritain = new WorldPower("Great Britain", 3, 25, "mousePointers\\british_flag", theContent);
            unitedStates = new WorldPower("United States", 4, 40, "mousePointers\\buy_usa_48_star_flag", theContent);
        }
        public String getWhosTurn()
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        return germany.getName();
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                        return sovietUnion.getName();
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        return greatBritain.getName();
                        break;
                    }
                case WhosTurn.UnitedStates:
                    {
                        return unitedStates.getName();
                        break;
                    }
            }
            return "junk";
        }

        public void nextWorldPower(Game1 game, MapController mapController)
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        checkForWin(game, mapController);
                        currentPlayer = WhosTurn.SovietUnion;
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                        checkForWin(game, mapController);
                        currentPlayer = WhosTurn.GreatBritain;
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        checkForWin(game, mapController);
                        currentPlayer = WhosTurn.UnitedStates;
                        break;
                    }
                case WhosTurn.UnitedStates:
                    {
                        checkForWin(game, mapController);
                        currentPlayer = WhosTurn.Germany;
                        break;
                    }
            }
        }

        private void checkForWin(Game1 game, MapController mapController)
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        // check for Soviet Union Victory 48 = Germany, 75 = Moscow
                        if ((mapController.getArrayOfTerritories()[48].getWhoControlsTerritory() == "Soviet Union") &&
                        (mapController.getArrayOfTerritories()[48].getWhoControlsTerritory() == "Soviet Union"))
                        {
                            game.mCurrentScreen = Game1.ScreenState.Victory;
                        }
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                        // check for GB Victory 48 = Geramny 38 = United Kingdom
                        if ((mapController.getArrayOfTerritories()[48].getWhoControlsTerritory() == "Great Britain") &&
                        (mapController.getArrayOfTerritories()[38].getWhoControlsTerritory() == "Great Britain"))
                        {
                            game.mCurrentScreen = Game1.ScreenState.Victory;
                        }
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        // check for US Victory 48 = Geramny 1 = United States
                        if ((mapController.getArrayOfTerritories()[48].getWhoControlsTerritory() == "United States") &&
                        (mapController.getArrayOfTerritories()[1].getWhoControlsTerritory() == "United States"))
                        {
                            game.mCurrentScreen = Game1.ScreenState.Victory;
                        }
                        break;
                    }
                case WhosTurn.UnitedStates:
                    {
                        // check for Germany Victory 48 = Geramny 1 = United States
                        if ((mapController.getArrayOfTerritories()[48].getWhoControlsTerritory() == "Germany"))
                        {
                            if ((mapController.getArrayOfTerritories()[1].getWhoControlsTerritory() == "Germany"))
                            {
                                game.mCurrentScreen = Game1.ScreenState.Victory;
                            }
                            if ((mapController.getArrayOfTerritories()[75].getWhoControlsTerritory() == "Germany"))
                            {
                                game.mCurrentScreen = Game1.ScreenState.Victory;
                            }
                            if ((mapController.getArrayOfTerritories()[38].getWhoControlsTerritory() == "Germany"))
                            {
                                game.mCurrentScreen = Game1.ScreenState.Victory;
                            }
                        }
                        break;
                    }
            }

        }

        public Texture2D getMousePointerSprite()
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        return germany.getMousePointerSprite();
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                       return  sovietUnion.getMousePointerSprite();
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        return greatBritain.getMousePointerSprite();
                        break;
                    }
                case WhosTurn.UnitedStates:
                    {
                        return unitedStates.getMousePointerSprite();
                        break;
                    }
            }
            return unitedStates.getMousePointerSprite();
        }

        public List<Unit> getListOfPurchasedUnits()
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        return germany.getListOfPurchasedUnits();
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                       return  sovietUnion.getListOfPurchasedUnits();
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        return greatBritain.getListOfPurchasedUnits();
                        break;
                    }
                default:
                    {
                        return unitedStates.getListOfPurchasedUnits();
                        break;
                    }
            }
        }

        public int getIPC()
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        return germany.getIPC();
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                        return sovietUnion.getIPC();
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        return greatBritain.getIPC();
                        break;
                    }
                default:
                    {
                        return unitedStates.getIPC();
                        break;
                    }
            }
        }

        public void addUnitToPurchaseList(Unit unit)
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        germany.addUnitToPurchaseList(unit);
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                        sovietUnion.addUnitToPurchaseList(unit);
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        greatBritain.addUnitToPurchaseList(unit); ;
                        break;
                    }
                default:
                    {
                        unitedStates.addUnitToPurchaseList(unit);
                        break;
                    }
            }
        }

        public void reduceIPC(int IPC)
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        germany.reduceIPC(IPC);
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                        sovietUnion.reduceIPC(IPC);
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        greatBritain.reduceIPC(IPC);
                        break;
                    }
                default:
                    {
                        unitedStates.reduceIPC(IPC);
                        break;
                    }
            }
        }

        public void removeUnitFromPurchaseList(Unit unit)
        {
            switch (currentPlayer)
            {
                case WhosTurn.Germany:
                    {
                        germany.removeUnitFromPurchaseList(unit);
                        break;
                    }
                case WhosTurn.SovietUnion:
                    {
                        sovietUnion.removeUnitFromPurchaseList(unit);
                        break;
                    }
                case WhosTurn.GreatBritain:
                    {
                        greatBritain.removeUnitFromPurchaseList(unit);
                        break;
                    }
                default:
                    {
                        unitedStates.removeUnitFromPurchaseList(unit);
                        break;
                    }
            }
        }
    }
}
