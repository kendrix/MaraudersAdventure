﻿using MaraudersAdventure.Zones.Etage;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;


// Add a using directive and a reference for System.Net.Http;
//using System.Net.Http;

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

        public delegate void WriteMessage(string message);

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
                    design = new MapDesign(Properties.Resources.chaussure, Properties.Resources.sol, 
                        Properties.Resources.saule_cogneur, Properties.Resources.bourse);
                    break;
                case MapType.labyrinthe:
                    design = new MapDesign(Properties.Resources.saule_cogneur, Properties.Resources.sol,
                        Properties.Resources.cailloux, Properties.Resources.bourse);
                    break;
                case MapType.standard:
                    design = new MapDesign(Properties.Resources.saule_cogneur, Properties.Resources.sol,
                        Properties.Resources.cailloux, Properties.Resources.bourse);
                    break;
            }

            maSimulation = new GameSimulation(conf, WriteLog);

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
        bool PartieEnCours = false;
        GameBackgroundWorker gbw;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PartieEnCours == false)
            {
                gbw = new GameBackgroundWorker(maSimulation, this);
                btnStart.Content = "Arreter";
                PartieEnCours = true;
            }
            else
            {
                gbw.CancelGame();
                WriteLog("PARTIE FINIE: arret demandé");
            }
        }

        public void UpdateMapLayout()
        {
            List<Personnage> mespersos = new List<Personnage>();
            mespersos.InsertRange(0, conf.EquipeRouge.Joueurs);
            mespersos.InsertRange(0, conf.EquipeVerte.Joueurs);

            List<Quete> mesquetes = new List<Quete>();
            mesquetes.InsertRange(0, conf.EquipeRouge.Quetes);
            mesquetes.InsertRange(0, conf.EquipeVerte.Quetes);

            ChessBoard.Children.Clear();
            for (int x = 0; x < Parametres.nbColonne; ++x)
            {
                for (int y = 0; y < Parametres.nbLigne; ++y)
                {
                    ZoneAbstraite zone = conf.Plateau.GetZone(new Position(x, y));
                    if (zone != null)
                    {
                        //List<ZoneAbstraite> meszones = conf.Plateau.GetNeighbourZones(zone.point);
                        ChessBoard.Children.Add(design.GetCaseImage(zone, mespersos, mesquetes));
                    }
                }
            }
            /***
             * Affichage des personnages
             ***/
            //this.personnages.ClearValue(TextBlock.TextProperty);

            personnages.Document.Blocks.Clear();

            String textEquipeRouge = conf.EquipeRouge.Nom + "\n";
            String textEquipeVerte = conf.EquipeVerte.Nom + "\n";

            foreach (Personnage p in mespersos)
            {
                if (p.equipe == TypeEquipe.Rouge)
                    textEquipeRouge += p.Nom + " (" + p.PointsDeVie + "pv)";
                else
                    textEquipeVerte += p.Nom + " (" + p.PointsDeVie + "pv)";
            }


            textEquipeRouge += "\n\n";
            textEquipeVerte += "\n\n";

            #region ajout des couleurs et du style de la fenêtre personnages

            TextRange styleEquipeRouge = new TextRange(personnages.Document.ContentEnd, personnages.Document.ContentEnd);
            styleEquipeRouge.Text = textEquipeRouge;
            styleEquipeRouge.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
            styleEquipeRouge.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);


            TextRange styleEquipeVerte = new TextRange(personnages.Document.ContentEnd, personnages.Document.ContentEnd);
            styleEquipeVerte.Text = textEquipeVerte;
            styleEquipeVerte.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green);
            styleEquipeVerte.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);

            #endregion

            /***
             * Fin affichage des personnages
             ***/
            this.Refresh();
            UpdateLayout();
        }

        List<Expander> listePerso = new List<Expander>();

        public void WriteLog(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                logs.Items.Add(s);
                //logs.ScrollIntoView(logs.Items[logs.Items.Count -1]);
                //logs.Items[logs.Items.Count - 1].Focus();

                /*logs.Focus();
                logs.SelectedIndex = logs.Items.Count - 1;
                ((ListBoxItem)logs.SelectedItem).Focus();*/
            }
            if (s != null && s.Contains("PARTIE FINIE"))
            {
                GameHistory.Instance.SaveGame(this.maSimulation);
                logs.Items.Add(" ----------------------");
                MessageBox.Show(s);
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Parametres.em.modeFonctionnement = eMode.Suicide;
            Parametres.em.Notify("");
            Parametres.em.ModeFonctionnement();
            WriteLog("Suicide collectif  -- PARTIE FINIE");
        }

    }
}
