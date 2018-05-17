using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    class Destroyer : NavalUnits
    {
        public Destroyer(String worldPower) :
            base(worldPower)
        {
            type = "Destroyer";
            movement = 2;
            attackFactor = 3;
            defenseFactor = 3;
            cost = 12;
            movementLeft = movement;
        }
    }
}
