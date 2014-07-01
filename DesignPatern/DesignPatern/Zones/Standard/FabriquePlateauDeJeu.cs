
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
            return plateau;
        }

        public override ZoneAbstraite CreerZone(string nom, Position p, int i)
        {
            return new Zone(nom, p, i);
        }
    }
}
