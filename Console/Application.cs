using System;
using System.Collections.Generic;
using System.Text;
using Console.Enums;
using Console.Services;

namespace Console
{

    public class Application
    {
        public ICargoManager CargoManager { get; }

        public Application(ICargoManager cargoManager)
        {
            this.CargoManager = cargoManager;
        }

        /// <summary>
        /// Run the whole application
        /// </summary>
        public void Run()
        {
            bool invalidSelection = false;
            string MenuFormat = "{0}-{1}";
            while (true)
            {
                ClearScreen();
                System.Console.WriteLine("MAIN MENU:");
                System.Console.WriteLine(MenuFormat, Menu.Flights.GetHashCode(), "Flights");
                System.Console.WriteLine(MenuFormat, Menu.Orders.GetHashCode(), "Orders");
                System.Console.WriteLine(MenuFormat, "x","Close Application");
                if (invalidSelection)
                {
                    System.Console.Write("Invalid Selection. Please try again. ");
                    invalidSelection = false;
                }
                var selection = ReadSelectedMenu(Menu.Main);
                if (selection.Equals("x"))
                {
                    break;
                }

                var success = Enum.TryParse(selection, out Menu selectedMenu);
                if (success)
                {
                    var isReturn = false;
                    switch (selectedMenu)
                    {
                        case Menu.Flights:
                            while (!isReturn)
                            {
                                DisplaySubMenu(selectedMenu);
                                selection = ReadSelectedMenu(selectedMenu);
                                if (selection.Equals("x"))
                                {
                                    isReturn = true;
                                }
                                else
                                {
                                    MenuAction action;
                                    if (Enum.TryParse(selection, true, out action))
                                    {
                                        WorkOnFlights(action);
                                    }
                                }
                            }
                            break;
                        case Menu.Orders:
                            while (!isReturn)
                            {
                                DisplaySubMenu(selectedMenu);
                                selection = ReadSelectedMenu(selectedMenu);
                                if (selection.Equals("x"))
                                {
                                    isReturn = true;
                                }
                                else
                                {
                                    if (Enum.TryParse(selection, out MenuAction menuAction))
                                    {
                                        WorkOnOrders(menuAction);
                                    }
                                }
                            }

                            break;
                        case Menu.Main:
                        default:
                            invalidSelection = true;
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Display all sub menus of a selected menu
        /// </summary>
        /// <param name="menu"></param>
        public void DisplaySubMenu(Menu menu)
        {
            if (menu != Menu.Main)
            {
                var type = menu.ToString();

                string MenuFormat = "{0} - {1} {2}";

                ClearScreen();
                System.Console.WriteLine($"Sub Menu - {type.ToUpper()}:");
                System.Console.WriteLine(MenuFormat, 1, MenuAction.Set.ToString(), type);
                System.Console.WriteLine(MenuFormat, 2, MenuAction.Print.ToString(), type);
                System.Console.WriteLine(MenuFormat, 3, MenuAction.Clear.ToString(), type);
                System.Console.WriteLine(MenuFormat, "x", "Return to ", Menu.Main.ToString());
            }

        }

        /// <summary>
        /// Display simple message to get console value of selected menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public string ReadSelectedMenu(Menu menu)
        {
            System.Console.Write($"Enter your selection from {menu.ToString()} Menu: ");
            var key = System.Console.ReadLine();
            return key;

        }

        /// <summary>
        /// Get Value from Console and display Proper Message based in Sub Menu selected.
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public string ReadInputForSubMenu(Menu menu)
        {
            switch (menu)
            {
                case Menu.Flights:
                    System.Console.Write($"Enter days of flights you want to load: ");
                    break;
                case Menu.Orders:
                    System.Console.Write($"Enter how much orders you want to load: ");
                    break;
                case Menu.Main:
                default:
                    break;
            }
            var key = System.Console.ReadLine();
            return key;

        }

        /// <summary>
        /// Work On Orders
        /// </summary>
        /// <param name="action"></param>
        public void WorkOnOrders(MenuAction action)
        {
            switch (action)
            {
                case MenuAction.Set:
                    System.Console.WriteLine();
                    var input = ReadInputForSubMenu(Menu.Orders);
                    int inputNumber;
                    if (int.TryParse(input, out inputNumber))
                    {
                        CargoManager.GenerateFlightOrdersByBatch(inputNumber);
                    }
                    break;
                case MenuAction.Print:
                    System.Console.WriteLine();
                    CargoManager.PrintOrders();
                    System.Console.WriteLine("Please press Enter to Continue...");
                    System.Console.ReadLine();
                    break;
                case MenuAction.Clear:
                    System.Console.WriteLine();
                    CargoManager.ClearOrders();
                    System.Console.WriteLine("Please press Enter to Continue...");
                    System.Console.ReadLine();
                    break;
                case MenuAction.Return:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Work on Flights 
        /// </summary>
        /// <param name="action"></param>
        public void WorkOnFlights(MenuAction action)
        {
            switch (action)
            {
                case MenuAction.Set:
                    System.Console.WriteLine();
                    var input = ReadInputForSubMenu(Menu.Flights);
                    int inputNumber;
                    if (int.TryParse(input, out inputNumber))
                    {
                        CargoManager.LoadFlightsByDay(inputNumber);
                        System.Console.Write("Loaded flights. ");
                        System.Console.WriteLine("Press Enter to continue...");
                        System.Console.ReadLine();
                    }
                    break;

                case MenuAction.Print:
                    System.Console.WriteLine();
                    CargoManager.PrintFlights();
                    System.Console.WriteLine("Press Enter to continue...");
                    System.Console.ReadLine();
                    break;

                case MenuAction.Clear:
                    System.Console.WriteLine();
                    System.Console.WriteLine();
                    CargoManager.ClearFlights();
                    System.Console.WriteLine("Press Enter to continue...");
                    System.Console.ReadLine();
                    break;

                case MenuAction.Return:
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Simply Clears the screen
        /// </summary>
        private void ClearScreen()
        {
            System.Console.Clear();
        }


    }
}
