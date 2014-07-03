using MaraudersAdventure.Objets.TypeObjet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MaraudersAdventure
{
    public class GameSimulation
    {
        public MaraudersAdventure.MapFinale.WriteMessage handler;

        public ConfigurationGame game;
        public Personnage[] personnagesEnJeu;
        public Personnage joueurActuel;

        public GameSimulation(ConfigurationGame _game, MaraudersAdventure.MapFinale.WriteMessage _handler)
        {         
            game = _game;
            handler = _handler;
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

            Quete[] qq = game.EquipeRouge.Quetes.ToArray();
            int j = 0;
            if (game.EquipeRouge.Quetes != null)
            {
                foreach (Quete q in qq)
                {
                    game.EquipeRouge.Quetes[j] = InitQuete(q);
                    j++;
                }
            }

            qq = game.EquipeVerte.Quetes.ToArray();
            j = 0;
            if (game.EquipeVerte.Quetes != null)
                foreach (Quete q in qq)
                {
                    game.EquipeVerte.Quetes[j] = InitQuete(q);
                    j++;
                }

            /*Random rmd = new Random(DateTime.Now.Millisecond);
            int nbObjets = rmd.Next(5, 15);
            for (int i = 0; i < nbObjets; i++)
            {
                //Faire attention aux zones interdites
                game.Plateau.GetZone(GetStartZone(i)).objets.Add(
                    new Aliment(rmd.Next(1, 5), "Jus de citrouille"));
            }*/
            
            if (game.Plateau.mytype == MapType.portoloin)
            {
                Portoloin.GeneratePortoloins(game);
            }
        }

        public Quete InitQuete(Quete q)
        {
            if (q.Type == TypeQuete.TrouverCase)
            {
                QueteZone qz = new QueteZone(q.Libelle,GetStartZone(7));
                q = qz;
            }
            else if (q.Type == TypeQuete.TrouverObjetUnique)
            {
                ObjetQuete o = new ObjetQuete("Objet de quete");
                game.Plateau.GetZone(GetStartZone(DateTime.Now.Millisecond)).objets.Add(o);
                QueteObjet qo = new QueteObjet(q.Libelle, o, TypeQuete.TrouverObjetUnique);
                q = qo;
            }
            return q;
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
            try
            {
                if (p.etat == Etat.mort || p.PointsDeVie <= 0)
                    return;

                Application.Current.Dispatcher.BeginInvoke(handler, p.Afficher() + ":");
                Application.Current.Dispatcher.BeginInvoke(handler, "\t" + p.SeDeplacer(game.Plateau));
                
                string r = Combattre();
                if (!string.IsNullOrEmpty(r))
                    Application.Current.Dispatcher.BeginInvoke(handler, "\t" + r);

                if (p.etat != Etat.mort)
                {
                    if( p.equipe == TypeEquipe.Rouge)
                        Application.Current.Dispatcher.BeginInvoke(handler, "\t" + p.RamasserObjets(game.Plateau.GetZone(p.Position), game.EquipeRouge.Quetes));
                    else
                        Application.Current.Dispatcher.BeginInvoke(handler, "\t" + p.RamasserObjets(game.Plateau.GetZone(p.Position), game.EquipeVerte.Quetes));
                    r = "";
                    r = UseObjets();

                    if (!string.IsNullOrEmpty(r))
                        Application.Current.Dispatcher.BeginInvoke(handler, "\t" + r);

                    Application.Current.Dispatcher.BeginInvoke(handler, " ----------------------");
                }
            }
            catch (Exception) { }
        }

        public string PartieFinie()
        {
            //TODO tester la fonction
            CheckQuest(game.EquipeRouge);
            CheckQuest(game.EquipeVerte);

            if (game.EquipeRouge.Quetes.Count == 0 && game.EquipeVerte.Quetes.Count == 0)
            {
                return string.Format("DEBUG: PAS DE QUETE ENREGISTREES");
            }
            if (personnagesEnJeu.FirstOrDefault((c) => c.PointsDeVie > 0) == default(Personnage))
            {
                return string.Format("PARTIE FINIE :tout le monde est mort");
            }
            else if (!game.EquipeRouge.quetesEnCours())
            {
                return string.Format("PARTIE FINIE : l'équipe rouge à accomplie toutes ses quêtes");
            }
            else if (!game.EquipeVerte.quetesEnCours())
            {
                return string.Format("PARTIE FINIE : l'équipe verte à accomplie toutes ses quêtes");
            }
            else
                return null;
        }

        private void CheckQuest(Equipe e)
        {
            try
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
            catch (Exception) { }
            
        }

        private string Combattre()
        {
            string res = "Combat : ";
            Equipe e;
            if (joueurActuel.equipe ==  TypeEquipe.Rouge)
                e = game.EquipeVerte;
            else 
                e = game.EquipeRouge;
            try
            {
                foreach (Personnage p in e.Joueurs)
                {
                    if (p.Position.X == joueurActuel.Position.X && p.Position.Y == joueurActuel.Position.Y)
                    {
                        return joueurActuel.Combattre(p);
                        //res = joueurActuel.Nom
                    }
                }
            }
            catch (Exception) { return ""; }

            return "";
        }

        private string UseObjets()
        {
            bool res = false;
            Etage e1 = Etage.none, e2 = Etage.none;
            foreach (Objet o in joueurActuel.Objets)
            {
                Equipe e;
                if (joueurActuel.equipe == TypeEquipe.Rouge)
                    e = game.EquipeRouge;
                else
                    e = game.EquipeVerte;

                if (o.monType == monTypeObjet.Portoloin)
                {
                    Portoloin p = (Portoloin)o;
                    e1 = p.monEtage;
                    e2 = p.destination;
                }

                if (true == o.Utilisation(joueurActuel, e) && monTypeObjet.ObjetDeQuete == o.monType)
                {
                    res = true;
                }
            }
            joueurActuel.Objets.Clear();
            if (e1 != Etage.none && e2 != Etage.none)
            {
                Portoloin p = new Portoloin(e1, e2);
                game.Plateau.GetZone(p.point).objets.Add(p);
            }

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
