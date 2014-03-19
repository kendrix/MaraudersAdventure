#region ---------------- Princesse.cs ----------------------
/*
    Namespaces      WpfAventure.Metier
    Classes         Princesse
 
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
    public class Princesse : Personnage
    {
        public Princesse(string unNom) : 
            base(unNom, TypePersonnage.Princesse)
        {
            Image = Properties.Resources.princesse; 
            ComportementEmettreUnSon = new ComportementParlerPrincesse();
            seDeplacer = new SeDeplacerApiedAvecHache();
            Notify(string.Format("La princesse {0} à été créée", unNom));

            PointsDeVie = 95;
            PointsDAttaque = 5;
            Vitesse = 1;
        }
    }
}
