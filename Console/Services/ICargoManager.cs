using Console.Models;
using System.Collections.Generic;

namespace Console.Services
{
    public interface ICargoManager
    {
        /// <summary>
        /// Read data from a context and save it locally
        /// </summary>
        /// <param name="data"></param>
        void InitializeData(Dictionary<string, Dictionary<string, string>> data);

        /// <summary>
        /// Load Flight based on days provided
        /// </summary>
        /// <param name="days"></param>
        void LoadFlightsByDay(int days);

        /// <summary>
        /// Put Orders to Flights based on Destination/Arrival
        /// </summary>
        /// <param name="batchCount"></param>
        void GenerateFlightOrdersByBatch(int batchCount);

        /// <summary>
        /// Clear Loaded Flights as well as it's Orders
        /// </summary>
        void ClearFlights();

        /// <summary>
        /// Clear Orders loaded to Flights
        /// </summary>
        void ClearOrders();

        /// <summary>
        /// Display all Orders
        /// </summary>
        void PrintOrders();

        /// <summary>
        /// Display all Flights
        /// </summary>
        void PrintFlights();
    }
}