using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        public ScheduledFlight(FlightRoute flightRoute)
        {
            FlightRoute = flightRoute;
            Passengers = new List<Passenger>();
        }

        public FlightRoute FlightRoute { get; private set; }
        public Plane Aircraft { get; private set; }
        public List<Passenger> Passengers { get; private set; }

        public void AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
        }

        public void SetAircraftForRoute(Plane aircraft)
        {
            Aircraft = aircraft;
        }
        
        //returns a string of the flight summary
        public string GetSummary()
        {
            double costOfFlight = 0;
            double profitFromFlight = 0;
            int totalLoyaltyPointsAccrued = 0;
            int totalLoyaltyPointsRedeemed = 0;
            int totalExpectedBaggage = 0;
            int seatsTaken = 0;

            foreach (var passenger in Passengers)
            {
                switch (passenger)
                {
                    case General p:
                        {
                            profitFromFlight += FlightRoute.BasePrice;
                            break;
                        }
                    case Loyalty p:
                        {
                            if (p.isUsingLoyaltyPoints)
                            {
                                int loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(FlightRoute.BasePrice));
                                p.loyaltyPoints -= loyaltyPointsRedeemed;
                                totalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                            }
                            else
                            {
                                totalLoyaltyPointsAccrued += FlightRoute.LoyaltyPointsGained;
                                profitFromFlight += FlightRoute.BasePrice;                           
                            }
                            break;
                        }
                    case AirlineEmployee p:
                        {
                            //todo
                            break;
                        }
                }
                costOfFlight += FlightRoute.BaseCost;
                seatsTaken++;
                totalExpectedBaggage += passenger.allowedBags;
            }

            //prequesite values 
            double profitSurplus = profitFromFlight - costOfFlight;
            bool proceed = profitSurplus > 0 && seatsTaken < Aircraft.NumberOfSeats && seatsTaken / (double)Aircraft.NumberOfSeats > FlightRoute.MinimumTakeOffPercentage;

            //result formatting
            StringBuilder result = new StringBuilder("Flight summary for " + FlightRoute.Title);
            int indentationLevel = 4;

            result.AppendLine()
                .AppendLine("Total passengers: " + seatsTaken)
                .Append(' ', indentationLevel).AppendLine("General sales: " + Passengers.Count(p => p is General))
                .Append(' ', indentationLevel).AppendLine("Loyalty member sales: " + Passengers.Count(p => p is Loyalty))
                .Append(' ', indentationLevel).AppendLine("Airline employee comps: " + Passengers.Count(p => p is AirlineEmployee))
                .AppendLine()
                .AppendLine("Total expected baggage: " + totalExpectedBaggage)
                .AppendLine()
                .AppendLine("Total revenue from flight: " + profitFromFlight)
                .AppendLine("Total costs from flight: " + costOfFlight)
                .AppendLine((profitSurplus > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + profitSurplus)
                .AppendLine()
                .AppendLine("Total loyalty points given away: " + totalLoyaltyPointsAccrued)
                .AppendLine("Total loyalty points redeemed: " + totalLoyaltyPointsRedeemed)
                .AppendLine()
                .Append(proceed ? "THIS FLIGHT MAY PROCEED" : "FLIGHT MAY NOT PROCEED");

            return result.ToString();
        }
    }
}
