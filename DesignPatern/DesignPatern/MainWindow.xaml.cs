using System;
using System.Windows;

namespace MaraudersAdventure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Map m = new Map(Simulation1(), 1);
            m.ShowDialog();
        }

        private SimulationJeu Simulation1()
        {
            SimulationJeu maSimulation = new SimulationJeu();
            FabriquePlateauDeJeu f = new FabriquePlateauDeJeu();
            maSimulation.plateau = f.CreerPlateau();

            Objet o = new ObjetQuete("Graal");

            Chevalier Kami = new Chevalier("Kami le terrible");
            Kami.Objectif = new QueteObjet("La recherche du Graal", o, TypeQuete.TrouverObjetUnique);

            Fantassin Jacques = new Fantassin("Jacques a dit");
            Jacques.Objectif = new QueteObjet("La recherche du Graal", o, TypeQuete.TrouverObjetUnique);

            Archer Thierry = new Archer("Thierry la fronde");
            Thierry.Objectif = new QueteObjet("La recherche du Graal", o, TypeQuete.TrouverObjetUnique);

            Princesse Ysia = new Princesse("Ysia");
            Ysia.Objectif = new QueteObjet("La recherche du Graal", o, TypeQuete.TrouverObjetUnique);

            Random rdm = new Random(DateTime.Now.Millisecond);
            Kami.PointsDeVie = rdm.Next(1,30);
            Jacques.PointsDeVie = rdm.Next(1, 30);
            Thierry.PointsDeVie = rdm.Next(1, 30);
            Ysia.PointsDeVie = rdm.Next(1, 30);

            Kami.Position = new Position(1, 0);
            Jacques.Position = new Position(3, 0);
            Thierry.Position = new Position(5, 0);
            Ysia.Position = new Position(7, 0);

            maSimulation.PersonnageList.Add(Kami);
            maSimulation.PersonnageList.Add(Jacques);
            maSimulation.PersonnageList.Add(Thierry);
            maSimulation.PersonnageList.Add(Ysia);


            maSimulation.plateau.zones[15].objets.Add(o);
            maSimulation.plateau.zones[42].objets.Add(new Aliment(3, "Cuisse de poulet"));
            maSimulation.plateau.zones[4].objets.Add(new Aliment(2, "Pomme"));
            maSimulation.plateau.zones[25].objets.Add(new Aliment(3, "Miche de pain"));


            return maSimulation;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Map m = new Map(Simulation2(), 2);
            m.ShowDialog();
        }

        private SimulationJeu Simulation2()
        {
            SimulationJeu maSimulation = new SimulationJeu();
            FabriquePlateauDeJeuAbstraite f = new FabriqueLabyrinthe();
            maSimulation.plateau = f.CreerPlateau();

            Chevalier Kami = new Chevalier("Kami le terrible");
            Kami.Objectif = new QueteZone("Aller au zoo", new Position(1,6));

            Fantassin Jacques = new Fantassin("Jacques a dit");
            Jacques.Objectif = new QueteZone("Aller au cinéma", new Position(4, 6));

            Archer Thierry = new Archer("Thierry la fronde");
            Thierry.Objectif = new QueteZone("Aller au pub", new Position(2, 3));

            Princesse Ysia = new Princesse("Ysia");
            Ysia.Objectif = new QueteZone("Aller à la bibliothèque", new Position(1, 0));

            Kami.Position = new Position(1, 0);
            Jacques.Position = new Position(3, 0);
            Thierry.Position = new Position(5, 0);
            Ysia.Position = new Position(7, 0);

            Random rdm = new Random(DateTime.Now.Millisecond);
            Kami.PointsDeVie = rdm.Next(10, 30);
            Jacques.PointsDeVie = rdm.Next(10, 30);
            Thierry.PointsDeVie = rdm.Next(10, 30);
            Ysia.PointsDeVie = rdm.Next(10, 30);

            maSimulation.PersonnageList.Add(Kami);
            maSimulation.PersonnageList.Add(Jacques);
            maSimulation.PersonnageList.Add(Thierry);
            maSimulation.PersonnageList.Add(Ysia);


            maSimulation.plateau.zones[43].objets.Add(new Aliment(3, "Tiramisu"));
            maSimulation.plateau.zones[10].objets.Add(new Aliment(2, "Patate"));
            maSimulation.plateau.zones[55].objets.Add(new Aliment(3, "Lapin"));
            maSimulation.plateau.zones[51].objets.Add(new Aliment(2, "Bierre"));
            maSimulation.plateau.zones[22].objets.Add(new Aliment(3, "Eau"));


            return maSimulation;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Map m = new Map(Simulation3(), 3);
            m.ShowDialog();
        }


        private SimulationJeu Simulation3()
        {
            SimulationJeu maSimulation = new SimulationJeu();
            FabriquePlateauDeJeu f = new FabriquePlateauDeJeu();
            maSimulation.plateau = f.CreerPlateau();

            Objet or = new ObjetQuete("Graal rose");
            Objet ov = new ObjetQuete("Graal vert");
            Objet oj = new ObjetQuete("Graal jaune");
            Objet ob = new ObjetQuete("Graal bleu");

            Chevalier Kami = new Chevalier("Kami le terrible");
            Kami.Objectif = new QueteObjet("La recherche du Graal", or, TypeQuete.TrouverObjetChacun);

            Fantassin Jacques = new Fantassin("Jacques a dit");
            Jacques.Objectif = new QueteObjet("La recherche du Graal", ov, TypeQuete.TrouverObjetChacun);

            Archer Thierry = new Archer("Thierry la fronde");
            Thierry.Objectif = new QueteObjet("La recherche du Graal", oj, TypeQuete.TrouverObjetChacun);

            Princesse Ysia = new Princesse("Ysia");
            Ysia.Objectif = new QueteObjet("La recherche du Graal", ob, TypeQuete.TrouverObjetChacun);

            Random rdm = new Random(DateTime.Now.Millisecond);
            Kami.PointsDeVie = rdm.Next(1, 30);
            Jacques.PointsDeVie = rdm.Next(1, 30);
            Thierry.PointsDeVie = rdm.Next(1, 30);
            Ysia.PointsDeVie = rdm.Next(1, 30);

            Kami.Position = new Position(1, 0);
            Jacques.Position = new Position(3, 0);
            Thierry.Position = new Position(5, 0);
            Ysia.Position = new Position(7, 0);

            maSimulation.PersonnageList.Add(Kami);
            maSimulation.PersonnageList.Add(Jacques);
            maSimulation.PersonnageList.Add(Thierry);
            maSimulation.PersonnageList.Add(Ysia);

            maSimulation.plateau.zones[22].Walkable = false;
            maSimulation.plateau.zones[24].Walkable = false;
            maSimulation.plateau.zones[23].Walkable = false;
            maSimulation.plateau.zones[44].Walkable = false;
            maSimulation.plateau.zones[46].Walkable = false;
            maSimulation.plateau.zones[45].Walkable = false;

            maSimulation.plateau.zones[18].objets.Add(oj);
            maSimulation.plateau.zones[33].objets.Add(or);//bon
            maSimulation.plateau.zones[29].objets.Add(ob); //bon
            maSimulation.plateau.zones[34].objets.Add(ov);
            maSimulation.plateau.zones[32].objets.Add(new Aliment(3, "Cuisse de poulet"));
            maSimulation.plateau.zones[4].objets.Add(new Aliment(2, "Pomme"));
            maSimulation.plateau.zones[25].objets.Add(new Aliment(3, "Miche de pain"));


            return maSimulation;

        }

    }
}
