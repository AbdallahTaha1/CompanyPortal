using CompanyPortal.DTOs.Auth;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Abstractions
{
    public interface IOtpServcie
    {
        Task<Result<string>> GetOtpAsync(string email);
        Task<Result> VerifyOtpAsync(VerifyOtpDto verifyOtpDto);
    }
}
