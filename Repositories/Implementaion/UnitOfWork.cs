using CompanyPortal.Data;
using CompanyPortal.Repositories.Abstractions;

namespace CompanyPortal.Repositories.Implementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _context = context;
            Users = userRepository;
            Companies = companyRepository;
        }

        public IUserRepository Users { get; }
        public ICompanyRepository Companies { get; }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose() =>
            _context.Dispose();
    }

}
