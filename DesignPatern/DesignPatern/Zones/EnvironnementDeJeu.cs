using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class EnvironnementDeJeu
    {
        public PlateauDeJeu CreerPlateauDeJeu(FabriquePlateauDeJeu fabrique)
        { 
            //Creer le plateau, le zone et les accès
            PlateauDeJeu plateau = fabrique.CreerPlateau();
            return plateau;
        }
    }
}
