
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    abstract class FabriquePlateauDeJeuAbstraite
    {
        public abstract AccesAbstrait CreerAcces(ZoneAbstraite z1, ZoneAbstraite z2);
     
        public virtual PlateauDeJeu CreerPlateau()
        {
            return new PlateauDeJeu();
        }
        public abstract ZoneAbstraite CreerZone(string nom, Position p, int i);
    }
}
