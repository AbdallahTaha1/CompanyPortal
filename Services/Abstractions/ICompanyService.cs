using CompanyPortal.DTOs.Company;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Abstractions
{
    public interface ICompanyService
    {
        Task<Result<CompanyDto>> GetCompanyAsync(string userId);
    }
}