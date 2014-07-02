using MaraudersAdventure.Historique_Interfaces;
using System;
using System.Collections.Generic;
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

        //Lance le jeu
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationForm conf = new ConfigurationForm();
            conf.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<ConfigurationGame> confs = new List<ConfigurationGame>();
            Historiques historiques = new Historiques(new List<GameSimulation>());
            historiques.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Map m = new Map(Simulation3(), 3);
            //m.ShowDialog();
        }
    
    }
}
