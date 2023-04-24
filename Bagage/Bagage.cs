using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    internal class Bagage
    {
        public string Destination { get; set; }
        public int Weight { get; set; }
        public Bagage(string destination, int weight)
        {
            Destination = destination;
            Weight = weight;
        }
    }
}
