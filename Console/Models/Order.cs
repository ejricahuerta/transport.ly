namespace Console.Models
{
    public class Order
    {

        private readonly string OrderFormat = "order: {0}, flightNumber: {1}, departure: {2}, arrival: {3}, day: {4}";
        private readonly string OrderNotScheduledFormat = "order: {0}, flightNumber: not scheduled";

        private Flight _flight;
        public bool IsLoaded { get; set; } = false;
        public string OrderId { get; set; }

        public string Destination { get; set; }


        public Flight Flight
        {
            get
            {
                return _flight;
            }
            set
            {
                _flight = value;
                IsLoaded = _flight != null;

            }
        }

        public void PrintFlightOrders()
        {

            string flightstring = string.Format(OrderNotScheduledFormat, OrderId);
            if (Flight != null && Flight.Day.HasValue && Flight.FlightNumber.HasValue)
            {
                flightstring = string.Format(OrderFormat, OrderId, Flight.FlightNumber, Flight.Departure, Flight.Arrival, Flight.Day);
            }
            System.Console.WriteLine(flightstring);

        }
    }
}
