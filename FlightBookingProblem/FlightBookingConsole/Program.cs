﻿using System;
using FlightBooking.Core;

namespace FlightBookingProblem
{
    class Program
    {
        private static ScheduledFlight _scheduledFlight ;

        static void Main(string[] args)
        {
            SetupAirlineData();

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
                            Passenger p = default(Passenger);
                            //section command section (ex. add *general* ...)
                            switch (command[1])
                            {
                                case "general":
                                    {
                                        p = new General
                                        {
                                            name = command[2],
                                            age = Convert.ToInt32(command[3])
                                        };
                                        break;
                                    }
                                case "loyalty":
                                    {
                                        p = new Loyalty
                                        {
                                            name = command[2],
                                            age = Convert.ToInt32(command[3]),
                                            loyaltyPoints = Convert.ToInt32(command[4]),
                                            isUsingLoyaltyPoints = Convert.ToBoolean(command[5]),
                                        };
                                        break;
                                    }
                                case "airline":
                                    {
                                        p = new AirlineEmployee
                                        {
                                            name = command[2],
                                            age = Convert.ToInt32(command[3])
                                        };
                                        break;
                                    }
                                case "discount":
                                    {
                                        p = new Discounted
                                        {
                                            name = command[2],
                                            age = Convert.ToInt32(command[3])
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
                new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12 });
        }
    }
}
