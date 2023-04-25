using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    public class Counter
    {
        private Queue<Bagage> outputBuffer;
        private int CounterNumber;
        public bool isOpen;
        private readonly object _lock = new object();
        private readonly object _monitor = new object();

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
                    Console.WriteLine($"Counter {CounterNumber} er åben!");
                    Monitor.PulseAll(_monitor);
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
                    Console.WriteLine($"Counter {CounterNumber} er lukket");
                    Monitor.PulseAll(_monitor);
                }
            }
        }

        public void ReceiveBagage(Bagage bagage)
        {
            lock (_monitor)
            {
                outputBuffer.Enqueue(bagage);
                Monitor.PulseAll(_monitor);
            }
        }

        public void SendBagageToSorting(Sorting sorting)
        {
            lock (_monitor)
            {
                while (outputBuffer.Count == 0)
                {
                    Monitor.Wait(_monitor);
                }

                sorting.SortBagage(outputBuffer);
            }
        }
    }
}
