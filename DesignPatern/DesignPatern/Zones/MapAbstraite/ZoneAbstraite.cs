
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public abstract class ZoneAbstraite
    {
        public int id;
        public  string nom;
        public Position point;
        public List<Objet> objets = new List<Objet>();
        public bool Walkable;

        public ZoneAbstraite() { }
        public ZoneAbstraite(string nom, Position p, int i)
        {
           this.nom = nom;
           this.point = p;
           this.id = i; 
           this.Walkable = true;
        }
    }
}
