#region ---------------- Fantassin.cs ----------------------
/*
    Namespaces      KStoreLibrary.Metier
    Classes         Fantassin
 
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
    class Fantassin : Personnage
    {

        public Fantassin(string unNom)
            : base(unNom, TypePersonnage.Fantassin)
        {
            Image = Properties.Resources.fantassin;
            ComportementCombat = new ComportementApiedAvecHache();
            seDeplacer = new SeDeplacerApiedAvecHache();
            ComportementEmettreUnSon = new ComportementCrier();


            PointsDeVie = 110;
            PointsDAttaque = 7;
            Vitesse = 1;
        }

        public override string Afficher()
        {
            return "Fantassin " + Nom;
        }

    }
}
