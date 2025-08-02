using CompanyPortal.DTOs.Auth;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Abstractions
{
    public interface ICompanySignUpService
    {
        Task<Result> SignUpAsync(CompanySignUpDto companySignUpDto);
    }
}
