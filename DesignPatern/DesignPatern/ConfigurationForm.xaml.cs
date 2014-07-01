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
    /// Interaction logic for ConfigurationForm.xaml
    /// </summary>
    public partial class ConfigurationForm : Window
    {
        string[] redTeam = { "Harry Potter", "Ron Weasley", "Hermione Granger", "Neville Longdubat", "Ginny Weasley", "Remus Lupin", "Nymphadora Tonks", "Fred Weasley", "George Weasley", "Professeur McGonagal" };
        string[] greenTeam = { "Voldemort", "Nagini", "Bellatrix Lestranges", "Lucius Malefoy", "Fenrir Greyback", "Vincent Crabbe", "Gregory Goyle", "Drago Malefoy", "Avery", "Nott" };



        List<Personnage> RTeam = new List<Personnage>();
        List<Personnage> GTeam = new List<Personnage>();



        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                cbJoueurRouge.Items.Add(i + 1);
                cbJoueurVerte.Items.Add(i + 1);

                RTeam.Add(new Chevalier(Parametres.em, redTeam[i], TypeEquipe.Rouge));
                GTeam.Add(new Chevalier(Parametres.em, greenTeam[i], TypeEquipe.Verte));
            }

            cbJoueurRouge.SelectedIndex = 0;
            cbJoueurVerte.SelectedIndex = 0;

            cbQueteRouge.Items.Add("Objet");
            cbQueteRouge.Items.Add("Lieu"); 
            cbQueteRouge.Items.Add("Tuer");

            cbQueteVerte.Items.Add("Objet");
            cbQueteVerte.Items.Add("Lieu");
            cbQueteVerte.Items.Add("Tuer");

            cbQueteRouge.SelectedIndex = 0;
            cbQueteVerte.SelectedIndex = 0;
            //radioMaraudeur. = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddGamers(cbJoueurRouge, lbJoueurRouge, RTeam);
        }

        private void cbJoueurVerte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddGamers(cbJoueurVerte, lbJoueurVerte, GTeam);
        }

        private void AddGamers(ComboBox nbJoueur, ListBox lstJoueurs, List<Personnage> tabNoms)
        {
            lstJoueurs.Items.Clear();
            int nb = nbJoueur.SelectedIndex + 1;

            for ( int i = 0; i <nb; i++)
            {
                lstJoueurs.Items.Add(tabNoms[i].Nom);
            }
        }

        private void btnAddQRouge_Click(object sender, RoutedEventArgs e)
        {
            AddQuest(cbQueteRouge, lbQueteRouge);
        }

        private void btnAddQVerte_Click(object sender, RoutedEventArgs e)
        {
            AddQuest(cbQueteVerte, lbQueteVerte);
        }

        private void AddQuest(ComboBox cb, ListBox lst)
        {
            string type = cb.SelectedValue.ToString();
            lst.Items.Add(type);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(nomEquipeVerte.Text) || string.IsNullOrEmpty(nomEquipeRouge.Text))
                return;
            List<Personnage> liste = new List<Personnage>();
            for (int i = 0; i < cbJoueurRouge.SelectedIndex + 1; i++)
            {
                liste.Add(RTeam[i]);
                Parametres.em.Attach(RTeam[i]);
            }

            List<Personnage> liste2 = new List<Personnage>();
            for (int i = 0; i < cbJoueurVerte.SelectedIndex + 1; i++)
            {
                liste2.Add(GTeam[i]);

                Parametres.em.Attach(GTeam[i]);
            }

            List<string> liste3 = new List<string>();
            foreach (string i in lbQueteRouge.Items)
                liste3.Add(i.ToString());
            List<string> liste4 = new List<string>();
            foreach (string i in lbQueteVerte.Items)
                liste4.Add(i.ToString());

            MapType type ;
            if (radioMaraudeur.IsChecked.Value)
                type = MapType.labyrinthe;
            else if (RadioEtage.IsChecked.Value)
                type = MapType.portoloin;
            else
                type = MapType.standard;

            ConfigurationGame conf = new ConfigurationGame(nomEquipeRouge.Text, nomEquipeVerte.Text,
                    liste, liste2, liste3, liste4, type);
            //MapGame game = new MapGame(conf);
            MapFinale game = new MapFinale(conf);
            game.ShowDialog();
        }

        private void lbJoueurRouge_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void lbJoueurVerte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ConfigureGamers(int id, List<Personnage> team)
        {
            PopUpPersonnage pup = new PopUpPersonnage(team[id]);            
            pup.ShowDialog();

            team[id] = pup.p;
        }

        private void lbJoueurRouge_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox i = (ListBox)sender;
            int ii = i.SelectedIndex;
            ConfigureGamers(ii, RTeam);
        }

        private void lbJoueurVerte_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox i = (ListBox)sender;
            int ii = i.SelectedIndex;
            ConfigureGamers(ii, GTeam);
        }
    }
}
