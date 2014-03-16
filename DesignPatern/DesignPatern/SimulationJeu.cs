#region ---------------- SimulationJeu.cs ----------------------
/*
    Namespaces      WpfAventure.Metier
    Classes         SimulationJeu
 
    Date              day
    Modif             day
                    
 * 
    Auteur          Vincent LE CERF 
    Copyright       METAGENIA, 1999 - 2013
    URL             http://www.metagenia.net
    Email           codesource@metagenia.net
*/
#endregion --------------------------------------------------
using System.Collections.Generic;

namespace MaraudersAdventure
{
    public class SimulationJeu
    {
        public readonly List<Personnage> PersonnageList;
        public PlateauDeJeu plateau;
        public int cpt = 0;
        public Personnage joueurActuel;

        public SimulationJeu()
        {
            PersonnageList = new List<Personnage>();
        }

        public string PartieFinie()
        {
            bool res = false;
            if (!PersonnageList.Exists((c) => c.PointsDeVie != 0))
            {
                return string.Format("PARTIE FINIE :tout le monde est mort");
            }
            else
            {
                bool resTrouverObjetChacun = true;
                bool resTrouverCase = true;
                TypeQuete tq = TypeQuete.TuerJoueur;

                foreach (Personnage p in PersonnageList)
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
                    else if (p != null && /*p.Objectif.Fini == false &&*/ p.Objectif.Type == TypeQuete.TrouverCase)
                    {
                        tq = p.Objectif.Type;
                        if (p.Objectif.Fini != true)
                            resTrouverCase = false;
                        //return null;
                    }
                }
                if (joueurActuel != null)
                {
                    string resultat="";
                    if (resTrouverObjetChacun && tq == TypeQuete.TrouverObjetChacun)
                    {
                        resultat = string.Format("PARTIE FINIE : Tous les joueurs ont trouvé leur objet" + "\n");
                        foreach (Personnage j in PersonnageList)
                            resultat = resultat + string.Format(j.Nom + " : " + j.GetHistorique()+"\n");
                    }
                    else if (resTrouverCase && tq == TypeQuete.TrouverCase)
                    {
                        resultat = string.Format("PARTIE FINIE : Tous les joueurs ont trouvé leur case" + "\n");
                        foreach (Personnage j in PersonnageList)
                            resultat = resultat + string.Format(j.Nom + " : " + j.GetHistorique() + "\n");
                    }
                    else if (res && tq == TypeQuete.TrouverObjetUnique)
                    {
                        resultat = string.Format("PARTIE FINIE : le gagnant est {0}\n", joueurActuel.Nom);
                        foreach (Personnage p in PersonnageList)
                        {
                            resultat = resultat + string.Format(p.Nom + " : " + p.GetHistorique() + "\n");
                        }
                    }
                    return resultat;
                }
                else
                    return null;
            }
        }

        public string UseObjets()
        {
            bool res = false;
            foreach (Objet o in joueurActuel.Objets)
                if (true == o.Utilisation(joueurActuel) && monTypeObjet.ObjetDeQuete == o.monType)
                {
                    res = true;
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


        public string JoueurSuivant()
        {
            string res = PartieFinie();
            if (!string.IsNullOrEmpty(res))
                return res;

            if (joueurActuel == null)
                joueurActuel = PersonnageList[0];
            else
            {
                int index = PersonnageList.IndexOf(joueurActuel);
                if (index < PersonnageList.Count - 1)
                    joueurActuel = PersonnageList[index + 1];
                else
                {
                    NouveauTour();
                    joueurActuel = PersonnageList[0];
                }
            }

            if (joueurActuel.PointsDeVie == 0)
            {
                if (PersonnageList.Exists((c) => c.PointsDeVie == 0))
                    PartieFinie();
                else
                    JoueurSuivant();
            }

            return string.Format("{0} : ({1} points de vie)", joueurActuel.Nom, joueurActuel.PointsDeVie);
        }

        public string NouveauTour()
        {
            cpt++;
            return string.Format("*************** Nouveau Tour n°{0} ***************", cpt);
        }
    }
}
