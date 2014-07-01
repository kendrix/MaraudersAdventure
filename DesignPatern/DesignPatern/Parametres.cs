using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public static class Parametres
    {
        static public int nbLigne = 12;//16;
        static public int nbColonne = 12;//9;
        static public int nbCases = nbColonne * nbLigne;
        static public readonly EtatMajor em = new EtatMajor();
    }
}
