using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAventure;

namespace MaraudersAdventure
{
    public enum TypeQuete
    {
        TrouverObjetChacun,
        TrouverObjetUnique,
        TrouverCase,
        TuerJoueur,
    }
    public abstract  class Quete
    {

        protected bool fini = false;
        public bool Fini
        {
            get { return fini; }
        }

        public string Libelle {get; set;}
        public TypeQuete Type  {get; set;}
        public abstract  bool FinirQuete(Personnage p);
    }

    public class QueteObjet : Quete
    {
        Objet objetATrouver = null;
        public Objet ObjetATrouver
        {
            get { return objetATrouver; }
        }

        public QueteObjet(string nom, Objet o, TypeQuete monType)
        {
            Libelle = nom;
            objetATrouver = o;
            this.Type = monType;
        }

        public override bool FinirQuete(Personnage p)
        {
            if (p.Objets.Contains(objetATrouver))
            {
                fini = true;
                return true;
            }
            
            return false;
        }
    }

    public class QueteZone : Quete
    {
        Position zoneATrouver = null;

        public Position ZoneATrouver
        {
          get { return zoneATrouver; }
        }

        public QueteZone(string nom, Position p)
        {
            Libelle = nom;
            zoneATrouver = p;
            Type = TypeQuete.TrouverCase;
        }

        public override  bool FinirQuete(Personnage p)
        {
            if (p.Position.X == zoneATrouver.X  && p.Position.Y == zoneATrouver.Y)
            {
                fini = true;
                return true;
            }
            else
                return false;
        }
    }/*

    public class QueteTuerPersonnage : Quete
    {
        Personnage personnageATuer = null;

        public Personnage PersonnageATuer
        {
          get { return personnageATuer; }
        }

        public QueteTuerPersonnage(string nom, Personnage o)
        {
            Libelle = nom;
            personnageATuer = o;
        }

        private bool FinirQuete(Personnage p)
        {
            if (p.Victimes.contains(personnageATuer))
            {
                fini = true;
                return true;
            }
            else
                return false;
        }
    }*/
}
