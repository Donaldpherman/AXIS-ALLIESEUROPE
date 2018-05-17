using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    class AntiAircraftGun : LandUnit
    {
        public AntiAircraftGun(string worldPower)
            : base(worldPower)
        {
            type = "AntiAircraftGun";
            movement = 1;
            attackFactor = 0;
            defenseFactor = 1;
            cost = 5;
            movementLeft = movement;
        }
    }
}
