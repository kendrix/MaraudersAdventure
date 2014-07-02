using MaraudersAdventure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Marauder_s_Adventure_Test
{
    [TestClass]
    public class Test
    {
        /*EtatMajor em = new MaraudersAdventure.EtatMajor();
        List<Personnage> personnagesR = new List<Personnage>();
        List<Personnage> personnagesV = new List<Personnage>();

        List<string> quetesR = new List<string>();
        List<string> quetesV = new List<string>();

        Personnage combattantR = new Chevalier( "Harry Potter", TypeEquipe.Rouge);
        Personnage combattantV = new Chevalier("Voldermort", TypeEquipe.Verte);

        MapType map = new MapType();

        private void setSetGameChevaliers()
        {
            //Tous les personnages sont des chevaliers
            personnagesR.Add(combattantR);
            personnagesR.Add(new Chevalier( "Ginny W.", TypeEquipe.Rouge));
            personnagesR.Add(new Chevalier ("Test", TypeEquipe.Rouge));

            personnagesV.Add(combattantV);
            personnagesV.Add(new Chevalier( "Pierre", TypeEquipe.Verte));
            personnagesV.Add(new Chevalier( "Méchant", TypeEquipe.Verte));

            quetesR.Add("Objet");
            quetesV.Add("Objet");

        }


        [TestMethod]
        public void TestCombattre()
        {
            setSetGameChevaliers();
            ConfigurationGame game = new ConfigurationGame("Equipe rouge", "Equipe verte",
                    personnagesR, personnagesV, quetesR, quetesV, MapType.standard);
            //Setter des positions aux personnages
            Position combattantPosition = new Position(1, 1);

            combattantR.Position = combattantPosition;
            combattantV.Position = combattantPosition;

            combattantR.Combattre(combattantV);

            Assert.AreEqual(97, combattantV.PointsDeVie);


            
        }
            */
    }
}
