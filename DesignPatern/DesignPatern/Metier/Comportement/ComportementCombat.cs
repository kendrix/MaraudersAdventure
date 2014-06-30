#region ---------------- ComportementCombat.cs ----------------------
/*
    Namespaces      WpfAventure.Metier.Comportements
    Classes         ComportementCombat
 
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
    abstract public class ComportementCombat
    {
        public abstract string Combattre(Personnage p);
    }
}
