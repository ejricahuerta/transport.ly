using Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console.Services
{
    public class CargoManager : ICargoManager
    {

        private List<Order> Orders { get; } = new List<Order>();

        private List<Flight> Flights { get;  set; } = new List<Flight>();

        private readonly List<string> FlightLocations = new List<string>
        {
                    Constant.TORONTO_CODE,
                    Constant.CALGARY_CODE,
                    Constant.VANCOUVER_CODE
        };

        public void GenerateFlightOrdersByBatch(int batchCount)
        {
            if (Flights.Any())
            {
                foreach (var flight in Flights)
                {
                    var orders = Orders.Take(batchCount)
                        .Where(p => !p.IsLoaded && flight.Arrival == p.Destination)
                        .Take(Constant.MAX_ORDER_PER_FLIGHT);
                    foreach (var order in orders)
                    {
                        order.Flight = flight;
                    }
                }
            }
            else
            {
                System.Console.WriteLine("There are no Flights available. ");
                System.Console.WriteLine("Press Enter to continue..");
                System.Console.ReadLine();
            }
        }

        public void InitializeData(Dictionary<string, Dictionary<string, string>> data)
        {
            foreach (var (key, value) in data)
            {
                Orders.Add(new Order { OrderId = key, Destination = value[Constant.DESTINATION] });
            }
        }

        public void LoadFlightsByDay(int days)
        {
            Flights.Clear();

            var flightNumber = 1;
            if (days > 0)
            {
                for (int i = 0; i < days; i++)
                {
                    foreach (var location in FlightLocations)
                    {
                        var flight = new Flight
                        {
                            Arrival = location,
                            Day = i + 1,
                            Departure = Constant.MONTREAL_CODE,
                            FlightNumber = flightNumber,
                        };
                        flightNumber++;
                        Flights.Add(flight);
                    }
                }
            }
        }

        public void ClearFlights()
        {
            if (Flights.Any())
            {

                Flights.Clear();
                if (Orders.Any(p => p.Flight != null))
                {
                    foreach (var order in Orders)
                    {
                        order.Flight = null;
                    }
                }

                System.Console.Write("Successfully cleared loaded flights. ");
            }
            else
            {
                System.Console.Write("There are no Flights found. ");

            }

        }

        public void PrintOrders()
        {
            if (!Orders.Any(p => p.Flight != null))
            {
                System.Console.WriteLine("No Orders Loaded.");
            }

            foreach (var order in Orders)
            {
                order.PrintFlightOrders();
            }
        }

        public void PrintFlights()
        {
            if (Flights.Any())
            {
                foreach (var flight in Flights)
                {
                    flight.PrintFlight();
                }
            }
            else
            {
                System.Console.WriteLine("There are no flights loaded.");
            }
        }

        public void ClearOrders()
        {
            if (Orders.Any(p => p.Flight != null))
            {
                foreach (var order in Orders)
                {
                    order.Flight = null;
                }
                System.Console.Write("Successfully cleared loaded Orders. ");
            }
            else
            {
                System.Console.Write("There are no Orders loaded to any Flights found. ");

            }
        }
    }
}

