using CompanyPortal.Data;
using CompanyPortal.Data.Entities;
using CompanyPortal.Repositories.Abstractions;

namespace CompanyPortal.Repositories.Implementaion
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

    }
}
