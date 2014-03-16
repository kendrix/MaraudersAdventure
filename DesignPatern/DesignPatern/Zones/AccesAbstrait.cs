
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public abstract class AccesAbstrait    
    {
        public ZoneAbstraite z1;
        public ZoneAbstraite z2;
        public string nom;
        public AccesAbstrait() { }
        public AccesAbstrait(ZoneAbstraite z1, ZoneAbstraite z2)
        {
            this.z1 = z1;
            this.z2 = z2;
        }

        public abstract bool AreNeighbour(Position z);
    }
}
