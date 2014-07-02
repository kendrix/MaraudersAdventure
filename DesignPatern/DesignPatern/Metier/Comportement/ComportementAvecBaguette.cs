#region ---------------- ComportementApiedAvecHache.cs ----------------------
/*
    Namespaces      WpfAventure.Metier.Comportements
    Classes         ComportementApiedAvecHache
 
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
    class ComportementAvecBaguette : ComportementCombat
    {
        int dégat = 5;
        public ComportementAvecBaguette(int degat)
        {
            dégat += degat;
        }
        //-----------------------------------------------------------------------------
        public override string Combattre(Personnage p)
        {
            p.PointsDeVie = p.PointsDeVie - dégat;
            return p.Nom + " perds " + dégat + " point de vie.";
        } 
    }
}
