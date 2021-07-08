using FlightBooking.Core;
using System;
using Xunit;
using System.Collections.Generic;

namespace FlightBooking.Test
{
    public class Flights
    {
        private FlightRoute londonToParis = new FlightRoute("London", "Paris")
        {
            BaseCost = 50,
            BasePrice = 100,
            LoyaltyPointsGained = 5,
            MinimumTakeOffPercentage = 0.7
        };

        private Dictionary<string, Plane> planes = new Dictionary<string, Plane>();

        public Flights()
        {
            Plane antonov = new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12 };
            planes.Add("antonov", antonov);
            Plane dornier = new Plane { Id = 456, Name = "Dornier 328", NumberOfSeats = 24 };
            planes.Add("dornier", dornier);
            Plane dash = new Plane { Id = 789, Name = "Dash 8-100", NumberOfSeats = 37 };
            planes.Add("dash", dash);
            Plane noseats = new Plane { Id = 789, Name = "Noseat BD48", NumberOfSeats = 0 };
            planes.Add("noseats", noseats);
        }

        [Fact]
        //a test flight with no alerternative planes that will *not* be able to carry a single passenger.
        public void CantFlyTooLittle()
        {

            ScheduledFlight _scheduledFlight = new ScheduledFlight(londonToParis);
            _scheduledFlight.SetAircraftForRoute(planes["antonov"]);

            Passenger p = new General
            {
                Name = "John",
                Age = Convert.ToInt32("32")
            };

            _scheduledFlight.AddPassenger(p);

            Assert.DoesNotContain("THIS FLIGHT MAY PROCEED", _scheduledFlight.GetSummary());
        }

        [Fact]
        //a test flight with no alerternative planes that will *not* be able to accomodate all passengers.
        public void CantFlyTooMany()
        {

            ScheduledFlight _scheduledFlight = new ScheduledFlight(londonToParis);
            _scheduledFlight.SetAircraftForRoute(planes["antonov"]);

            Passenger p = new General { Name = "John", Age = Convert.ToInt32("32") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Mary", Age = Convert.ToInt32("14") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Matthew", Age = Convert.ToInt32("27") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Joseph", Age = Convert.ToInt32("53") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Nicholas", Age = Convert.ToInt32("35") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Melissa", Age = Convert.ToInt32("22") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Steven", Age = Convert.ToInt32("36") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Kevin", Age = Convert.ToInt32("65") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Laura", Age = Convert.ToInt32("6") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Lauren", Age = Convert.ToInt32("16") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Crystal", Age = Convert.ToInt32("37") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Mark", Age = Convert.ToInt32("54") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Charles", Age = Convert.ToInt32("74") };
            _scheduledFlight.AddPassenger(p);

            Assert.DoesNotContain("THIS FLIGHT MAY PROCEED", _scheduledFlight.GetSummary());
        }

