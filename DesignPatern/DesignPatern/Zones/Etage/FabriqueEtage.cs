
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class FabriqueEtage : FabriquePlateauDeJeuAbstraite
    {
        public override AccesAbstrait CreerAcces(ZoneAbstraite z1, ZoneAbstraite z2)
        {
            return new AdjacentEtage((CaseEtage)z1, (CaseEtage)z2);
        }
        public override PlateauDeJeu CreerPlateau()
        {
            PlateauDeJeu plateau = new PlateauDeJeu(MapType.portoloin);
            //creer adjacent et case
            CaseEtage actuelle = null;
            CaseEtage ancienne = null;
            AdjacentEtage lien = null;
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
                actuelle = (CaseEtage)CreerZone("Zone " + i, p, i);
                plateau.AjoutZone((ZoneAbstraite)actuelle);
                if (ancienne != null)
                {
                    lien = (AdjacentEtage)CreerAcces((ZoneAbstraite)actuelle, (ZoneAbstraite)ancienne);
                    plateau.AjoutAcces((AccesAbstrait)lien);
                }
                ancienne = actuelle;
                h++;
            }

            List<ZoneAbstraite> liste = plateau.zones;
            ZoneAbstraite za;
            foreach (CaseEtage z in liste)
            {
                if (z.point.Y != 0)
                {
                    za = (ZoneAbstraite)plateau.GetZone(new Position(z.point.X, z.point.Y - 1));
                    if (za != null)
                    {
                        lien = (AdjacentEtage)CreerAcces((ZoneAbstraite)z, za);
                        plateau.AjoutAcces((AccesAbstrait)lien);
                    }
                    za = null;
                }
                if (z.point.Y != Parametres.nbColonne - 1)
                {
                    za = (ZoneAbstraite)plateau.GetZone(new Position(z.point.X, z.point.Y + 1));
                    if (za != null)
                    {
                        lien = (AdjacentEtage)CreerAcces((ZoneAbstraite)z, za);
                        plateau.AjoutAcces((AccesAbstrait)lien);
                    }
                    za = null;
                }
            }

            foreach (CaseEtage z in plateau.zones)
            {
                if (z.point.Y == 3 || z.point.Y == 7 || z.point.Y == 8)
                    z.Walkable = false;
            }
            return plateau;
        }

        public override ZoneAbstraite CreerZone(string nom, Position p, int i)
        {
            return new CaseEtage(nom, p, i);
        }

    }
}
