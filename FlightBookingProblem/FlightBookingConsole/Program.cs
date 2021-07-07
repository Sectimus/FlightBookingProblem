using System;
using FlightBooking.Core;

namespace FlightBookingProblem
{
    class Program
    {
        private static ScheduledFlight _scheduledFlight;

        static void Main(string[] args)
        {
            SetupAirlineData(); //AirlineData is currently hardcoded, however can be modified to work dynamically.

            string[] command;
            do
            {
                string enteredText = Console.ReadLine() ?? "";
                command = enteredText.ToLower().Split(' ');

                //first command section (ex. *add* general ...)
                switch (command[0])
                {
                    case "add":
                        {
                            Passenger p = null;
                            //section command section (ex. add *general* ...)
                            switch (command[1])
                            {
                                case "general":
                                    {
                                        p = new General
                                        {
                                            Name = command[2],
                                            Age = Convert.ToInt32(command[3])
                                        };
                                        break;
                                    }
                                case "loyalty":
                                    {
                                        p = new Loyalty
                                        {
                                            Name = command[2],
                                            Age = Convert.ToInt32(command[3]),
                                            LoyaltyPoints = Convert.ToInt32(command[4]),
                                            IsUsingLoyaltyPoints = Convert.ToBoolean(command[5]),
                                        };
                                        break;
                                    }
                                case "airline":
                                    {
                                        p = new AirlineEmployee
                                        {
                                            Name = command[2],
                                            Age = Convert.ToInt32(command[3])
                                        };
                                        break;
                                    }
                                case "discount":
                                    {
                                        p = new Discounted
                                        {
                                            Name = command[2],
                                            Age = Convert.ToInt32(command[3])
                                        };
                                        break;
                                    }
                                default:
                                    {
                                        DisplayUnknownCommand(command[1], command[0]);
                                        break;
                                    }
                            }
                            if (p != null) _scheduledFlight.AddPassenger(p);
                            break;
                        }
                    //provides expansion for printing other values
                    case "print":
                        {
                            //section command section (ex. print *summary* ...)
                            switch (command[1])
                            {
                                case "summary":
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine(_scheduledFlight.GetSummary());
                                        break;
                                    }
                                default:
                                    {
                                        DisplayUnknownCommand(command[1], command[0]);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "exit":
                        {
                            Environment.Exit(1);
                            break;
                        }
                    default:
                        {
                            DisplayUnknownCommand(command[0]);
                            break;
                        }
                }
            } while (command[0] != "exit");
        }

        /// <summary>
        /// Prints the string entered with the unknown command highlighted.
        /// </summary>
        /// <param name="unknown">The last unknown command</param>
        /// <param name="known">All previously known commands</param>
        private static void DisplayUnknownCommand(string unknown, string known = "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("UNKNOWN COMMAND: ");
            Console.ResetColor();
            Console.Write(known+" ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(unknown);
            Console.ResetColor();
        }

        /// <summary>
        /// Schedules a test flight with hardcoded values.
        /// </summary>
        private static void SetupAirlineData()
        {
            FlightRoute londonToParis = new FlightRoute("London", "Paris")
            {
                BaseCost = 50, 
                BasePrice = 100, 
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };

            _scheduledFlight = new ScheduledFlight(londonToParis, ScheduledFlight.Ruleset.Relaxed);

            _scheduledFlight.SetAircraftForRoute(
                aircraft:new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12 },
                alternatives:new Plane[] 
                {
                    new Plane { Id = 456, Name = "Dornier 328", NumberOfSeats = 24 },
                    new Plane { Id = 789, Name = "Dash 8-100", NumberOfSeats = 37 }
                }
            );
        }
    }
}
