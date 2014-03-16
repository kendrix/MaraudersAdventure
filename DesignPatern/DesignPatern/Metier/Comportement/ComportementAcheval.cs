#region ---------------- ComportementAcheval.cs ----------------------
/*
    Namespaces      WpfAventure.Metier.Comportements
    Classes         ComportementAcheval
 
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
    class ComportementAcheval : ComportementCombat
    {
        //-----------------------------------------------------------------------------
        public override string Combattre()
        {
            return "A cheval et une épée";
        }
    }
}
