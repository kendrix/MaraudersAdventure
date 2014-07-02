#region ---------------- Personnage.cs ----------------------
/*
    Namespaces      WpfAventure.Metier
    Classes         Personnage
 
    Date              day
    Modif             day
                    
 * 
    Auteur          Vincent LE CERF 
    Copyright       METAGENIA, 1999 - 2013
    URL             http://www.metagenia.net
    Email           codesource@metagenia.net
*/
#endregion --------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;

namespace MaraudersAdventure
{
    public enum Etat
    {
        mort,
        vivant
    }
    public enum TypePersonnage
    {
        Princesse,
        Chevalier,
        Archer,
        Mage,
        Fantassin
    }
    public enum TypeEquipe
    {
        Rouge,
        Verte
    };

    public abstract class Personnage  : ObservateurAbstrait
    {
        public string Nom { get; set; }
        public ComportementCombat ComportementCombat { get; set; }
        public ComportementEmettreUnSon ComportementEmettreUnSon { get; set; }
        public SeDeplacerClass seDeplacer { get; set; }

        public TypePersonnage type { get; set; }
        public int PointsDeVie { get; set; }
        public int PointsDAttaque { get; set; }
        public int Vitesse { get; set; }
        public TypeEquipe equipe;
        private Position position;
        protected EtatMajor em;

        public Position Position
        {
            get { return position; }
            set 
            {
                position = value;
                Historique.Add(position);/*
                if( Objectif != null)
                    Objectif.FinirQuete(this);*/
            }
        } 
      
        public Etat etat { get; set; }
        //public Quete Objectif { get; set; }
        public List<Objet> Objets = new List<Objet>();
        public Bitmap Image;

        public List<Position> Historique;

        //-----------------------------------------------------------------------------
        protected Personnage(EtatMajor em, string sonNom, TypePersonnage tp, TypeEquipe eq)
        {
            Nom = sonNom;
            ComportementCombat = null;
            ComportementEmettreUnSon = null;
            type = tp;
            Historique = new List<Position>();
            Image = Properties.Resources.archer;
            equipe = eq;
            etat = Etat.vivant;
            this.em = em;
        }

        //-----------------------------------------------------------------------------
        public virtual string Afficher()
        {
            return Nom;
        }

        //-----------------------------------------------------------------------------
        public string SeDeplacer(PlateauDeJeu plateau)
        {
            if (seDeplacer != null)
            {
                //Position p = new Position(1,1);
                List<ZoneAbstraite> voisins = plateau.GetNeighbourZones(this.position);
                Random rdm = new Random(DateTime.Now.Millisecond + this.PointsDeVie + this.position.X);
                bool found = false;
                int i = 0;

                if (voisins.Find((c) => c.Walkable == true) == null)
                    return "Je suis trop fatigué pour me déplacer";
                    
                while (!found)
                {
                    i = rdm.Next(0, voisins.Count - 1);
                    if (voisins[i].Walkable)
                        found = true;
                }
                //TODO: mettre une fonction avec dijkstra (Valérie)
                return seDeplacer.SeDeplacer(this, voisins[i].point);
            }
            return "Je suis trop fatigué pour me déplacer";
        }

        //-----------------------------------------------------------------------------
        public string EmettreUnSon()
        {
            if (ComportementEmettreUnSon != null)
                return ComportementEmettreUnSon.EmmettreSon();
            return "Je suis muet";
        }

        //-----------------------------------------------------------------------------
        public string Combattre(Personnage p)
        {
            string res = "";

            if (ComportementCombat != null)
                res += ComportementCombat.Combattre(p);

            if (p.ComportementCombat != null)
                res = res + "/n" + p.ComportementCombat.Combattre(this);

            return res; 
        }

        //-----------------------------------------------------------------------------
        public string RamasserObjets(ZoneAbstraite z, List<Quete> quetes)
        {
            string res = "Il n'y a rien ici.";
            if (z != null && z.objets != null && z.objets.Count != 0)
            {
                int cpt = 0;
                foreach (Objet o in z.objets)
                {
                    if (o.monType == monTypeObjet.ObjetDeQuete)
                    {
                       ObjetQuete obj = (ObjetQuete)o;
                       foreach(Quete q in quetes)
                       {
                           if (q.Type == TypeQuete.TrouverObjetUnique && q.Fini == false)
                           {
                                QueteObjet quete = (QueteObjet)q;
                                if (quete.ObjetATrouver == obj)
                                {
                                    Objets.Add(o);

                                    if (cpt == 0)
                                        res = string.Format("{0} à ramassé : ", Nom) + " " + o.Nom;
                                    else
                                        res = res + ", " + o.Nom;

                                    z.objets = null;
                                }
                            }                          
                       }
                    }
                    else
                    {
                        Objets.Add(o);

                        if (cpt == 0)
                            res = string.Format("{0} à ramassé : ", Nom) + " " + o.Nom;
                        else
                            res = res + ", " + o.Nom;

                        z.objets = null;
                    }
                    cpt++;
               }

            }

            return res;

        }

        public string GetHistorique()
        {
            string result = "";
            if (Historique != null && Historique.Count != 0)
                foreach (Position p in Historique)
                {
                    if(p == Historique[0])
                        result = string.Format("Parcours : {1} - {2}", result, p.X, p.Y);
                    else
                        result = string.Format("{0}  //   {1} - {2}", result, p.X, p.Y);
                }
            else
                result = "N'as pas bougé";

            return result;
        }


        public override void Update(string s)
        {
            string EtatObservé = em.ModeFonctionnement().ToString();
            if (EtatObservé == "Suicide")
            {
                etat = Etat.mort;
                PointsDeVie = 0;
            }
            if (EtatObservé == "Ressuscitez")
            {
                etat = Etat.vivant;
                PointsDeVie = 10;
            }

        }


    }
}
