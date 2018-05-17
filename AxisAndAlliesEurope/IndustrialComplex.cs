using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisAndAlliesEurope
{
    class IndustrialComplex : Unit
    {

        public IndustrialComplex(String worldPower):
            base (worldPower)
        {
            type = "IndustrialComplex";
        }
    }
}
