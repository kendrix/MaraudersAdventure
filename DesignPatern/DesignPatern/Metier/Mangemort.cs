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
    public class Mangemort : Personnage
    {

        public Mangemort(EtatMajor em, string unNom, TypeEquipe e)
            : base(em, unNom, TypePersonnage.Fantassin, e)
        {
            if (e == TypeEquipe.Rouge)
                Image = Properties.Resources.pieds_marrons;
            else
                Image = Properties.Resources.pieds_noirs;

            ComportementCombat = new ComportementAvecSortilegesImpardonables(PointsDAttaque);
            seDeplacer = new SeDeplacerApiedAvecHache();
            ComportementEmettreUnSon = new ComportementCrier();


            PointsDeVie = 80;
            PointsDAttaque = 15;
            Vitesse = 1;
        }

        public override string Afficher()
        {
            return "Mangemort " + Nom;
        }


    }
}
