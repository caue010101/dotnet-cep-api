
namespace CepSystem.Domain.Entities
{

    public class Address
    {
        public string ZipCode { get; init; }
        public string Street { get; init; }
        public string? Neighborhood { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string? AreaCode { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public Address() { }

        public Address(string zipCode, string street, string? neighborhood, string city, string state,
            string? areaCode)
        {

            if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentException("zipCode is required ");
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("street is required ");
            if (string.IsNullOrWhiteSpace(neighborhood)) throw new ArgumentException("neighborhood is required ");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("city is required ");
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("state is required ");
            if (string.IsNullOrWhiteSpace(areaCode)) throw new ArgumentException("areaCode is required ");


            ZipCode = zipCode.Replace("-", "").Trim();
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            AreaCode = areaCode;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
