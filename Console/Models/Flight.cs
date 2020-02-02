using System.Collections.Generic;

namespace Console.Models
{
    public class Flight
    {
        private readonly string FlightFormat = "Flight: {0}, departure: {1}, arrival: {2}, day: {3}";

        public int? Day { get; set; }
        public int? FlightNumber { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }

        public void PrintFlight()
        {
            string flightString = string.Empty;
            if (Day.HasValue && FlightNumber.HasValue)
            {

                flightString = string.Format(FlightFormat, FlightNumber < 10 ? $"0{FlightNumber}":$"{FlightNumber}", Departure, Arrival, Day);
            }
            System.Console.WriteLine(flightString);
        }
       
    }
}
