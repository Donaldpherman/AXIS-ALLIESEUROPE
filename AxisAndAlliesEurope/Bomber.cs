using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    public class Bomber : AirUnit
    {
        public Bomber(string worldPower) :
            base(worldPower)
        {
            type = "Bomber";
            movement = 6;
            attackFactor = 4;
            defenseFactor = 1;
            cost = 15;
            movementLeft = movement;
        }
    }
}
