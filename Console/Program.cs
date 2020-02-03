using Console.Helpers;
using Console.Services;
using Figgle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Console
{
    partial class Program
    {
        public Program()
        {
        }
        public static Application Application { get; set; }

        static void Main(string[] args)
        {
            PrintTitle();
            System.Console.WriteLine("This a simple application for Scheduling Flights and Orders by Transport.ly");
            System.Console.WriteLine("Press Enter to continue...");
            System.Console.Read();

            try
            {
                var parsed = JSONParser.ParseFromFile<Dictionary<string, Dictionary<string, string>>>(Constant.FILE_PATH);
                var cargoManager = new CargoManager();
                cargoManager.InitializeData(parsed);

                Application = new Application(cargoManager);

                Application.Run();

                System.Console.WriteLine("Thank you for using the application! Bye.");

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Unable to run the application properly.");
                System.Console.WriteLine($"Error: {e.Message}");
                System.Console.WriteLine($"Error Stack Trace: {e.StackTrace}");
            }

        }


        private static void PrintTitle()
        {
            var title = FiggleFonts.Standard.Render("Transport.ly");
            System.Console.WriteLine(title);

        }

    }
}
