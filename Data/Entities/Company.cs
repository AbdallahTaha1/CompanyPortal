namespace CompanyPortal.Data.Entities
{
    public class Company
    {
        public Guid Id { get; set; }

        public string ArabicName { get; set; } = string.Empty;
        public string EnglishName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LogoUrl { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
