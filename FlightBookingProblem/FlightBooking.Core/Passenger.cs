namespace FlightBooking.Core
{
    public class Passenger
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int AllowedBags { get; set; }
    }

    public class General : Passenger
    {

    }

    public class Loyalty : Passenger
    {
        public int LoyaltyPoints { get; set; }
        public bool IsUsingLoyaltyPoints { get; set; }
    }

    public class AirlineEmployee : Passenger
    {

    }
}
