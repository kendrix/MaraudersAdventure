using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class ObjetQuete : Objet
    {
        public ObjetQuete(string nom): 
            base(monTypeObjet.ObjetDeQuete)
        {
            this.Nom = nom;
        }

        public override bool Utilisation(Personnage p)
        {
            if (p.Objectif.Type == TypeQuete.TrouverObjetChacun || p.Objectif.Type == TypeQuete.TrouverObjetUnique)
            {
                QueteObjet q = (QueteObjet)p.Objectif;
                return q.FinirQuete(p);
            }
            return false;
        }
    }
}
