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
                //Faire attention aux zones interdites
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
            Combattre();
            if (p.etat != Etat.mort)
            {
                p.RamasserObjets(game.Plateau.GetZone(p.Position));
                UseObjets();
            }
            
        }


        public string PartieFinie()
        {
            CheckQuest(game.EquipeRouge);
            CheckQuest(game.EquipeVerte);

            if (game.EquipeRouge.Quetes.Count == 0 && game.EquipeVerte.Quetes.Count == 0)
            {
                return string.Format("DEBUG: PAS DE QUETE ENREGISTREES");
            }                 
            if (personnagesEnJeu.First((c) => c.PointsDeVie != 0) == null)
            {
                return string.Format("PARTIE FINIE :tout le monde est mort");
            }
            else if (game.EquipeRouge.Quetes.First((q) => q.Fini == false) == null)
            {
                return string.Format("PARTIE FINIE : l'équipe rouge à accomplie toutes ses quêtes");
            }
            else if (game.EquipeVerte.Quetes.First((q) => q.Fini == false) == null)
            {
                return string.Format("PARTIE FINIE : l'équipe verte à accomplie toutes ses quêtes");
            }
            else
                return null;
        }

        private void CheckQuest(Equipe e)
        {
            foreach (Quete q in e.Quetes)
            {
                if (q.Fini == true)
                    continue;

                if (q.Type == TypeQuete.TrouverCase)
                {
                    QueteZone qz = (QueteZone)q;
                    foreach (Personnage p in e.Joueurs)
                        if (p.Position.X == qz.ZoneATrouver.X && p.Position.Y == qz.ZoneATrouver.Y)
                            q.FinirQuete(p);
                }
                else if (q.Type == TypeQuete.TrouverObjetUnique)
                {
                    QueteObjet qo = (QueteObjet)q;
                    foreach (Personnage p in e.Joueurs)
                        if (p.Objets.Contains(qo.ObjetATrouver))
                            q.FinirQuete(p);
                }
                /* else if (q.Type == TypeQuete.TuerJoueur)
                 {
                     quete qo = (QueteObjet)q;
                     if (
                 }*/

            }
        }

        private void Combattre()
        {
            Equipe e;
            if (joueurActuel.equipe ==  TypeEquipe.Rouge)
                e = game.EquipeVerte;
            else 
                e = game.EquipeRouge;

            foreach (Personnage p in e.Joueurs)
            {
                if (p.Position.X == joueurActuel.Position.X && p.Position.Y == joueurActuel.Position.Y)
                    joueurActuel.Combattre(p);
            }
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
