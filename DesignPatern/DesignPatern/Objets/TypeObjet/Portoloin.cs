using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure.Objets.TypeObjet
{
    class Portoloin : Objet
    {

        public Etage monEtage;
        public Etage destination;

        public Portoloin(Etage _monetage, Etage _destination) : base(monTypeObjet.Portoloin)
        {
            monEtage = _monetage;
            destination = _destination;

            this.Nom = "Portoloin";

            GenerateNewPosition();
                //this.point = new Position(rdm.Next(6, 12), rdm.Next(1, 12));
        }

        public override bool Utilisation(Personnage p, Equipe e)
        {            
            //Comment changer le personnage d'étage ? 
            Random rdm = new Random(DateTime.Now.Millisecond);

            if (destination == Etage.etage1)
                p.Position = new Position(rdm.Next(0, 11), rdm.Next(0, 2));
            else if (destination == Etage.etage2)
                p.Position = new Position(rdm.Next(0, 11), rdm.Next(4, 6));
            else
                p.Position = new Position(rdm.Next(0, 11), rdm.Next(9, 11));

            return false;
        }

        public void GenerateNewPosition()
        {
            Random rdm = new Random(DateTime.Now.Millisecond);

            if (monEtage == Etage.etage1)
                this.point = new Position(rdm.Next(0, 11), rdm.Next(0, 2));
            else if (monEtage == Etage.etage2)
                this.point = new Position(rdm.Next(0, 11), rdm.Next(4, 6));
            else
                this.point = new Position(rdm.Next(0, 11), rdm.Next(9, 11));
            
        }
        
        static public void  GeneratePortoloins(ConfigurationGame game)
        {
            Position old;
            //ETAGE 1
            Portoloin p = new Portoloin(Etage.etage1, Etage.etage2);
            old = p.point;
            game.Plateau.GetZone(p.point).objets.Add(p);

            p = new Portoloin(Etage.etage1, Etage.etage3);
            while (p.point.X == old.X && p.point.Y == old.Y)
            {
                p.GenerateNewPosition();
            }
            game.Plateau.GetZone(p.point).objets.Add(p);
            //ETAGE 2
            p = new Portoloin(Etage.etage2, Etage.etage1);
            old = p.point;
            game.Plateau.GetZone(p.point).objets.Add(p);

            p = new Portoloin(Etage.etage2, Etage.etage3);
            while (p.point.X == old.X && p.point.Y == old.Y)
            {
                p.GenerateNewPosition();
            }
            game.Plateau.GetZone(p.point).objets.Add(p);
            //ETAGE 3
            p = new Portoloin(Etage.etage3, Etage.etage1);
            old = p.point;
            game.Plateau.GetZone(p.point).objets.Add(p);

            p = new Portoloin(Etage.etage3, Etage.etage2);
            while (p.point.X == old.X && p.point.Y == old.Y)
            {
                p.GenerateNewPosition();
            }
            game.Plateau.GetZone(p.point).objets.Add(p);
        }
    }
}
