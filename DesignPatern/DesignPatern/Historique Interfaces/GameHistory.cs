using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public sealed class GameHistory
    {
        public static GameHistory Instance { get; set; }
        public List<GameSimulation> historySimulations = new List<GameSimulation>();

        static GameHistory() { Instance = new GameHistory(); }

        public void SaveGame(GameSimulation _gm)
        {
            this.historySimulations.Add(_gm);
        }    
    }
}
