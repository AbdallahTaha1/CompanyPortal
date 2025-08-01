using CompanyPortal.DTOs.Auth;

namespace CompanyPortal.Services.Abstractions
{
    public interface IOtpServcie
    {
        Task<bool> VerifyOtpAsync(VerifyOtpDto dto);
        Task<string> GetOtpAsync(string email);

    }
}
