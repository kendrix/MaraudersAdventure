#region ---------------- ComportementAcheval.cs ----------------------
/*
    Namespaces      WpfAventure.Metier.Comportements
    Classes         ComportementAcheval
 
    Date            2013 10 10
    Modif           2013 10 10
                    
    Auteur          Vincent LE CERF 
    Copyright       METAGENIA, 1999 - 2013
    URL             http://www.metagenia.net
    Email           codesource@metagenia.net
*/
#endregion ------------------------------------------------

namespace MaraudersAdventure
{
    class SeDeplacerAcheval : SeDeplacerClass
    {
        public SeDeplacerAcheval()
        {
            NombreDePas = 2;
        }

        public override string SeDeplacer(Personnage p, Position z)
        {
            p.Position = z;
            p.PointsDeVie = p.PointsDeVie - 1;

            return string.Format("Je suis en zone {0}-{1}", p.Position.X, p.Position.Y);
        }
    }
}
