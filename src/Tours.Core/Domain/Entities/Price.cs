using System.Text.Json.Serialization;

namespace Tours.Core.Domain.Entities;
public class Price : ValueObject
{
    public double Amount { get; }

    [JsonConstructor]
    public Price(double amount)
    {
        Amount = amount;
    }

    public Price Add(Price price)
    {
        return new Price(price.Amount + Amount);
    }

    public Price Remove(Price price)
    {
        return new Price(Amount - price.Amount);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }

}

