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

namespace MaraudersAdventure.Historique_Interfaces
{
    /// <summary>
    /// Interaction logic for Historiques.xaml
    /// </summary>
    public partial class Historiques : Window
    {
        List<GameSimulation> simulations;

        public Historiques(List<GameSimulation> _simulations)
        {
            InitializeComponent();
            this.simulations = _simulations;
            UpdateHistoriques();
        }

        public void UpdateHistoriques()
        {
            /***
             * Valeurs pour tester la vue
             ***/
            MaraudersAdventure.MapFinale.WriteMessage _handler = null;

            Personnage p1 = new Sorcier(Parametres.em,"Test", TypeEquipe.Rouge);
            List<Personnage> listRouge = new List<Personnage>();
            listRouge.Add(p1);
            simulations.Add(new GameSimulation(
                new ConfigurationGame("rouge", "verte", listRouge, new List<Personnage>(), 
                    new List<string>(), new List<string>(), MapType.standard), _handler));
            simulations.Add(new GameSimulation(
                new ConfigurationGame("rouge2", "verte2", new List<Personnage>(), 
                    new List<Personnage>(), new List<string>(), new List<string>(), MapType.standard), _handler));
            simulations.Add(new GameSimulation(
                new ConfigurationGame("rouge3", "verte3", new List<Personnage>(), 
                    new List<Personnage>(), new List<string>(), new List<string>(), MapType.standard), _handler));
            /***
             * Fin valeurs pour tester la vue
             ***/


            foreach (GameSimulation gm in this.simulations)
            {
                Button bh = new Button();
                bh.Content = "En savoir plus";

                bh.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                bh.Click += bh_Click;
                bh.Tag = this.simulations.IndexOf(gm);

                parties.Items.Add(new TemplateHistorique(gm.game.EquipeRouge.Nom, gm.game.EquipeVerte.Nom, bh));
            }

            this.Refresh();
        }

        private void bh_Click(object sender, RoutedEventArgs e)
        {
            GameSimulation gm = simulations.ElementAt(Convert.ToInt32(((Button)sender).Tag));
            HistoriqueForm historique = new HistoriqueForm(gm.game, DateTime.Now, gm);
            historique.ShowDialog();
            //Afficher la fenêtre en fonction de simulation de jeu
        }

        private void parties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class TemplateHistorique
    {
        public string rouge { get; set; }
        public string verte { get; set; }
        public Button button { get; set; }
        public string tag { get; set; }

        public TemplateHistorique(string _rouge, string _verte, Button _button)
        {
            this.rouge = _rouge;
            this.verte = _verte;
            this.button = _button;
            this.tag = this.button.Tag.ToString();
        }
    }
}