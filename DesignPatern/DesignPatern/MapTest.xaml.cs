using MaraudersAdventure.Zones.Etage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaraudersAdventure
{
    /// <summary>
    /// Interaction logic for MapTest.xaml
    /// </summary>
    public partial class MapTest : Window
    {
         ConfigurationGame conf;
        MapDesign design;

        public MapTest(ConfigurationGame _conf)
        {
            InitializeComponent();
            conf = _conf;
            switch (conf.Plateau.mytype)
            {
                case MapType.etage:
                    design = new MapDesign(Properties.Resources.inconnu, Properties.Resources.herbe, Properties.Resources.arbre, Properties.Resources.objet);
                    break;
                case MapType.maraudeurs:
                    design = new MapDesign(Properties.Resources.inconnu, Properties.Resources.herbe, Properties.Resources.arbre, Properties.Resources.objet);
                    break;
                case MapType.standard:
                    design = new MapDesign(Properties.Resources.inconnu, Properties.Resources.herbe, Properties.Resources.arbre, Properties.Resources.objet);
                    break;
            }
            UpdateMapLayout();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void UpdateMapLayout()
        {
            ChessBoard.Children.Clear();
            for (int x = 0; x < Parametres.nbColonne; ++x)
            {
                for (int y = 0; y < Parametres.nbLigne; ++y)
                {                    
                    ZoneAbstraite zone = conf.Plateau.GetZone(new Position(x, y));
                    if (zone != null)
                    {
                        List<ZoneAbstraite> meszones = conf.Plateau.GetNeighbourZones(zone.point);
                        ChessBoard.Children.Add(design.GetCaseImage(zone, conf.EquipeRouge.Joueurs, conf.EquipeVerte.Joueurs));
                    }
                }
            }
            this.Refresh();
            UpdateLayout();
        }

        List<Expander> listePerso = new List<Expander>();

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
    }
}
