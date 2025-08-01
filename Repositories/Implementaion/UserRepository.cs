using CompanyPortal.Data;
using CompanyPortal.Data.Entities;
using CompanyPortal.Repositories.Abstractions;

namespace CompanyPortal.Repositories.Implementaion
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        // Additional methods specific to UserRepository can be added here
    }

}
