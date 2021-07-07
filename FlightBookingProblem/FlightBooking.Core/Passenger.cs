namespace FlightBooking.Core
{
    public abstract class Passenger
    {
        //properties
        public string name { get; set; }
        public int age { get; set; } // -1 age indicates none was given
        public int allowedBags { get; protected set;}

        //constructors
        public Passenger(string name = "N/A", int age = -1)
        {
            this.name = name;
            this.age = age;
            this.allowedBags = 1;
        }
    }

    public class General : Passenger
    {
        //constructors
        public General(string name, int age) : base(name, age) { }
        public General() : base() { }
    }

    public class Loyalty : Passenger
    {
        //properties
        public int loyaltyPoints { get; set; }
        public bool isUsingLoyaltyPoints { get; set; }

        //constructors
        public Loyalty(string name, int age, int loyaltyPoints = 0, bool isUsingLoyaltyPoints = false) : base(name, age)
        {
            this.loyaltyPoints = loyaltyPoints;
            this.isUsingLoyaltyPoints = isUsingLoyaltyPoints;
        }
        public Loyalty() : base() {
            base.allowedBags = 2; //loyalty passengers are allowed an additional bag
        }
    }

    public class AirlineEmployee : Passenger
    {
        //constructors
        public AirlineEmployee(string name, int age) : base(name, age) { }
        public AirlineEmployee() : base() { }
    }

    public class Discounted : Passenger
    {
        //constructors
        public Discounted(string name, int age) : base(name, age) { }
        public Discounted() : base() {
            base.allowedBags = 0; //discounted passengers are not allowed any bags
        }
    }
}
