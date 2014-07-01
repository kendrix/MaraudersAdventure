
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
            return plateau;
        }

        public override ZoneAbstraite CreerZone(string nom, Position p, int i)
        {
            return new CaseEtage(nom, p, i);
        }

    }
}
