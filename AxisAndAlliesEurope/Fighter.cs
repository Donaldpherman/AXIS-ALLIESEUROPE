using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    public class Fighter : AirUnit
    {
        public Fighter(String worldPower) :
            base(worldPower)
        {
            type = "Fighter";
            movement = 4;
            attackFactor = 3;
            defenseFactor = 4;
            cost = 12;
            movementLeft = movement;
        }

    }
}
