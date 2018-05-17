using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    class Battleship : NavalUnits
    {
        public bool isDamaged { get; set; }

        public Battleship(string worldPower) :
            base(worldPower)
        {
            type = "Battleship";
            movement = 2;
            attackFactor = 4;
            defenseFactor = 4;
            cost = 24;
            movementLeft = movement;
        }
    }
}
