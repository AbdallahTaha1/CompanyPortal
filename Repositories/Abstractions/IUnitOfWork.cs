namespace CompanyPortal.Repositories.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICompanyRepository Companies { get; }
        Task<int> SaveChangesAsync();
    }
}
