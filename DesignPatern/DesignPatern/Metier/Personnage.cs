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
    public abstract class Personnage  : SujetAbstrait
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
        protected Personnage(string sonNom, TypePersonnage tp, TypeEquipe eq)
        {
            Nom = sonNom;
            ComportementCombat = null;
            ComportementEmettreUnSon = null;
            type = tp;
            Historique = new List<Position>();
            Image = Properties.Resources.archer;
            equipe = eq;
        }

        //-----------------------------------------------------------------------------
        public virtual string Afficher()
        {
            return Nom;
        }

        //-----------------------------------------------------------------------------
        public string SeDeplacer()
        {
            if (seDeplacer != null)
            {
                Position p = new Position(1,1);
                //TODO: mettre une fonction avec dijkstra (Valérie)
                return seDeplacer.SeDeplacer(this, p);
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
        public string Combattre()
        {
            if (ComportementCombat != null)
                return ComportementCombat.Combattre();
            return "Je ne combat pas"; 
        }

        //-----------------------------------------------------------------------------
        public string RamasserObjets(ZoneAbstraite z)
        {
            string res = "Il n'y a rien ici.";
            if (z != null && z.objets != null && z.objets.Count != 0)
            {
                int cpt = 0;
                foreach (Objet o in z.objets)
                {
                    /*if (o.monType == monTypeObjet.ObjetDeQuete)
                    {
                        ObjetQuete obj = (ObjetQuete)o;
                        QueteObjet quete = (QueteObjet)Objectif;
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
                    else
                    {*/
                        Objets.Add(o);

                        if (cpt == 0)
                            res = string.Format("{0} à ramassé : ", Nom) + " " + o.Nom;
                        else
                            res = res + ", " + o.Nom;

                        z.objets = null;
                    }
                    cpt++;
                //}

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


    }
}
