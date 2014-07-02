
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class Acces : AccesAbstrait
    {
        private bool canBeUse = true;

        public bool CanBeUse
        {
            get
            {
                if (z1.Walkable && z2.Walkable)
                    return true;
                else
                    return false;
            }
            set { canBeUse = value; }
        }
        public Acces() { }

        public Acces(Zone z1, Zone z2)
        {
            this.z1 = z1;
            this.z2 = z2;
        }

        public override bool AreNeighbour(Position z)
        {
            if (z == null)
                return false;

           /* if ((this.z1.point.X == z.X && this.z1.point.Y == z.Y)
                && (this.z2.point.X == z.X && this.z2.point.Y == z.Y) && CanBeUse)
                return true;
            */
            if (((z.X == z1.point.X) && (z.Y == z1.point.Y)|| (z.X == z2.point.X) &&(z.Y == z2.point.Y)) && canBeUse)
                return true;
            return false;
        }
    }
}
