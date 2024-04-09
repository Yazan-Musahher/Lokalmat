using Sales.Domain.Models;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.ProductAggregate.Entities;

public sealed class Price : Entity<PriceId>
{
    public decimal Value { get; private set; }

    public string? Currency { get; private set; }

    private Price(PriceId priceId, decimal value, string currency) :
        base(priceId)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Price value must be greater than 0", nameof(value));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be empty", nameof(currency));
        }

        Value = value;
        Currency = currency;
    }

    public static Price Create(
        decimal value,
        string currency)
    {
        return new(
            PriceId.CreateUnique(),
            value,
            currency);
    }

}
