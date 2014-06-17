
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public enum Etage
    {
        etage1,
        etage2,
        etage3
    };
    class CaseEtage : ZoneAbstraite
    {
        Etage monEtage;

        public CaseEtage(string nom, Position p, int i)
            : base(nom, p, i)
        { }
    }
}
