namespace FlightBooking.Core
{
    public abstract class Passenger
    {
        //properties
        public string Name { get; set; }
        public int Age { get; set; } // -1 age indicates none was given
        public int AllowedBags { get; protected set;}

        //constructors
        public Passenger(string name = "N/A", int age = -1)
        {
            this.Name = name;
            this.Age = age;
            this.AllowedBags = 1;
        }
    }

    /// <summary>
    /// General - Normal fare paying passengers.
    /// </summary>
    public class General : Passenger
    {
        //constructors
        public General(string name, int age) : base(name, age) { }
        public General() : base() { }
    }

    /// <summary>
    /// Loyalty Members – Repeat customers who get benefits for choosing to fly with the airline.
    /// </summary>
    public class Loyalty : Passenger
    {
        //properties
        public int LoyaltyPoints { get; set; }
        public bool IsUsingLoyaltyPoints { get; set; }

        //constructors
        public Loyalty(string name, int age, int loyaltyPoints = 0, bool isUsingLoyaltyPoints = false) : base(name, age)
        {
            this.LoyaltyPoints = loyaltyPoints;
            this.IsUsingLoyaltyPoints = isUsingLoyaltyPoints;
        }
        public Loyalty() : base() {
            base.AllowedBags = 2; //loyalty passengers are allowed an additional bag
        }
    }

    /// <summary>
    /// Airline Employee – Employees of the airline who fly for free as a perk.
    /// </summary>
    public class AirlineEmployee : Passenger
    {
        //constructors
        public AirlineEmployee(string name, int age) : base(name, age) { }
        public AirlineEmployee() : base() { }
    }

    /// <summary>
    /// Discounted - General Passengers that don't accrue loyalty points and are not allowed a bag; however are only charged half the normal rate.
    /// </summary>
    public class Discounted : Passenger
    {
        //constructors
        public Discounted(string name, int age) : base(name, age) { }
        public Discounted() : base() {
            base.AllowedBags = 0; //discounted passengers are not allowed any bags
        }
    }
}
