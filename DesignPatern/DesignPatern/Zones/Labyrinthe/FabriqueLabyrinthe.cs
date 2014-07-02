
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class FabriqueLabyrinthe : FabriquePlateauDeJeuAbstraite
    {
        public override AccesAbstrait CreerAcces(ZoneAbstraite z1, ZoneAbstraite z2)
        {
            return new Adjacent((Case)z1, (Case)z2);
        }
        public override PlateauDeJeu CreerPlateau()
        {
            PlateauDeJeu plateau = new PlateauDeJeu(MapType.labyrinthe);
            //creer adjacent et case
            Case actuelle = null;
            Case ancienne = null;
            Adjacent lien = null;
            Position p = null;
            int h = 0;
            int j = 0;
            for (int i = 0; i < Parametres.nbCases; i++)
            {
                if (h >= Parametres.nbColonne)
                {
                    h = 0;
                    j++;
                }
                p = new Position(h, j);
                actuelle = (Case)CreerZone("Zone " + i, p, i);
                plateau.AjoutZone((ZoneAbstraite)actuelle);
                if (ancienne != null)
                {
                    lien = (Adjacent)CreerAcces((ZoneAbstraite)actuelle, (ZoneAbstraite)ancienne);
                    plateau.AjoutAcces((AccesAbstrait)lien);
                }
                ancienne = actuelle;
                h++;
            }

            List<ZoneAbstraite> liste = plateau.zones;
            ZoneAbstraite za;
            foreach (Case z in liste)
            {
                if (z.point.Y != 0)
                {
                    za = (ZoneAbstraite)plateau.GetZone(new Position(z.point.X, z.point.Y - 1));
                    if (za != null)
                    {
                        lien = (Adjacent)CreerAcces((ZoneAbstraite)z, za);
                        plateau.AjoutAcces((AccesAbstrait)lien);
                    }
                    za = null;
                }
                if (z.point.Y != Parametres.nbColonne - 1)
                {
                    za = (ZoneAbstraite)plateau.GetZone(new Position(z.point.X, z.point.Y + 1));
                    if (za != null)
                    {
                        lien = (Adjacent)CreerAcces((ZoneAbstraite)z, za);
                        plateau.AjoutAcces((AccesAbstrait)lien);
                    }
                    za = null;
                }
            }
            Random rdm = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10; i++)
            {
                plateau.zones[rdm.Next(0, Parametres.nbCases)].Walkable = false;
            }
            return plateau;
        }

        public override ZoneAbstraite CreerZone(string nom, Position p, int i)
        {
            return new Case(nom, p, i);
        }
    }
}
