using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public class GameSimulation
    {
        public ConfigurationGame game;
        public Personnage[] personnagesEnJeu;
        public Personnage joueurActuel;

        public GameSimulation(ConfigurationGame _game)
        {
            game = _game;
            InitGame();
        }

        public void InitGame() 
        {
            int totalJoueurs = game.EquipeRouge.Joueurs.Count + game.EquipeVerte.Joueurs.Count;
            personnagesEnJeu = new Personnage[totalJoueurs];
            int joueurMaxParEquipe = (game.EquipeVerte.Joueurs.Count < game.EquipeRouge.Joueurs.Count)
                ? game.EquipeRouge.Joueurs.Count : game.EquipeVerte.Joueurs.Count;
            int cpt = 0;

            for (int i = 0; i < joueurMaxParEquipe; i++)
            {
                if (game.EquipeVerte.Joueurs.Count-1 >= i)
                {
                    personnagesEnJeu[cpt] = game.EquipeVerte.Joueurs[i];
                    personnagesEnJeu[cpt].Position = GetStartZone(i);
					cpt++;
                }
                if (game.EquipeRouge.Joueurs.Count >= i + 1)
                {
                    personnagesEnJeu[cpt] = game.EquipeRouge.Joueurs[i];
                    personnagesEnJeu[cpt].Position = GetStartZone(i+25);
					cpt++;
                }
                
            }
            if (game.EquipeRouge.Quetes != null)
                foreach (Quete q in game.EquipeRouge.Quetes)
                    InitQuete(q);
            if (game.EquipeVerte.Quetes != null)
                foreach (Quete q in game.EquipeVerte.Quetes)
                    InitQuete(q);

            Random rmd = new Random(DateTime.Now.Millisecond);
            int nbObjets = rmd.Next(5, 15);
            for (int i = 0; i < nbObjets; i++)
            {
                game.Plateau.GetZone(GetStartZone(i)).objets.Add(
                    new Aliment(rmd.Next(1, 5), "Jus de citrouille"));
            }
        }

        public void InitQuete(Quete q)
        {
            if (q.Type == TypeQuete.TrouverCase)
            {
                QueteZone qz = new QueteZone(q.Libelle,GetStartZone(7));
                q = qz;
            }
            else if (q.Type == TypeQuete.TrouverObjetUnique)
            {
                ObjetQuete o = new ObjetQuete("");
                game.Plateau.GetZone(GetStartZone(2)).objets.Add(o);
                QueteObjet qo = new QueteObjet(q.Libelle, o, TypeQuete.TrouverObjetUnique);
                q = qo;
            }
        }

        public Position GetStartZone(int i)
        {
            Position p = null;
            bool found = false;
            while (!found)
            {
                int x, y;
                Random rdm = new Random(DateTime.Now.Millisecond + i);
                x = rdm.Next(0, Parametres.nbColonne);
                y = rdm.Next(0, Parametres.nbLigne);
                p = new Position(x, y);
                if (game.Plateau.GetZone(p).Walkable)
                    found = true;
            }
            return p;
        }

        public bool etatPartie = false;


        public  void tour(Personnage p)
        {
            p.SeDeplacer(game.Plateau);
            Combatre();
            if (p.etat != Etat.mort)
            {
                p.RamasserObjets(game.Plateau.GetZone(p.Position));
                UseObjets();
            }
            
        }


        public string PartieFinie()
        {
            /*bool res = false;
            if (personnagesEnJeu.First((c) => c.PointsDeVie != 0) == null)
            {
                return string.Format("PARTIE FINIE :tout le monde est mort");
            }
            else
            {
                bool resTrouverObjetChacun = true;
                bool resTrouverCase = true;
                TypeQuete tq = TypeQuete.TuerJoueur;

                foreach (Personnage p in personnagesEnJeu)
                {
                    //TrouverObjetUnique
                    if (p != null && p.Objectif.Type == TypeQuete.TrouverObjetUnique)
                    {
                        tq = p.Objectif.Type;
                        if (p.Objectif.Fini == true)
                            res = true;
                    }
                    //TrouverObjetChacun
                    else if (p != null && p.Objectif.Type == TypeQuete.TrouverObjetChacun)
                    {
                        tq = p.Objectif.Type;
                        if (p.Objectif.Fini != true)
                            return null;
                    }
                    //TrouverCase
                    else if (p != null && /*p.Objectif.Fini == false &&*//* p.Objectif.Type == TypeQuete.TrouverCase)
                    {
                        tq = p.Objectif.Type;
                        if (p.Objectif.Fini != true)
                            resTrouverCase = false;
                        //return null;
                    }
                }
                if (joueurActuel != null)
                {
                    string resultat = "";
                    if (resTrouverObjetChacun && tq == TypeQuete.TrouverObjetChacun)
                    {
                        resultat = string.Format("PARTIE FINIE : Tous les joueurs ont trouvé leur objet" + "\n");
                        foreach (Personnage j in personnagesEnJeu)
                            resultat = resultat + string.Format(j.Nom + " : " + j.GetHistorique() + "\n");
                    }
                    else if (resTrouverCase && tq == TypeQuete.TrouverCase)
                    {
                        resultat = string.Format("PARTIE FINIE : Tous les joueurs ont trouvé leur case" + "\n");
                        foreach (Personnage j in personnagesEnJeu)
                            resultat = resultat + string.Format(j.Nom + " : " + j.GetHistorique() + "\n");
                    }
                    else if (res && tq == TypeQuete.TrouverObjetUnique)
                    {
                        resultat = string.Format("PARTIE FINIE : le gagnant est {0}\n", joueurActuel.Nom);
                        foreach (Personnage p in personnagesEnJeu)
                        {
                            resultat = resultat + string.Format(p.Nom + " : " + p.GetHistorique() + "\n");
                        }
                    }
                    return resultat;
                }
                else
                    return null;
            }*/
            return "vv";
        }

        private void Combatre()
        {
            //TODO: faire la fonction de combat
        }

        private string UseObjets()
        {
            bool res = false;
            foreach (Objet o in joueurActuel.Objets)
            {
                Equipe e;
                if (joueurActuel.equipe == TypeEquipe.Rouge)
                    e = game.EquipeRouge;
                else
                    e = game.EquipeVerte;

                if (true == o.Utilisation(joueurActuel, e) && monTypeObjet.ObjetDeQuete == o.monType)
                {
                    res = true;
                }
            }
            joueurActuel.Objets.Clear();
            if (res)
            {
                string r = PartieFinie();
                if (!string.IsNullOrEmpty(r))
                    return r;
                else
                    return null;
            }
            else
                return null;
        }


    }
}
