using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public abstract class SujetAbstrait
    {
        private readonly List<ObservateurAbstrait> observateurList = new List<ObservateurAbstrait>();

        public void Attach(ObservateurAbstrait observer)
        {
            observateurList.Add(observer);
        }

        public void Detach(ObservateurAbstrait observer)
        {
            observateurList.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (ObservateurAbstrait o in observateurList)
            {
                o.Update(message);
            }
        }
    }
}
