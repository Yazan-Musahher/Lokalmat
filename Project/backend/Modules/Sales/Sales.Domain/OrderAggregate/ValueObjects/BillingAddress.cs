using Sales.Domain.Models;

namespace Sales.Domain.OrderAggregate.ValueObjects
{
    public sealed class BillingAddress : ValueObject
    {
        public string Name { get; private set; }
        public string StreetName { get; private set; }
        public string StreetNumber { get; private set; } 
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string AdditionalDetails { get; private set; }

        private BillingAddress(
            string name,
            string streetName, 
            string streetNumber, 
            string postalCode, 
            string city, 
            string additionalDetails)
        {
            Name = name;
            StreetName = streetName;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            AdditionalDetails = additionalDetails;
        }

        public static BillingAddress Create(
            string name,
            string streetName, 
            string streetNumber, 
            string postalCode, 
            string city, 
            string additionalDetails)
        {
            return new BillingAddress(name, streetName, streetNumber, postalCode, city, additionalDetails);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return StreetName;
            yield return StreetNumber;
            yield return PostalCode;
            yield return City;
            yield return AdditionalDetails;
        }
        
    }
}
