using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public class ObservateurConcret : ObservateurAbstrait
    {
        private readonly string nom;
        private string etatObservé;

        public ObservateurConcret(SujetConcret subject, string name)
        {
            Subjet = subject;
            nom = name;
        }

        public SujetConcret Subjet { get; set; }

        public override void Update(string message)
        {
            etatObservé = Subjet.SubjectState;
            Console.WriteLine("Observer {0} : new state is {1} --> {2}", nom, etatObservé, message);
        }
    }
}
