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
    class ConvoyTerritory : SeaTerritory
    {

        public ConvoyTerritory(ContentManager theContent, int x, int y, String nameOfTerritory, String whoControlTerritory, int iPCValue)
            : base(theContent, x, y, nameOfTerritory, whoControlTerritory, iPCValue)
        {
        }
    }
}
