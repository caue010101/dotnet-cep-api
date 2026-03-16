
namespace CepSystem.Domain.Entities
{

    public class Address
    {
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string? Complement { get; private set; }
        public string? Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string? AreaCode { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Address() { }

        public Address(string zipCode, string street, string? complement, string? neighborhood, string city, string state,
            string? areaCode)
        {

            if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentException("zipCode is required ");
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("street is required ");
            if (string.IsNullOrWhiteSpace(complement)) throw new ArgumentException("complement is required ");
            if (string.IsNullOrWhiteSpace(neighborhood)) throw new ArgumentException("neighborhood is required ");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("city is required ");
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("state is required ");
            if (string.IsNullOrWhiteSpace(areaCode)) throw new ArgumentException("areaCode is required ");


            ZipCode = zipCode.Replace("-", "").Trim();
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            AreaCode = areaCode;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
