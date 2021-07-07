using System;
using FlightBooking.Core;

namespace FlightBookingProblem
{
    class Program
    {
        private static ScheduledFlight _scheduledFlight ;

        static void Main(string[] args)
        {
            SetupAirlineData();
            
            string command = "";
            do
            {
                command = Console.ReadLine() ?? "";
                var enteredText = command.ToLower();
                if (enteredText.Contains("print summary"))
                {
                    Console.WriteLine();
                    Console.WriteLine(_scheduledFlight.GetSummary());
                }
                else if (enteredText.Contains("add general"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new General
                    {
                        name = passengerSegments[2], 
                        age = Convert.ToInt32(passengerSegments[3])
                    });
                }
                else if (enteredText.Contains("add loyalty"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new Loyalty
                    {
                        name = passengerSegments[2], 
                        age = Convert.ToInt32(passengerSegments[3]),
                        loyaltyPoints = Convert.ToInt32(passengerSegments[4]),
                        isUsingLoyaltyPoints = Convert.ToBoolean(passengerSegments[5]),
                    });
                }
                else if (enteredText.Contains("add airline"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new AirlineEmployee
                    {
                        name = passengerSegments[2],
                        age = Convert.ToInt32(passengerSegments[3]),
                    });
                }
                else if (enteredText.Contains("add discount"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new Discounted
                    {
                        name = passengerSegments[2],
                        age = Convert.ToInt32(passengerSegments[3])
                    });
                }
                else if (enteredText.Contains("exit"))
                {
                    Environment.Exit(1);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("UNKNOWN INPUT");
                    Console.ResetColor();
                }
            } while (command != "exit");
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

            _scheduledFlight = new ScheduledFlight(londonToParis);

            _scheduledFlight.SetAircraftForRoute(
                new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12 });
        }
    }
}
