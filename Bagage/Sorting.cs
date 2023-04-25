using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    public class Sorting
    {
        //Declare fields for the different buffers
        private readonly Queue<Bagage> inputBuffer;
        private readonly Queue<Bagage> sortingBuffer;
        public readonly Queue<Bagage> outputBuffer1;
        public readonly Queue<Bagage> outputBuffer2;
        public readonly Queue<Bagage> outputBuffer3;

        //Lock objects for input and output buffers
        private readonly object _lockInput = new object();
        private readonly object _lockOutput = new object();

        //A monitor object
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
            lock (_lockInput)//Lock the input buffer to add new bagages
            {
                foreach (Bagage bagage in bagageQueue)//For each bagage the is in the bagagequeue it adds and item
                {
                    inputBuffer.Enqueue(bagage);
                }
            }

            while (true)//While there are still bagages in the input or sorting buffer
            {
                lock (_monitor)//Lock the monitor to synchronize access to the input and sorting buffer
                {
                    if (inputBuffer.Count == 0 && sortingBuffer.Count == 0)//If there are no bagages left to sort, notify all the waiting threads
                    {
                        Monitor.PulseAll(_monitor);
                        break;
                    }

                    lock (_lockInput)//Move all the bagage from the input buffer to sorting buffer
                    {
                        while (inputBuffer.Count > 0)
                        {
                            Bagage bagage = inputBuffer.Dequeue();
                            sortingBuffer.Enqueue(bagage);
                        }
                    }

                    while (sortingBuffer.Count > 0)//Sort each bagage in the sorting buffer and add it to the appropriate output buffer
                    {
                        Bagage bagage = sortingBuffer.Dequeue();
                        if (bagage.Destination == "Denmark")
                        {
                            lock (_lockOutput)//Lock the outputbuffer for denmark and add the bagage
                            {
                                outputBuffer1.Enqueue(bagage);
                                Monitor.Pulse(_monitor);//NotifyFilters any waiting threads
                            }
                        }
                        else if (bagage.Destination == "Sverige")
                        {
                            lock (_lockOutput)//Lock the output buffer for sverige and add the bagage
                            {
                                outputBuffer2.Enqueue(bagage);
                                Monitor.Pulse(_monitor);//Notify any waiting threads
                            }
                        }
                        else if (bagage.Destination == "Norge")
                        {
                            lock (_lockOutput)//Lock the outputbuffer for norge and ad the bagage
                            {
                                outputBuffer3.Enqueue(bagage);
                                Monitor.Pulse(_monitor);//Notify any waiting threads
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ugyldig destination");//If the bagage has an invalid destination, print an error message
                        }
                    }

                    Monitor.Wait(_monitor);//Wait for a pulse on the monitor before continuing to sort more bagages
                }
            }
        }
    }
}

