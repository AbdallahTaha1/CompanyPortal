using CompanyPortal.DTOs.Auth;

namespace CompanyPortal.Services.Abstractions
{
    public interface ILoginService
    {
        Task<AuthResultDto> Login(LoginDto loginDto);
    }
}
