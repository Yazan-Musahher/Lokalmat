using Sales.Domain.Models;

namespace Sales.Domain.OrderAggregate.ValueObjects;


public sealed class ShippingAddress : ValueObject
{
    public string StreetName { get; private set; }
    public string StreetNumber { get; private set; } 
    public string PostalCode { get; private set; }
    public string City { get; private set; }
    public string AdditionalDetails { get; private set; }

    private ShippingAddress(string streetName, string streetNumber, string postalCode, string city, string additionalDetails)
    {
        StreetName = streetName;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
        AdditionalDetails = additionalDetails;
    }

    public static ShippingAddress Create(
        string streetName, 
        string streetNumber, 
        string postalCode, 
        string city, 
        string additionalDetails)
    {
        return new ShippingAddress(streetName, streetNumber, postalCode, city, additionalDetails);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return StreetName;
        yield return StreetNumber;
        yield return PostalCode;
        yield return City;
        yield return AdditionalDetails;
    }
    
}
