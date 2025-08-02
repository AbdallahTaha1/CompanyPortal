using CompanyPortal.DTOs.Auth;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResultDto> Login(LoginDto loginDto);
        Task<Result> SignUpAsync(CompanySignUpDto companySignUpDto);
    }
}
