using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    public class Infantry : LandUnit
    {
        public Infantry(String worldPower)
            : base(worldPower)
        {
            type = "Infantry";
            movement = 1;
            attackFactor = 1;
            defenseFactor = 2;
            cost = 3;
            movementLeft = movement;
        }
    }
}
