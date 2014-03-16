using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int l, int L)
        {
            X = l;
            Y = L;
        }
    }
}
