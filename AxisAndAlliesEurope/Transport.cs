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
using System.Collections;

namespace AxisAndAlliesEurope
{
    public class Transport : NavalUnits
    {
        private ArrayList arrayListOfLoadedUnits = new ArrayList();

        public Transport(string worldPower) :
            base(worldPower)
        {
            type = "Transport";
            movement = 1;
            attackFactor = 0;
            defenseFactor = 2;
            cost = 8;
            movementLeft = movement;
        }

        public bool loadLandUnit(Unit landUnit)
        {
            if ((arrayListOfLoadedUnits.Count < 2) && (!arrayListOfLoadedUnits.Contains(new Armor(base.getWorldPower()))))
            {
                arrayListOfLoadedUnits.Add(landUnit);
                return true;
            }

            return false;
        }

        public Unit unLoadLandUnit(Unit unit)
        {
            for (int i = 0; i < arrayListOfLoadedUnits.Count; ++i)
            {
                if (unit.getType() == ((Unit)(arrayListOfLoadedUnits[i])).getType())
                    return removeLoadedLandUnit(unit, i);
            }

            return null;
        }

        private Unit removeLoadedLandUnit(Unit unit, int x)
        {
            arrayListOfLoadedUnits.RemoveAt(x);
            return unit;
        }

        public ArrayList getArrayListOfLoadedUnits()
        {
            return arrayListOfLoadedUnits;
        }
    }
}
