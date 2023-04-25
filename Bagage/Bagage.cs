using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    public class Bagage
    {
        public string Destination { get; set; }//Prperty that gets or sets the destination of the bagage
        public int Weight { get; set; }//Property that gets ot sets the weight of thae bagage in kg
        public Bagage(string destination, int weight)//Constructor that initialized a new instance of the bagage class with the given destination and weight 
        {
            Destination = destination;
            Weight = weight;
        }
    }
}
