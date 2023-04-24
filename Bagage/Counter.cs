using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    internal class Counter
    {
        private Queue<Bagage> outputBuffer;
        private int _counterNumber;
        private bool _isOpen;
        private object _lockObject = new object();

        public Counter(int counterNumber)
        {
            _counterNumber = counterNumber;
            outputBuffer = new Queue<Bagage>(); 
        }

        public void Open()
        {
            lock (_lockObject)
            {
                if (!_isOpen)
                {
                    _isOpen = true;
                    Console.WriteLine($"Counter {_counterNumber} er åben!");
                }
            }
        }

        public void Close()
        {
            lock (_lockObject)
            {
                if (_isOpen)
                {
                    _isOpen = false;
                    Console.WriteLine($"Counter {_counterNumber} er lukket");
                }
            }
        }
        public void ReceiveBagage(Bagage bagage)
        {
            outputBuffer.Enqueue(bagage);
        }

        public void SendBagageToSorting(Sorting sorting)
        {
            sorting.SortBagage(outputBuffer);
        }
    }
}
