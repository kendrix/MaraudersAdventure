using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class Aliment : Objet
    {
        int plusvie = 0;

        public Aliment(int val, string nom) : 
            base(monTypeObjet.Aliment)
        {
            plusvie = val;
            Nom = nom;
        }
        public override bool Utilisation(Personnage p)
        {
            p.PointsDeVie = p.PointsDeVie + plusvie;
            return true;
        }
    }
}
