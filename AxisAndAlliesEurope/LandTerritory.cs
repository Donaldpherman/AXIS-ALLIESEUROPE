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
    public class LandTerritory : Territory
    {

        public LandTerritory(ContentManager theContent, int x, int y, String nameOfTerritory, String whoControlsTerritory, int iPCValue)
            : base(theContent, x, y, nameOfTerritory, whoControlsTerritory, iPCValue)
        {
        }

        override public bool isSeaTerritory()
        {
            return false;
        }

        override public bool containsTransport()
        {
            return false;
        }

        override public bool addUnitToEmptyTransport(Unit landUnit)
        {
            return false;
        }
    }
}
