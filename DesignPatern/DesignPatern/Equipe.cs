using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public class Equipe
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
            nom = _nom;
            quetes = new List<Quete>();
            foreach (string que in _quetes)
            {
                if (que.Equals("Objet"))
                   quetes.Add(new QueteObjet("dd", null, TypeQuete.TrouverObjetUnique));
                else if (que.Equals("TrouverCase"))
                   quetes.Add(new QueteZone("dd", null));
            }
            joueurs = _listenom;
        }

        /// <summary>
        /// Retourne vrais si une quete est encore en cours
        /// </summary>
        /// <returns></returns>
        public bool quetesEnCours()
        {
            foreach (Quete q in quetes)
                if (!q.Fini)
                    return true;
            return false;
        }
    }
}
