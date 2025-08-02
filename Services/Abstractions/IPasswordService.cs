using CompanyPortal.DTOs.Auth;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Abstractions
{
    public interface IPasswordService
    {
        Task<Result> SetPasswordAsync(SetPasswordDto dto);
        bool VerifyPassword(string hashedPassword, string inputPassword);
    }
}
