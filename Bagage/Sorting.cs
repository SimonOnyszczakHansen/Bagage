using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    public class Sorting
    {
        private readonly Queue<Bagage> inputBuffer;
        private readonly Queue<Bagage> sortingBuffer;
        public readonly Queue<Bagage> outputBuffer1;
        public readonly Queue<Bagage> outputBuffer2;
        public readonly Queue<Bagage> outputBuffer3;
        private readonly object _lockInput = new object();
        private readonly object _lockOutput = new object();
        private readonly object _monitor = new object();

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
            lock (_lockInput)
            {
                foreach (Bagage bagage in bagageQueue)
                {
                    inputBuffer.Enqueue(bagage);
                }
            }

            while (true)
            {
                lock (_monitor)
                {
                    if (inputBuffer.Count == 0 && sortingBuffer.Count == 0)
                    {
                        Monitor.PulseAll(_monitor);
                        break;
                    }

                    lock (_lockInput)
                    {
                        while (inputBuffer.Count > 0)
                        {
                            Bagage bagage = inputBuffer.Dequeue();
                            sortingBuffer.Enqueue(bagage);
                        }
                    }

                    while (sortingBuffer.Count > 0)
                    {
                        Bagage bagage = sortingBuffer.Dequeue();
                        if (bagage.Destination == "Denmark")
                        {
                            lock (_lockOutput)
                            {
                                outputBuffer1.Enqueue(bagage);
                                Monitor.Pulse(_monitor);
                            }
                        }
                        else if (bagage.Destination == "Sverige")
                        {
                            lock (_lockOutput)
                            {
                                outputBuffer2.Enqueue(bagage);
                                Monitor.Pulse(_monitor);
                            }
                        }
                        else if (bagage.Destination == "Norge")
                        {
                            lock (_lockOutput)
                            {
                                outputBuffer3.Enqueue(bagage);
                                Monitor.Pulse(_monitor);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ugyldig destination");
                        }
                    }

                    Monitor.Wait(_monitor);
                }
            }
        }
    }
}

