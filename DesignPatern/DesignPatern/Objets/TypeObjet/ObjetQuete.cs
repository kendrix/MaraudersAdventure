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

        public override bool Utilisation(Personnage p, Equipe e)
        {
            foreach (Quete qq in e.Quetes)
            {
                if (qq.Type == TypeQuete.TrouverObjetChacun || qq.Type == TypeQuete.TrouverObjetUnique)
                {
                    QueteObjet q = (QueteObjet)qq;
                    return q.FinirQuete(p);
                }
            }
            return false;
        }
    }
}
