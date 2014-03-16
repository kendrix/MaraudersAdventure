
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class Adjacent : AccesAbstrait
    {
        public Adjacent(ZoneAbstraite z1, ZoneAbstraite z2)
            :base(z1, z2)
        { }

        

        public override bool AreNeighbour(Position z)
        {
            if (z == null)
                    return false;
                if ((this.z1.point.X == z.X && this.z1.point.Y == z.Y)
                    && (this.z2.point.X == z.X && this.z2.point.Y == z.Y))
                    return true;
                return false;
        }
    }
}
