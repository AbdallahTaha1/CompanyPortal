namespace CompanyPortal.DTOs.Auth
{
    public class AuthResultDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string JWTToken { get; set; } = string.Empty;
        public DateTime JWTTokenExpiresOn { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
    }
}
