
#region ---------------- ComportementParler.cs ----------------------
/*
    Namespaces      WpfAventure.Metier.Comportements
    Classes         ComportementParler
 
    Date            2013 10 10
    Modif           2013 10 10
                    
    Auteur          Vincent LE CERF 
    Copyright       METAGENIA, 1999 - 2013
    URL             http://www.metagenia.net
    Email           codesource@metagenia.net
*/
#endregion --------------------------------------------------

namespace MaraudersAdventure
{
    class ComportementParler : ComportementEmettreUnSon
    {
        //-----------------------------------------------------------------------------
        public override string EmmettreSon()
        {
            return "Je Parle";
        }
    }
}
