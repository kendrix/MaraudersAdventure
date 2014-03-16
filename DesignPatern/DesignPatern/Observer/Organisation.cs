using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    class Organisation : SujetConcret
    {
        public eMode modeFonctionnement;
        public eMode ModeFonctionnement
        {
            get { return modeFonctionnement; }
            set
            {
                modeFonctionnement = value;
                Update();
            }
        }

        public Organisation()
        {
        }

        public Organisation(Organisation unParent)
        {
        }

        private void Update()
        {
            throw new NotImplementedException();
        }
    }
}