        //todo
        [Fact]
        //a flight can't take off if the cost surplus < 0 (no real profit)
        public void CostsTooMuch()
        {
            ScheduledFlight _scheduledFlight = new ScheduledFlight(londonToParis);
            _scheduledFlight.SetAircraftForRoute(planes["antonov"]);

            Passenger p = new Discounted { Name = "Mary", Age = Convert.ToInt32("14") };
            _scheduledFlight.AddPassenger(p);
            p = new Discounted { Name = "Matthew", Age = Convert.ToInt32("27") };
            _scheduledFlight.AddPassenger(p);
            p = new Discounted { Name = "Joseph", Age = Convert.ToInt32("53") };
            _scheduledFlight.AddPassenger(p);
            p = new Discounted { Name = "Nicholas", Age = Convert.ToInt32("35") };
            _scheduledFlight.AddPassenger(p);
            p = new Discounted { Name = "Melissa", Age = Convert.ToInt32("22") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Steven", Age = Convert.ToInt32("36") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Kevin", Age = Convert.ToInt32("65") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Laura", Age = Convert.ToInt32("6") };
            _scheduledFlight.AddPassenger(p);
            p = new Discounted { Name = "Lauren", Age = Convert.ToInt32("16") };
            _scheduledFlight.AddPassenger(p);
            p = new Discounted { Name = "Crystal", Age = Convert.ToInt32("37") };
            _scheduledFlight.AddPassenger(p);

            Assert.DoesNotContain("THIS FLIGHT MAY PROCEED", _scheduledFlight.GetSummary());
        }

        [Fact]
        //an average flight that should be able to depart
        public void DefaultSuccessfulFlight()
        {
            ScheduledFlight _scheduledFlight = new ScheduledFlight(londonToParis);
            _scheduledFlight.SetAircraftForRoute(planes["antonov"]);

            Passenger p = new Discounted { Name = "Mary", Age = Convert.ToInt32("14") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Matthew", Age = Convert.ToInt32("27") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Joseph", Age = Convert.ToInt32("53") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Nicholas", Age = Convert.ToInt32("35") };
            _scheduledFlight.AddPassenger(p);
            p = new Discounted { Name = "Melissa", Age = Convert.ToInt32("22") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Steven", Age = Convert.ToInt32("36") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Kevin", Age = Convert.ToInt32("65") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Laura", Age = Convert.ToInt32("6") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Lauren", Age = Convert.ToInt32("16") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Crystal", Age = Convert.ToInt32("37") };
            _scheduledFlight.AddPassenger(p);

            Assert.Contains("THIS FLIGHT MAY PROCEED", _scheduledFlight.GetSummary());
        }

        [Fact]
        //a flight full of airlineemployees with relaxed rules
        public void RelaxedSuccessfulFlight()
        {
            ScheduledFlight _scheduledFlight = new ScheduledFlight(londonToParis, ScheduledFlight.Ruleset.Relaxed);
            _scheduledFlight.SetAircraftForRoute(planes["antonov"]);

            Passenger p = new AirlineEmployee { Name = "Mary", Age = Convert.ToInt32("14") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Matthew", Age = Convert.ToInt32("27") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Joseph", Age = Convert.ToInt32("53") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Nicholas", Age = Convert.ToInt32("35") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Melissa", Age = Convert.ToInt32("22") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Steven", Age = Convert.ToInt32("36") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Kevin", Age = Convert.ToInt32("65") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Laura", Age = Convert.ToInt32("6") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Lauren", Age = Convert.ToInt32("16") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Crystal", Age = Convert.ToInt32("37") };
            _scheduledFlight.AddPassenger(p);

            Assert.Contains("THIS FLIGHT MAY PROCEED", _scheduledFlight.GetSummary());
        }
        
        [Fact]
        //flight with 0 seats will never depart
        public void ImpossibleSeatingFlight()
        {
            ScheduledFlight _scheduledFlight = new ScheduledFlight(londonToParis);
            _scheduledFlight.SetAircraftForRoute(planes["noseats"]);

            Passenger p = new Discounted { Name = "Mary", Age = Convert.ToInt32("14") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Matthew", Age = Convert.ToInt32("27") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Joseph", Age = Convert.ToInt32("53") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Nicholas", Age = Convert.ToInt32("35") };
            _scheduledFlight.AddPassenger(p);
            p = new Discounted { Name = "Melissa", Age = Convert.ToInt32("22") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Steven", Age = Convert.ToInt32("36") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Kevin", Age = Convert.ToInt32("65") };
            _scheduledFlight.AddPassenger(p);
            p = new AirlineEmployee { Name = "Laura", Age = Convert.ToInt32("6") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Lauren", Age = Convert.ToInt32("16") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Crystal", Age = Convert.ToInt32("37") };
            _scheduledFlight.AddPassenger(p);

            Assert.DoesNotContain("THIS FLIGHT MAY PROCEED", _scheduledFlight.GetSummary());
        }

        [Fact]
        //flight wil use an alternative if not possible to depart
        public void AlternativeAircraft()
        {
            ScheduledFlight _scheduledFlight = new ScheduledFlight(londonToParis);
            _scheduledFlight.SetAircraftForRoute(planes["antonov"], new Plane[] { planes["dornier"], planes["dash"] });

            Passenger p = new General { Name = "John", Age = Convert.ToInt32("32") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Mary", Age = Convert.ToInt32("14") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Matthew", Age = Convert.ToInt32("27") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Joseph", Age = Convert.ToInt32("53") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Nicholas", Age = Convert.ToInt32("35") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Melissa", Age = Convert.ToInt32("22") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Steven", Age = Convert.ToInt32("36") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Kevin", Age = Convert.ToInt32("65") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Laura", Age = Convert.ToInt32("6") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Lauren", Age = Convert.ToInt32("16") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Crystal", Age = Convert.ToInt32("37") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Mark", Age = Convert.ToInt32("54") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Charles", Age = Convert.ToInt32("74") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Melissa", Age = Convert.ToInt32("22") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Steven", Age = Convert.ToInt32("36") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Kevin", Age = Convert.ToInt32("65") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Laura", Age = Convert.ToInt32("6") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Lauren", Age = Convert.ToInt32("16") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Crystal", Age = Convert.ToInt32("37") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Mark", Age = Convert.ToInt32("54") };
            _scheduledFlight.AddPassenger(p);
            p = new General { Name = "Charles", Age = Convert.ToInt32("74") };
            _scheduledFlight.AddPassenger(p);


            string summary = _scheduledFlight.GetSummary();
            Assert.DoesNotContain("THIS FLIGHT MAY PROCEED", summary);
            Assert.Contains("Dornier 328", summary);
            Assert.DoesNotContain("Dash 8-100", summary);
        }
    }
}
