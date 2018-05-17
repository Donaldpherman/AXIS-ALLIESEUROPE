using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    class Artillery : LandUnit
    {
        public Artillery(String worldPower)
            : base(worldPower)
        {
            type = "Artillery";
            movement = 1;
            attackFactor = 2;
            defenseFactor = 2;
            cost = 4;
            movementLeft = movement;
        }
    }
}
