using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    class AirCraftCarrier : NavalUnits
    {
        public AirCraftCarrier(string worldPower) :
            base (worldPower)
        {
            type = "AirCraftCarrier";
            movement = 2;
            attackFactor = 1;
            defenseFactor = 3;
            cost = 18;
            movementLeft = movement;
        }
    }
}
