using CompanyPortal.DTOs.Auth;

namespace CompanyPortal.Services.Abstractions
{
    public interface IPasswordService
    {
        Task SetPasswordAsync(SetPasswordDto dto);
    }
}
