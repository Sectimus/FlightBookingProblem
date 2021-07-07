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

    public class General : Passenger
    {
        //constructors
        public General(string name, int age) : base(name, age) { }
        public General() : base() { }
    }

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
            base.AllowedBags = 0; //discounted passengers are not allowed any bags
        }
    }
}
