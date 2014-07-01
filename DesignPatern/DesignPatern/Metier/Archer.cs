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
        public Archer(EtatMajor em, string unNom, TypeEquipe e)
            : base(em, unNom, TypePersonnage.Archer, e)
        {
            Image = Properties.Resources.archer;
            ComportementCombat = new ComportementAvecArc();
            seDeplacer = new SeDeplacerApiedAvecHache();
            ComportementEmettreUnSon = new ComportementParler();
            
            PointsDeVie = 101;
            PointsDAttaque = 8;
            Vitesse = 2;
        }

        public override string Afficher()
        {
            return "Archer " + Nom;
        }

    }
}
