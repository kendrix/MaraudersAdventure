using MaraudersAdventure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
namespace MaraudersAdventure
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    /// 
    public static class ExtensionMethods
    {

        private static Action EmptyDelegate = delegate() { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }


    public partial class Map : Window
    {
        SimulationJeu maSimulation;


        int choixSimulation;

        public Map(SimulationJeu jeu, int choix)
        {
            maSimulation = jeu;
            InitializeComponent();
            UpdateMapLayout();
            choixSimulation = choix;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Afficher("*************** Lancement de la simulation ***************");
            Afficher(maSimulation.NouveauTour());
            Afficher(maSimulation.JoueurSuivant());

            this.Refresh();

            switch (choixSimulation)
            {
                case 1:
                    Simulation1();
                    break;
                case 2:
                    Simulation2();
                    break;

                case 3:
                    Simulation3();
                    break;
            }


            this.Refresh();
        }
        /*
        public void NouveauTour()
        {
            cpt++;

            Afficher(string.Format("*************** Nouveau Tour n°{0} ***************", cpt));

            this.Refresh();
        }

        public void JoueurSuivant()
        {
            if (joueurActuel == null)
                joueurActuel = maSimulation.PersonnageList[0];
            else
            {
                int index = maSimulation.PersonnageList.IndexOf(joueurActuel);
                if (index < maSimulation.PersonnageList.Count - 1)
                    joueurActuel = maSimulation.PersonnageList[index + 1];
                else
                {
                    NouveauTour();
                    joueurActuel = maSimulation.PersonnageList[0];
                }
            }

            Afficher(string.Format("{0} : ({1} points de vie)", joueurActuel.Nom, joueurActuel.PointsDeVie));
            if (joueurActuel.PointsDeVie == 0)
            {
                if (maSimulation.PersonnageList.Exists((c)=>c.PointsDeVie==0))
                    PartieFinie();
                else
                    JoueurSuivant();
            }


            UpdateMapLayout();
            this.Refresh();

            Thread.Sleep(500);
           
        }
        */
        public BitmapSource ToWpfBitmap(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
        public void UpdateMapLayout()
        {
            ChessBoard.Children.Clear();
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    ZoneAbstraite zone = maSimulation.plateau.GetZone(new Position(x, y));
                    List<ZoneAbstraite> meszones = maSimulation.plateau.GetNeighbourZones(zone.point);
                    MapZone arbre = new MapZone(zone);
                    MapZone herbe = new MapZone(zone);
                    MapZone herbepossible = new MapZone(zone);
                    MapZone inconnu = new MapZone(zone);
                    MapZone personnage = new MapZone(zone);
                    MapZone objet = new MapZone(zone);

                    arbre.Source = ToWpfBitmap(Properties.Resources.arbre);
                    herbe.Source = ToWpfBitmap(Properties.Resources.herbe);
                    herbepossible.Source = ToWpfBitmap(Properties.Resources.herbepossible);
                    inconnu.Source = ToWpfBitmap(Properties.Resources.inconnu);
                    objet.Source = ToWpfBitmap(Properties.Resources.objet);

                    if (zone != null)
                    {
                        bool personnagesurcase = false;
                        bool quetezone = false;
                        foreach (Personnage p in maSimulation.PersonnageList)
                            if (zone != null && p.Position.X == zone.point.X && p.Position.Y == zone.point.Y)
                            {
                                personnagesurcase = true;
                                personnage.Source = ToWpfBitmap(p.Image);
                            }
                            else if (p.Objectif.Type == TypeQuete.TrouverCase)
                            {
                                QueteZone z = (QueteZone)p.Objectif;
                                if (z != null 
                                    && zone != null
                                    && z.ZoneATrouver.X == zone.point.X 
                                    && z.ZoneATrouver.Y == zone.point.Y)
                                {
                                    quetezone = true;
                                }
                            }

                        if (personnagesurcase)
                            ChessBoard.Children.Add(personnage);
                        else if (quetezone)
                            ChessBoard.Children.Add(inconnu);
                        else if (zone.objets != null && zone.objets.Count > 0)
                            ChessBoard.Children.Add(objet);
                        else if (zone.Walkable)
                            ChessBoard.Children.Add(herbe);
                        else
                            ChessBoard.Children.Add(arbre);
                    }
                }
            }
            this.Refresh();
            UpdateLayout();
        }
        public void Afficher(string message)
        {
            Visuel.Items.Add(message);
            Visuel.ScrollIntoView(Visuel.Items[Visuel.Items.Count - 1]);
        }
        /*public bool PartieFinie()
        {
            bool res = false;
            if (!maSimulation.PersonnageList.Exists((c) => c.PointsDeVie != 0))
            {
                Afficher(string.Format("PARTIE FINIE :tout le monde est mort"));
                return true;
            }
            else
            {
                bool resTrouverObjetChacun = true;
                bool resTrouverCase = true;
                TypeQuete tq  = TypeQuete.TuerJoueur;

                foreach (Personnage p in maSimulation.PersonnageList)
                {
                    //TrouverObjetUnique
                    if (p != null && p.Objectif.Type == TypeQuete.TrouverObjetUnique)
                    {
                        tq = p.Objectif.Type;
                        if (p.Objectif.Fini == true)
                            res = true;
                    }
                    //TrouverObjetChacun
                    else if (p != null && p.Objectif.Type == TypeQuete.TrouverObjetChacun)
                    {
                        tq = p.Objectif.Type;
                        if (p.Objectif.Fini != true)
                            return false;
                    }
                    //TrouverCase
                    else if (p != null && p.Objectif.Fini == false && p.Objectif.Type == TypeQuete.TrouverCase)
                    {
                        tq = p.Objectif.Type;
                        if (p.Objectif.Fini != true)
                            resTrouverCase = false;
                        return false;
                    }
                }
                if (joueurActuel != null)
                {
                    if (resTrouverObjetChacun && tq == TypeQuete.TrouverObjetChacun)
                    {
                        Afficher(string.Format("PARTIE FINIE : Tous les joueurs ont trouvé leur objet"));
                        foreach(Personnage j in maSimulation.PersonnageList)
                            Afficher(string.Format(j.Nom + " : " + j.GetHistorique()));
                    }
                    else if (resTrouverCase && tq == TypeQuete.TrouverCase)
                    {
                        Afficher(string.Format("PARTIE FINIE : Tous les joueurs ont trouvé leur objet"));
                        foreach (Personnage j in maSimulation.PersonnageList)
                            Afficher(string.Format(j.Nom + " : " + j.GetHistorique()));
                    }
                    else if (res && tq == TypeQuete.TrouverObjetUnique)
                    {
                        Afficher(string.Format("PARTIE FINIE : le gagnant est {0}", joueurActuel.Nom));
                        foreach (Personnage p in maSimulation.PersonnageList)
                        {
                            Afficher(string.Format(p.Nom + " : " + p.GetHistorique()));
                        }
                    }
                }
                else
                    return false;
                return true;
            }
        }

        public bool UseObjets()
        {
            bool res = false;
            foreach (Objet o in joueurActuel.Objets)
                if (true == o.Utilisation(joueurActuel) && monTypeObjet.ObjetDeQuete == o.monType)
                {
                    res = true;
                }
            joueurActuel.Objets.Clear();
            if(res)
                return PartieFinie();
            else
                return false;
        }
        */
        /// <summary>
        /// SIMULATIONS
        /// </summary>
      
        public void Simulation1()
        {
            this.Refresh();

            for (int i = 0; i < 3; i++)
            {
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition1(i, maSimulation.joueurActuel)))
                    break;

                this.Refresh();
                Afficher(maSimulation.JoueurSuivant());
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition1(i, maSimulation.joueurActuel)))
                    break;

                Afficher(maSimulation.JoueurSuivant());
                this.Refresh();
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition1(i, maSimulation.joueurActuel)))
                    break;

                Afficher(maSimulation.JoueurSuivant());
                this.Refresh();
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition1(i, maSimulation.joueurActuel)))
                    break;

                Afficher(maSimulation.NouveauTour());
                UpdateMapLayout();
                this.Refresh();
            }
        }
        public Position GetPosition1(int i, Personnage p)
        {
            Position[] tab1 = { new Position(1, 1), new Position(1, 2), new Position(1, 3), new Position(1, 4), new Position(0, 4) };
            Position[] tab2 = { new Position(3, 1), new Position(3, 2), new Position(3, 3), new Position(2, 3), new Position(2, 4) };
            Position[] tab3 = { new Position(5, 1), new Position(4, 1), new Position(3, 1), new Position(2, 1), new Position(2, 2) };
            Position[] tab4 = { new Position(7, 1), new Position(6, 1), new Position(5, 1), new Position(5, 2), new Position(5, 3) };
            Position[][] tab = { tab1, tab2, tab3, tab4 };
            return GetPositionByPersonnage(i, p,tab);
        }
        public Position GetPosition2(int i, Personnage p)
        {
            //KAMI --CHEVALIER
            Position[] tab1 = { new Position(1, 1), new Position(1, 2), new Position(1, 3), new Position(1, 4), new Position(1, 5), 
                                  new Position(1, 6) };
            //JACQUES -- FANTASSIN
            Position[] tab2 = { new Position(4, 0), new Position(4, 1), new Position(4, 2), new Position(4, 3), new Position(4, 4), 
                                  new Position(4, 6) };
            //THIERRY -- ARCHER
            Position[] tab3 = { new Position(4, 0), new Position(3, 0), new Position(2, 0), new Position(2, 1), new Position(2, 2), 
                                  new Position(2, 3)};
            //YSIA -- PRINCESSE
            Position[] tab4 = { new Position(6, 0), new Position(5, 0), new Position(4, 0), new Position(3, 0), new Position(2, 0), 
                                  new Position(1, 0) };
            
            Position[][] tab = { tab1, tab2, tab3, tab4 };

            return GetPositionByPersonnage(i, p, tab);
        }
        public Position GetPositionByPersonnage(int i, Personnage p, Position[][] tab)
        {
            switch (p.type)
            {
                case TypePersonnage.Archer:
                    return tab[2][i];
                case TypePersonnage.Chevalier:
                    return tab[0][i];
                case TypePersonnage.Fantassin:
                    return tab[1][i];
                case TypePersonnage.Princesse:
                    return tab[3][i];
            }
            return new Position(0, 0);
        }
        public bool SimulatrionGenerique(Personnage p, Position po)
        {
            string res = maSimulation.joueurActuel.SeDeplacer(po);
            this.Refresh();
            UpdateMapLayout();
            Afficher("        " + res);
            bool fini = false;
            ZoneAbstraite z = maSimulation.plateau.GetZone(maSimulation.joueurActuel.Position);
            res = maSimulation.joueurActuel.RamasserObjets(z);
            Afficher("        " + res);
            res = maSimulation.UseObjets();
            if (res != null)
            {
                fini = true;
                Afficher(res);
            }

            this.Refresh();

            Thread.Sleep(500);
            return fini;
        }
        public void Simulation3()
        {
            this.Refresh();

            for (int i = 0; i < 5; i++)
            {
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition1(i, maSimulation.joueurActuel)))
                    break;

                this.Refresh();
                Afficher(maSimulation.JoueurSuivant());
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition1(i, maSimulation.joueurActuel)))
                    break;

                Afficher(maSimulation.JoueurSuivant());
                this.Refresh();
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition1(i, maSimulation.joueurActuel)))
                    break;

                Afficher(maSimulation.JoueurSuivant());
                this.Refresh();
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition1(i, maSimulation.joueurActuel)))
                    break;

                Afficher(maSimulation.JoueurSuivant());

                UpdateMapLayout();
                this.Refresh();
            }
        }
       
        public void Simulation2()
        {
            this.Refresh();

            for (int i = 0; i < 6; i++)
            {
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition2(i, maSimulation.joueurActuel)))
                    break;

                this.Refresh();
                Afficher(maSimulation.JoueurSuivant());
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition2(i, maSimulation.joueurActuel)))
                    break;

                Afficher(maSimulation.JoueurSuivant());
                this.Refresh();
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition2(i, maSimulation.joueurActuel)))
                    break;

                Afficher(maSimulation.JoueurSuivant());
                this.Refresh();
                if (SimulatrionGenerique(maSimulation.joueurActuel, GetPosition2(i, maSimulation.joueurActuel)))
                    break;

                UpdateMapLayout();
                this.Refresh();
                string res = (maSimulation.PartieFinie());
                if (!string.IsNullOrEmpty(res))
                    Afficher(res);
                else
                    Afficher(maSimulation.NouveauTour());
            }
        }
    }

}
