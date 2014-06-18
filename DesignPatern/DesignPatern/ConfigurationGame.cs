using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public class ConfigurationGame
    {
        Equipe equipeRouge;
        internal Equipe EquipeRouge
        {
            get { return equipeRouge; }
            set { equipeRouge = value; }
        }

        Equipe equipeVerte;
        internal Equipe EquipeVerte
        {
            get { return equipeVerte; }
            set { equipeVerte = value; }
        }

        PlateauDeJeu plateau;
        public PlateauDeJeu Plateau
        {
            get { return plateau; }
            set { plateau = value; }
        }


        public int cpt = 0;
        public Personnage joueurActuel;


        public ConfigurationGame(string nomR, string nomV, List<Personnage> joueursR, List<Personnage> joueursV, 
            List<string> queteR, List<string> queteV, MapType map)
        {
            equipeRouge = new Equipe(nomR, joueursR, queteR);
            equipeVerte = new Equipe(nomV, joueursV, queteV);

            if (map == MapType.standard)
            {
                FabriquePlateauDeJeu f = new FabriquePlateauDeJeu();
                plateau = f.CreerPlateau();
            }
            else if ( map == MapType.maraudeurs)
            {
               // plateau = ;

                FabriqueLabyrinthe f = new FabriqueLabyrinthe();
                plateau = f.CreerPlateau();
            }
            else
            {
                //plateau = ;
                FabriqueEtage f = new FabriqueEtage();
                plateau = f.CreerPlateau();
            }
        }
    }
}
