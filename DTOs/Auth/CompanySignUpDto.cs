namespace CompanyPortal.DTOs.Auth
{
    public class CompanySignUpDto
    {
        public string ArabicName { get; set; } = string.Empty;
        public string EnglishName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }
        public IFormFile? Logo { get; set; }
    }
}
