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
    public class Gobelin : Personnage
    {
        public Gobelin(EtatMajor em, string unNom, TypeEquipe e)
            : base(em, unNom, TypePersonnage.Archer, e)
        {
            if (e == TypeEquipe.Rouge)
                Image = Properties.Resources.pieds_marrons;
            else
                Image = Properties.Resources.pieds_noirs;
            ComportementCombat = new ComportementAMainsNues(PointsDAttaque);
            seDeplacer = new SeDeplacerApiedAvecHache();
            ComportementEmettreUnSon = new ComportementParler();
            
            PointsDeVie = 101;
            PointsDAttaque = 8;
            Vitesse = 2;
        }

        public override string Afficher()
        {
            return "Gobelin " + Nom;
        }

    }
}
