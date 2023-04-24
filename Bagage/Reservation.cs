using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagage
{
    internal class Reservation
    {
        public int PassengerNumber { get; set; }
        public string PassengerName { get; set; }
        public string FlightDeparture { get; set; }

        public Reservation(int passengerNumber, string passengerName, string flightDeparture)
        {
            PassengerNumber = passengerNumber;
            PassengerName = passengerName;
            FlightDeparture = flightDeparture;
        }
        public void Display()
        {
            Console.WriteLine($"Passenger {PassengerName} ({PassengerNumber}) is booked on flight {FlightDeparture}");
        }

        public bool IsOnFlight(string flightCode)
        {
            return FlightDeparture == flightCode;
        }

        public bool HasMatchingPassengerNumber(int passengerNumber)
        {
            return PassengerNumber == passengerNumber;
        }
    }
}
