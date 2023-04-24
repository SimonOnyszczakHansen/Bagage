using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    internal class Sorting
    {
        private Queue<Bagage> inputBuffer;
        private Queue<Bagage> sortingBuffer;
        private Queue<Bagage> outputBuffer1;
        private Queue<Bagage> outputBuffer2;
        private Queue<Bagage> outputBuffer3;

        public Sorting()
        {
            inputBuffer = new Queue<Bagage>();
            sortingBuffer = new Queue<Bagage>();
            outputBuffer1 = new Queue<Bagage>();
            outputBuffer2 = new Queue<Bagage>();
            outputBuffer3 = new Queue<Bagage>();
        }

        public void SortBagage(Queue<Bagage> bagageQueue)
        {
            lock (inputBuffer)
            {
                foreach (var bagage in bagageQueue)
                {
                    inputBuffer.Enqueue(bagage);
                }
            }

            while (inputBuffer.Count > 0 || sortingBuffer.Count > 0)
            {
                lock (inputBuffer)
                {
                    while (inputBuffer.Count > 0)
                    {
                        Bagage bagage = inputBuffer.Dequeue();
                        sortingBuffer.Enqueue(bagage);
                    }
                }

                while (sortingBuffer.Count > 0)
                {
                    Bagage bagage;
                    lock (sortingBuffer)
                    {
                        bagage = sortingBuffer.Dequeue();
                    }

                    if (bagage.Destination == "Denmark")
                    {
                        outputBuffer1.Enqueue(bagage);
                    }

                    else if (bagage.Destination == "Sverige")
                    {
                        outputBuffer2.Enqueue(bagage);
                    }

                    else if ( bagage.Destination == "Norge")
                    {
                        outputBuffer3.Enqueue(bagage);
                    }

                    else
                    {
                        Console.WriteLine("Ugyldig destination");
                    }
                }
            }
        }
    }
}

