

namespace CoreFitness.Infrastrcuture.Models;

//Domain model/ Aggregate for Membership. This model includes properties such as Title, Description, Benefits, Price, and MonthlyClasses. It also includes validation logic to ensure that required fields are provided and that the price is not negative. The Create method allows for easy instantiation of new Membership objects, while the Rehydrate method can be used to reconstruct a Membership from existing data.
public sealed class Membership
{

    //private constructor
    private Membership(string id, string title, string description, List<string> benefits, decimal price, int monthlyClasses)
    {
        Id = Required(id, nameof(id));
        Title = Required(title, nameof(title));
        Description = Required(description, nameof(description));
        Benefits = benefits;
        Price = CheckPrice(price, nameof(price));
        MonthlyClasses = monthlyClasses;
    }

        public string Id { get; }
        public string Title { get; private set; } 
        public string Description { get; private set; }
        public List<string> Benefits { get; private set; }
        public decimal Price { get; private set; } 

        public int MonthlyClasses { get; private set; }

    //validation methods
    private static string Required (string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{propertyName} must be provided ");

        return value.Trim();
    }

    private static decimal CheckPrice (decimal value, string propertyName)
    {
        if (value < 0)
            throw new ArgumentException($"{propertyName} can not be a negative value");

        return value;
    }

    // create and rehydrate 

    public static Membership Create(string title, string description, List<string> benefits, decimal price = 0, int monthlyClasses = 20) =>
        new(Guid.NewGuid().ToString(), title, description, benefits, price, monthlyClasses);



    public static Membership Rehydrate(string id, string title, string description, List<string> benefits, decimal price = 0, int monthlyClasses = 20) =>
       new(id, title, description, benefits, price, monthlyClasses);

}
