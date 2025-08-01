using CompanyPortal.Consts;

namespace CompanyPortal.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public Company? Company { get; set; }

        public UserRole Role { get; set; } = default!;
    }
}
