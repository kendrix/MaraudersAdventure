#region ---------------- Archer.cs ----------------------
/*
    Namespaces      WpfAventure.Metier
    Classes         Archer
 
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
    class Archer : Personnage
    {
        public Archer(string unNom)
            : base(unNom, TypePersonnage.Archer)
        {
            Image = Properties.Resources.archer;
            ComportementCombat = new ComportementAvecArc();
            seDeplacer = new SeDeplacerApiedAvecHache();
            ComportementEmettreUnSon = new ComportementParler();
        }

        public override string Afficher()
        {
            return "Archer " + Nom;
        }

    }
}
