#region ---------------- ComportementAvecArc.cs ----------------------
/*
    Namespaces      WpfAventure.Metier.Comportements
    Classes         ComportementAvecArc
 
    Date            2013 10 10
    Modif           2013 10 10
                    
    Auteur          Vincent LE CERF 
    Copyright       METAGENIA, 1999 - 2013
    URL             http://www.metagenia.net
    Email           codesource@metagenia.net
*/
#endregion ------------------------------------------------

namespace MaraudersAdventure
{
    class ComportementAvecArc : ComportementCombat
    {
        int dégat = 4;
        //-----------------------------------------------------------------------------
        public override string Combattre(Personnage p)
        {
            p.PointsDeVie = p.PointsDeVie - dégat;
        
            return "A pied et un arc";
        }
    }
}
