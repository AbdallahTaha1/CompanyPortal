using CompanyPortal.DTOs.Auth;

namespace CompanyPortal.Services.Abstractions
{
    public interface ICompanySignUpService
    {
        Task SignUpAsync(CompanySignUpDto companySignUpDto);
    }
}
