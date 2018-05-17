using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    class Armor : LandUnit
    {
        public Armor(string worldPower)
            : base(worldPower)
        {
            type = "Armor";
            movement = 2;
            attackFactor = 3;
            defenseFactor = 2;
            cost = 3;
            movementLeft = movement;
        }
    }
}
