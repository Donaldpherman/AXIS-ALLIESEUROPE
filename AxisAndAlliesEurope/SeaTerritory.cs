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
    public class SeaTerritory : Territory
    {

        public SeaTerritory(ContentManager theContent, int x, int y, String nameOfTerritory)
            : base(theContent,x , y, nameOfTerritory, "SeaTerritory", 0)
        {
        }

        protected SeaTerritory(ContentManager theContent, int x, int y, String nameOfTerritory, String whoControlsTerritory, int iPCValue)
            : base(theContent, x, y, nameOfTerritory, whoControlsTerritory, iPCValue)
        {
        }

        public override bool isSeaTerritory()
        {
            return true;
        }

        public override bool containsTransport()
        {
            foreach(Unit unit in base.getArrayListOfUnits())
            {
                if (unit.getType() == "Transport")
                    return true;
            }
            return false;
        }

        public override bool addUnitToEmptyTransport(Unit landUnit)
        {
            for (int i = 0; i < base.getArrayListOfUnits().Count; ++i)
            {
                if (((Unit)arrayListOfUnits[i]).getType() == "Transport")
                {
                    if ((((Transport)arrayListOfUnits[i]).loadLandUnit(landUnit)))
                        return true;
                }
            }

            return false;
        }
    }
}
