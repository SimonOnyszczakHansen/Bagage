using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    public class Counter
    {
        private Queue<Bagage> outputBuffer;//A queue to hold the bagage recieved by the counter
        private int CounterNumber;//Number of the counter
        public bool isOpen;//A bool that indicates if the counter is open or closed
        private readonly object _lock = new object();//A lock object synchronizing access to the isOpen field
        private readonly object _monitor = new object();//A monitor object to synchronize access to the outputbuffer field

        public Counter(int counterNumber)
        {
            CounterNumber = counterNumber;
            outputBuffer = new Queue<Bagage>();
        }

        public void Open()
        {
            lock (_lock)
            {
                if (isOpen != true)
                {
                    isOpen = true;
                    Console.WriteLine($"Counter {CounterNumber} er åben!");//Tell the user that the counter is open
                    Monitor.PulseAll(_monitor);//Signal all waiting threads that the state of the counter has changed
                }
            }
        }

        public void Close()
        {
            lock (_lock)
            {
                if (isOpen != false)
                {
                    isOpen = false;
                    Console.WriteLine($"Counter {CounterNumber} er lukket");//Tell the user that the counter is closed
                    Monitor.PulseAll(_monitor);//Signal all waiting threads that the state of the outputbuffer queue has changed
                }
            }
        }

        public void ReceiveBagage(Bagage bagage)
        {
            lock (_monitor)
            {
                outputBuffer.Enqueue(bagage);//Add the bagage to the ooutputbuffer queue
                Monitor.PulseAll(_monitor);//Signal all waiting threads that the state of the outputbuffer quueue has changed
            }
        }

        public void SendBagageToSorting(Sorting sorting)
        {
            lock (_monitor)
            {
                while (outputBuffer.Count == 0)
                {
                    Monitor.Wait(_monitor);//Wait until there is at least one bagage in the queue
                }

                sorting.SortBagage(outputBuffer);//send the bagage to the sorting area 
            }
        }
    }
}
