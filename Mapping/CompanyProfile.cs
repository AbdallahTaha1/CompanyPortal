using AutoMapper;
using CompanyPortal.Data.Entities;
using CompanyPortal.DTOs.Company;

namespace CompanyPortal.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
        }
    }

}
