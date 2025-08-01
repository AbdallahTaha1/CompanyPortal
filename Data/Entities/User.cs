namespace CompanyPortal.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        // Hashed password - can be null until OTP is verified and password is set
        public string? PasswordHash { get; set; } = string.Empty;

        // OTP
        public string? OtpCode { get; set; }
        public DateTime? OtpGeneratedAt { get; set; }
        public bool IsVerified { get; set; } = false;

        public Company? Company { get; set; }

        public string Role { get; set; } = string.Empty;
    }
}
