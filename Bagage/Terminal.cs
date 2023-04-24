using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    internal class Terminal
    {
        // Properties
        public int TerminalNumber { get; set; }
        public bool IsOpen { get; set; }
        public Queue<string> BaggageQueue { get; set; }
        private readonly object _lock = new object();

        // Constructor
        public Terminal(int terminalNumber)
        {
            TerminalNumber = terminalNumber;
            IsOpen = false;
            BaggageQueue = new Queue<string>();
        }

        // Methods
        public void Open()
        {
            lock (_lock)
            {
                IsOpen = true;
                Console.WriteLine($"Terminal {TerminalNumber} is now open");
                Monitor.PulseAll(_lock);
            }
        }

        public void Close()
        {
            lock (_lock)
            {
                IsOpen = false;
                Console.WriteLine($"Terminal {TerminalNumber} is now closed");
                Monitor.PulseAll(_lock);
            }
        }

        public void AddBaggage(string baggageNumber)
        {
            lock (_lock)
            {
                while (!IsOpen)
                {
                    Console.WriteLine($"Terminal {TerminalNumber} is closed and cannot accept baggage");
                    Monitor.Wait(_lock);
                }

                if (BaggageQueue.Count >= 10)
                {
                    Console.WriteLine($"Warning: Terminal {TerminalNumber} baggage queue is full");
                }

                BaggageQueue.Enqueue(baggageNumber);
                Console.WriteLine($"Baggage {baggageNumber} added to Terminal {TerminalNumber} queue");
                Monitor.PulseAll(_lock);
            }
        }

        public string RetrieveBaggage()
        {
            lock (_lock)
            {
                while (!IsOpen)
                {
                    Console.WriteLine($"Terminal {TerminalNumber} is closed and cannot retrieve baggage");
                    Monitor.Wait(_lock);
                }

                if (BaggageQueue.Count == 0)
                {
                    Console.WriteLine($"Warning: Terminal {TerminalNumber} baggage queue is empty");
                    Monitor.PulseAll(_lock);
                    return null;
                }

                string baggageNumber = BaggageQueue.Dequeue();
                Console.WriteLine($"Baggage {baggageNumber} retrieved from Terminal {TerminalNumber} queue");
                Monitor.PulseAll(_lock);
                return baggageNumber;
            }
        }
    }
}
