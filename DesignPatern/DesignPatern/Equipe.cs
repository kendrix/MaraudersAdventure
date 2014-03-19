using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class Equipe
    {
        List<Quete> quetes;
        public List<Quete> Quetes
        {
            get { return quetes; }
            set { quetes = value; }
        }

        List<Personnage> joueurs;
        public List<Personnage> Joueurs
        {
            get { return joueurs; }
            set { joueurs = value; }
        }

        string nom;
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public Equipe (string _nom, List<Personnage> _listenom, List<string> _quetes)
        {
            _nom = nom;
            Personnage p;

            foreach (string que in _quetes)
            {
                //if (que.Equals(""))
                //   Quete q = new QueteObjet();
            }
            /*
            foreach (string joueur in _listenom)
            {
                p = new Chevalier(joueur);
                joueurs.Add(p);
            }*/
            joueurs = _listenom;
        }
    }
}
