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
        public Chevalier(EtatMajor em, string unNom, TypeEquipe e) : 
            base(em, unNom, TypePersonnage.Chevalier, e)
        {
            if (e == TypeEquipe.Rouge)
                Image = Properties.Resources.pieds_marrons;
            else
                Image = Properties.Resources.pieds_noirs;

            ComportementCombat = new ComportementAcheval();
            seDeplacer = new SeDeplacerAcheval();
            ComportementEmettreUnSon = new ComportementCrier();

            PointsDeVie = 105;
            PointsDAttaque = 10;
            Vitesse = 1;
        }

        public override string Afficher()
        {
            return "Chevalier " + Nom;
        }


    }
}
