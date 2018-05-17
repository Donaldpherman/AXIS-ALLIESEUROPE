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
    public class Unit : IEquatable<Unit>
    {
        protected string type;
        protected int movement;
        protected int attackFactor;
        protected int defenseFactor;
        protected int cost;
        private string worldPower;
        protected int movementLeft;

        public Unit(String worldPower)
        {
            this.worldPower = worldPower;
        }

        public string getType()
        {
            return type;
        }

        public bool Equals(Unit unit)
        {
            if (string.Compare(unit.getWorldPower(), this.getWorldPower(), true) == 0)
                return (unit.type == this.type);
            else
                return false;
        }

        public bool canMove()
        {
            return (movementLeft > 0);
        }

        public void decreasementMovementLeft()
        {         
           movementLeft--;
        }

        public void resetMovementLeft()
        {
            movementLeft = movement;
        }
        /// <summary>
        /// return the String name of the world power controlling the unit object
        /// </summary>
        /// <returns>String worldPower</returns>
        public String getWorldPower()
        {
            return worldPower;
        }
        public bool hit(String whoControlsTerritory, ArrayList arrayListOfUnits)
        {
            // not a seaTerritory
            if (string.Compare(whoControlsTerritory, "SeaTerritory", true) != 0)
            {
                if (string.Compare(whoControlsTerritory, worldPower, true) == 0)
                {
                    return (CombatControl.rollDice() <= defenseFactor);
                }
                else
                {
                    return (CombatControl.rollDice() <= attackFactor);
                }
            }

            // immpliment Non Convoy SeaTerritory Battles Later on.

            // check if allied or axis is the defender, compaire movementLeft to movement

            foreach (Unit unit in arrayListOfUnits)
            {
                if (unit.movementLeft < unit.movement)
                {
                    whoControlsTerritory = unit.getWorldPower();
                    break;
                }
            }

            if (string.Compare(whoControlsTerritory, worldPower, true) == 0)
            {
                return (CombatControl.rollDice() <= defenseFactor);
            }
            else
            {
                return (CombatControl.rollDice() <= attackFactor);
            }
        }

        public int getCost()
        {
            return cost;
        }
    }
}
