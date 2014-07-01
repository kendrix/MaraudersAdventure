using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAventure;

namespace MaraudersAdventure
{
    public enum eMode
    {
        NonDefini,
        Suicide,
        Ressuscitez,
        PositionRetour
    };

    public class EtatMajor : SujetAbstrait
    {
        // public eMode modeFonctionnement;
        public eMode modeFonctionnement { get; set; }
        public EtatMajor()
            : this(null)
        {
        }

        public EtatMajor(EtatMajor unParent)
        {
            Parent = unParent;        }

        public EtatMajor Parent { get; set; }
        public eMode ModeFonctionnement()
        {
            return modeFonctionnement;
        }
    }
}
