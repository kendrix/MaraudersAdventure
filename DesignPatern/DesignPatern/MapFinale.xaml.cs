using MaraudersAdventure.Zones.Etage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MaraudersAdventure
{
    public static class ExtensionMethods
    {

        private static Action EmptyDelegate = delegate() { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
    /// <summary>
    /// Interaction logic for MapTest.xaml
    /// </summary>
    public partial class MapFinale : Window
    {
        ConfigurationGame conf;
        MapDesign design;
        GameSimulation maSimulation;
        DateTime time;

        public MapFinale(ConfigurationGame _conf)
        {
            InitializeComponent();
            time = DateTime.Now;
            conf = _conf;
            switch (conf.Plateau.mytype)
            {
                case MapType.portoloin:
                    design = new MapDesign(Properties.Resources.inconnu, Properties.Resources.herbe, Properties.Resources.arbre, Properties.Resources.objet);
                    break;
                case MapType.labyrinthe:
                    design = new MapDesign(Properties.Resources.inconnu, Properties.Resources.herbe, Properties.Resources.arbre, Properties.Resources.objet);
                    break;
                case MapType.standard:
                    design = new MapDesign(Properties.Resources.inconnu, Properties.Resources.herbe, Properties.Resources.arbre, Properties.Resources.objet);
                    break;
            }

            maSimulation = new GameSimulation(conf);

            UpdateMapLayout();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Personnage p in conf.EquipeRouge.Joueurs)
            {
                // Add new expander, stack panel and text block.
                var newExpander = new Expander { Name = p.Afficher(), Header = p.Afficher() };
                var newstackPanel = new StackPanel { Name = p.Afficher() };
                var newtextBlock = new TextBlock { Text = "777" };

                // Add above items as children.     
                newstackPanel.Children.Add(newtextBlock);
                newExpander.Content = newstackPanel;
                myStack.Children.Add(newExpander);


                //listePerso.Add(new Expander());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
		
		private void Voir_Hitorique(object sender, RoutedEventArgs e)
        {
            Historique historique = new Historique(conf,time,maSimulation);
            historique.ShowDialog();
        }

        public void UpdateMapLayout()
        {
            List<Personnage> mm = conf.EquipeRouge.Joueurs;
            mm.AddRange(conf.EquipeVerte.Joueurs);
            ChessBoard.Children.Clear();
            for (int x = 0; x < Parametres.nbColonne; ++x)
            {
                for (int y = 0; y < Parametres.nbLigne; ++y)
                {
                    ZoneAbstraite zone = conf.Plateau.GetZone(new Position(x, y));
                    if (zone != null)
                    {
                        //List<ZoneAbstraite> meszones = conf.Plateau.GetNeighbourZones(zone.point);
                        ChessBoard.Children.Add(design.GetCaseImage(zone, mm));
                    }
                }
            }
            this.Refresh();
            UpdateLayout();
        }

        List<Expander> listePerso = new List<Expander>();

        public void StartGame()
        {
            int cptTours = 0;

            while (!maSimulation.etatPartie)
            {
                foreach (Personnage personnageEnCours in maSimulation.personnagesEnJeu)
                {

                    if (personnageEnCours.etat == Etat.mort)
                        continue;

                    maSimulation.joueurActuel = personnageEnCours;

                    maSimulation.tour(maSimulation.joueurActuel);

                    if (string.IsNullOrEmpty(maSimulation.PartieFinie()))
                    {
                        maSimulation.etatPartie = true;
                        break;
                    }
                    Thread.Sleep(2000);
                    UpdateMapLayout();
                }
                cptTours++;
            }

            UpdateMapLayout();
        }
    }
}
