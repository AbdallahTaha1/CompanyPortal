using AutoMapper;
using CompanyPortal.DTOs.Company;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Services.Abstractions;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Implementaion
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CompanyDto>> GetCompanyAsync(string userId)
        {
            var company = await _unitOfWork.Companies.FindAsync(c => c.UserId.ToString() == userId);
            if (company == null)
            {
                return Result<CompanyDto>.Fail("Company not found");
            }

            var companyDto = _mapper.Map<CompanyDto>(company);

            return Result<CompanyDto>.Ok(companyDto);
        }


    }
}
