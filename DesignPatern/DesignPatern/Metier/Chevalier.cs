#region ---------------- Chevalier.cs ----------------------
/*
    Namespaces      WpfAventure.Metier
    Classes         Chevalier
 
    Date              day
    Modif             day
                    
 * 
    Auteur          Vincent LE CERF 
    Copyright       METAGENIA, 1999 - 2013
    URL             http://www.metagenia.net
    Email           codesource@metagenia.net
*/
#endregion --------------------------------------------------

namespace MaraudersAdventure
{
    class Chevalier : Personnage
    {
        public Chevalier(string unNom) : 
            base(unNom, TypePersonnage.Chevalier)
        {
            Image = Properties.Resources.chev;
            ComportementCombat = new ComportementAcheval();
            seDeplacer = new SeDeplacerAcheval();
            ComportementEmettreUnSon = new ComportementCrier();
        }

        public override string Afficher()
        {
            return "Chevalier " + Nom;
        }
    }
}
