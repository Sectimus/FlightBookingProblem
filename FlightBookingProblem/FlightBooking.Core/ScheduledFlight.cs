using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        public enum Ruleset
        {
            Default,
            Relaxed
        }

        public ScheduledFlight(FlightRoute flightRoute, Ruleset ruleset = Ruleset.Default)
        {
            FlightRoute = flightRoute;
            Passengers = new List<Passenger>();
            this.ruleset = ruleset;
        }

        public Ruleset ruleset { get; set; }
        public FlightRoute FlightRoute { get; private set; }
        public Plane Aircraft { get; private set; }
        public List<Plane> alternativeAircraft { get; private set; }
        public List<Passenger> Passengers { get; private set; }

        public void AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
        }

        public void SetAircraftForRoute(Plane aircraft, Plane[] alternatives = null)
        {
            Aircraft = aircraft;
            //check for default null value to init a 0 len array as a default param
            this.alternativeAircraft = new List<Plane>();
            this.alternativeAircraft.AddRange(alternatives != null ? alternatives : new Plane[0]);
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
                    case Discounted p:
                        {
                            profitFromFlight += FlightRoute.BasePrice/2; //discounted passengers are only charged half the normal base price for the route
                            break;
                        }
                }
                costOfFlight += FlightRoute.BaseCost;
                seatsTaken++;
                totalExpectedBaggage += passenger.allowedBags;
            }

            //prequesite values 
            double profitSurplus = profitFromFlight - costOfFlight;
            bool flightRulesetMet = meetsFlightRuleset(profitSurplus, seatsTaken, this.ruleset);
            if (!flightRulesetMet) //check if alternative aircraft is needed
            {
                for (int i = alternativeAircraft.Count-1; i >=0; i--)
                {
                    if (!meetsFlightRuleset(profitSurplus, seatsTaken, this.ruleset, alternativeAircraft[i]))
                    {
                        alternativeAircraft.RemoveAt(i);
                    }
                }
            }

            //result formatting
                StringBuilder result = new StringBuilder("Flight summary for " + FlightRoute.Title);
            int indentationLevel = 4;

            result.AppendLine()
                .AppendLine("Total passengers: " + seatsTaken)
                .Append(' ', indentationLevel).AppendLine("General sales: " + Passengers.Count(p => p is General))
                .Append(' ', indentationLevel).AppendLine("Loyalty member sales: " + Passengers.Count(p => p is Loyalty))
                .Append(' ', indentationLevel).AppendLine("Airline employee comps: " + Passengers.Count(p => p is AirlineEmployee))
                .Append(' ', indentationLevel).AppendLine("Discounted sales: " + Passengers.Count(p => p is Discounted))
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
                .AppendLine(flightRulesetMet ? "THIS FLIGHT MAY PROCEED" : "THIS FLIGHT MAY NOT PROCEED");

            //append alternatives if needed
            if (!flightRulesetMet && alternativeAircraft.Count > 0)
            {
                result.AppendLine("Other more suitable aircraft are: ");
                foreach (var alternative in alternativeAircraft)
                {
                    result.AppendLine(alternative.Name+" could handle this flight.");
                }
            }


            return result.ToString();
        }


        private bool meetsFlightRuleset(double profitSurplus, int seatsTaken, Ruleset ruleset = Ruleset.Default, Plane _aircraft = null)
        {
            //check if looking for alternative aircraft requirements
            _aircraft = _aircraft != null ? _aircraft : Aircraft;
            bool profitable = profitSurplus > 0; //the revenue generated from the flight must exceed the cost of the flight 
            bool seatsAvailable = seatsTaken < _aircraft.NumberOfSeats; //the number of passengers cannot exceed the amount of seats on the plane
            bool minimumPassengersReached = seatsTaken / (double)_aircraft.NumberOfSeats > FlightRoute.MinimumTakeOffPercentage; //the aircraft must have a minimum percentage of passengers booked for that route

            bool result = false;
            //They have indicated they might want more rule sets in the future.
            switch (ruleset)
            {
                case Ruleset.Relaxed:
                    {
                        bool relaxedEmployeeOverfill = Passengers.Count(p => p is AirlineEmployee) / (double)_aircraft.NumberOfSeats > FlightRoute.MinimumTakeOffPercentage;
                        //(if the number of airline employees aboard is greater than the minimum percentage of passengers required, then the revenue generated doesn’t need to exceed cost)
                        result = seatsAvailable && minimumPassengersReached && (profitable || relaxedEmployeeOverfill);
                        break;
                    }
                default:
                    {
                        result = seatsAvailable && minimumPassengersReached && profitable;
                        break;
                    }
            }

            return result;
        }
    }
}
