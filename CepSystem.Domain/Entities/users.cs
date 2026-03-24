
namespace CepSystem.Domain.Entities
{

    public class User
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public User(Guid id, string name, string email, string passwordHash)
        {

            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required ");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required ");
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("Password is required ");

            id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;

        }
    }
}
