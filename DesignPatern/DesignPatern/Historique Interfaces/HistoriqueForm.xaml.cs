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
    /// Interaction logic for HistoriqueForm.xaml
    /// </summary>
    public partial class HistoriqueForm : Window
    {
        ConfigurationGame conf;
        DateTime time;
        GameSimulation maSimulation;
        public HistoriqueForm(ConfigurationGame conf, DateTime time, GameSimulation maSimulation)
        {
            InitializeComponent();
            this.conf = conf;
            this.time = time;
            this.maSimulation = maSimulation;
            ajoute_element();
        }

        private void ajoute_element()
        {
            lb.Items.Add("Le temps de commencer : " + time.Hour + ":" + time.Minute + ":" + time.Second);
            lb.Items.Add("Vous avez joue : " + (DateTime.Now.Hour - time.Hour) + " hours " + (DateTime.Now.Minute - time.Minute) + " minutes " + (DateTime.Now.Second - time.Second) + " seconds");
            lb.Items.Add("");
            lb.Items.Add("Equipe Rouge a " + conf.EquipeRouge.Joueurs.Count + " joueurs");
            lb.Items.Add("Les members sont suivant:");
            for (int i = 0; i < conf.EquipeRouge.Joueurs.Count; i++)
            {
                for (int j = 0; j < maSimulation.personnagesEnJeu.Length; j++)
                {
                    if (conf.EquipeRouge.Joueurs[i].Nom.Equals(maSimulation.personnagesEnJeu[j].Nom))
                    {
                        lb.Items.Add(conf.EquipeRouge.Joueurs[i].Nom + " : " + maSimulation.personnagesEnJeu[j].etat);
                    }
                }

            }
            lb.Items.Add("");

            lb.Items.Add("Equipe Rouge a " + conf.EquipeRouge.Quetes.Count + " quetes");
            lb.Items.Add("Les quetes sont suivant: ");
            for (int i = 0; i < conf.EquipeRouge.Quetes.Count; i++)
            {
                lb.Items.Add(conf.EquipeRouge.Quetes[i].Libelle);
            }
            lb.Items.Add("");
            lb.Items.Add("Equipe Verte a " + conf.EquipeVerte.Joueurs.Count + " joueurs");

            lb.Items.Add("Les members sont suivant:");
            for (int i = 0; i < conf.EquipeVerte.Joueurs.Count; i++)
            {
                for (int j = 0; j < maSimulation.personnagesEnJeu.Length; j++)
                {
                    if (conf.EquipeVerte.Joueurs[i].Nom.Equals(maSimulation.personnagesEnJeu[j].Nom))
                    {
                        lb.Items.Add(conf.EquipeVerte.Joueurs[i].Nom + " : " + maSimulation.personnagesEnJeu[j].etat);
                    }
                }
            }
            lb.Items.Add("");
            lb.Items.Add("Equipe Verte a " + conf.EquipeVerte.Quetes.Count + " quetes");
            lb.Items.Add("Les quetes sont suivant:");
            for (int i = 0; i < conf.EquipeVerte.Quetes.Count; i++)
            {
                lb.Items.Add(conf.EquipeVerte.Quetes[i].Libelle);
            }
        }
    }
}