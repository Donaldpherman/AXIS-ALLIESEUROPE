using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    public class Submarine : NavalUnits
    {
        public bool isSubmerged { get; set; }

        public Submarine(String worldPower) :
        base (worldPower)
        {
            type = "Submarine";
            movement = 2;
            attackFactor = 2;
            defenseFactor = 2;
            cost = 8;
            movementLeft = movement;
        }
    }
}
