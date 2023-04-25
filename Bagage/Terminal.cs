using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    public class Terminal
    {
        private string flightDestination;
        private Queue<Bagage> bagageQueue;

        public Terminal(string destination)
        {
            flightDestination = destination;
            bagageQueue = new Queue<Bagage>();
        }

        public void ReceiveBagage(Queue<Bagage> bagage)
        {
            lock (bagageQueue)
            {
                foreach (Bagage item in bagage)
                {
                    if (item.Destination == flightDestination)
                    {
                        Monitor.Enter(bagageQueue);
                        try
                        {
                            bagageQueue.Enqueue(item);
                        }
                        finally
                        {
                            Monitor.Exit(bagageQueue);
                        }
                    }
                }
            }
        }

        public Queue<Bagage> GetBagage()
        {
            lock (bagageQueue)
            {
                Monitor.Enter(bagageQueue);
                try
                {
                    var result = new Queue<Bagage>(bagageQueue);
                    bagageQueue.Clear();
                    return result;
                }
                finally
                {
                    Monitor.Exit(bagageQueue);
                }
            }
        }
    }
}
