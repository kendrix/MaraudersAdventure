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
    public class Moldu : Personnage
    {
        public Moldu(EtatMajor em, string unNom, TypeEquipe e) : 
            base(em, unNom, TypePersonnage.Princesse, e)
        {
            if (e == TypeEquipe.Rouge)
                Image = Properties.Resources.pieds_marrons;
            else
                Image = Properties.Resources.pieds_noirs;

            //ComportementCombat = new ComportementAMainsNues(PointsDAttaque);
            ComportementEmettreUnSon = new ComportementParlerPrincesse();
            seDeplacer = new SeDeplacerApiedAvecHache();

            PointsDeVie = 95;
            PointsDAttaque = 1;
            Vitesse = 1;
        }

    }
}
