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

        public Terminal(string destination)//Constructor to initialize the object with destination and bagagequeue
        {
            flightDestination = destination;
            bagageQueue = new Queue<Bagage>();
        }

        public void ReceiveBagage(Queue<Bagage> bagage)//Method to recieve bagage and add to bagagequeue if destination matches
        {
            lock (bagageQueue)//Lock bagagequeue object 
            {
                foreach (Bagage item in bagage)//Goes through each bagage item in the queue
                {
                    if (item.Destination == flightDestination)//Check if destination of bagage matches flight destination
                    {
                        Monitor.Enter(bagageQueue);//Enter section for bagagequeue to ensure thread safetu
                        try
                        {
                            bagageQueue.Enqueue(item);//Add the item to the bagage queue
                        }
                        finally
                        {
                            Monitor.Exit(bagageQueue);//Exit section for bagagequeue
                        }
                    }
                }
            }
        }

        public Queue<Bagage> GetBagage()//Method to get all the items from bagagequeue
        {
            lock (bagageQueue)//Lock bagagqueue for thread safety
            {
                Monitor.Enter(bagageQueue);//Enter section for bagagequeue 
                try
                {
                    Queue<Bagage> result = new Queue<Bagage>(bagageQueue);//Create a new queue with the same bagage items as bagagequeue
                    bagageQueue.Clear();//Clears the queue
                    return result;//Return the new queue with bagageitems
                }
                finally
                {
                    Monitor.Exit(bagageQueue);//Exit section for bagagequeue
                }
            }
        }
    }
}
