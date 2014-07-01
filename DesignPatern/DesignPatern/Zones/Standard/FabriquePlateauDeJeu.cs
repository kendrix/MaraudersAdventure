
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class FabriquePlateauDeJeu : FabriquePlateauDeJeuAbstraite
    {
        public override AccesAbstrait CreerAcces(ZoneAbstraite z1, ZoneAbstraite z2)
        {
            return new Acces((Zone)z1, (Zone)z2);
        }
        public override PlateauDeJeu CreerPlateau()
        {
            PlateauDeJeu plateau = new PlateauDeJeu(MapType.labyrinthe);
            //creer adjacent et case
            Zone actuelle = null;
            Zone ancienne = null;
            Acces lien = null;
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
                actuelle = (Zone)CreerZone("Zone " + i, p, i);
                plateau.AjoutZone((ZoneAbstraite)actuelle);
                if (ancienne != null)
                {
                    lien = (Acces)CreerAcces((ZoneAbstraite)actuelle, (ZoneAbstraite)ancienne);
                    plateau.AjoutAcces((AccesAbstrait)lien);
                }
                ancienne = actuelle;
                h++;
            }

            List<ZoneAbstraite> liste = plateau.zones;
            ZoneAbstraite za;
            foreach (Zone z in liste)
            {
                if (z.point.Y != 0)
                {
                    za = (ZoneAbstraite)plateau.GetZone(new Position(z.point.X, z.point.Y - 1));
                    if (za != null)
                    {
                        lien = (Acces)CreerAcces((ZoneAbstraite)z, za);
                        plateau.AjoutAcces((AccesAbstrait)lien);
                    }
                    za = null;
                }
                if (z.point.Y != Parametres.nbColonne)
                {
                    za =(ZoneAbstraite)plateau.GetZone(new Position(z.point.X, z.point.Y + 1));
                    if (za != null)
                    {
                        lien = (Acces)CreerAcces((ZoneAbstraite)z, za);
                        plateau.AjoutAcces((AccesAbstrait)lien);
                    }
                    za = null;
                }
            }

            return plateau;
        }

        public override ZoneAbstraite CreerZone(string nom, Position p, int i)
        {
            return new Zone(nom, p, i);
        }
    }
}
