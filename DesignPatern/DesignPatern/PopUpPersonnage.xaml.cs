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
    /// Interaction logic for PopUpPersonnage.xaml
    /// </summary>
    public partial class PopUpPersonnage : Window
    {
        public Personnage p;
        //TypeEquipe equipe;

        public PopUpPersonnage(Personnage _p)
        {
            InitializeComponent();
            p=_p;
        }

     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EtatMajor em = new EtatMajor();
            cbType.Items.Add(new Gobelin(em, p.Nom, p.equipe));
            cbType.Items.Add(new Sorcier(em, p.Nom, p.equipe));
            cbType.Items.Add(new Moldu(em, p.Nom, p.equipe));
            cbType.Items.Add(new Mangemort(em, p.Nom, p.equipe));

            if (p.type == TypePersonnage.Archer)
                cbType.SelectedIndex = 0;
            else if (p.type == TypePersonnage.Chevalier)
                cbType.SelectedIndex = 1;
            else if (p.type == TypePersonnage.Fantassin)
                cbType.SelectedIndex = 3;
            else if (p.type == TypePersonnage.Princesse)
                cbType.SelectedIndex = 2;
            
            txtNom.Text = p.Nom;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            p = (Personnage) cbType.SelectedItem;
            this.Close();
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Personnage p = (Personnage)cbType.SelectedItem;
            txtPtAtt.Content = p.PointsDAttaque;
            txtPtVie.Content = p.PointsDeVie;
            txtVitesse.Content = p.Vitesse;
        }
    }
}
