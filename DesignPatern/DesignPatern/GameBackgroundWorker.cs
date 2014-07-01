using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MaraudersAdventure
{
    class GameBackgroundWorker : BackgroundWorker
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        GameSimulation game;
        MapFinale map;
        public GameBackgroundWorker(GameSimulation game, MapFinale map)
        {
            this.game = game;
            this.map = map;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ReportProgress);
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                //this.tbProgress.Text = "Canceled!";
            }
            else if (!(e.Error == null))
            {
                //this.tbProgress.Text = ("Error: " + e.Error.Message);
            }
            else
            {
                //this.tbProgress.Text = "Done!";
                
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int cptTours = 0;

            while (!game.etatPartie)
            {
                foreach (Personnage personnageEnCours in game.personnagesEnJeu)
                {

                    if (personnageEnCours.etat == Etat.mort)
                        continue;

                    game.joueurActuel = personnageEnCours;

                    game.tour(game.joueurActuel);
                    string res = game.PartieFinie();
                    Application.Current.Dispatcher.BeginInvoke(game.handler, res);
                    if (!string.IsNullOrEmpty(res))
                    {
                        game.etatPartie = true;
                        break;
                    }
                    Thread.Sleep(2000);
                    worker.ReportProgress(1);
                }
                cptTours++;
            }

            worker.ReportProgress(1);

        }

        private void worker_ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            map.UpdateMapLayout();
            //this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
        }


    }
}
