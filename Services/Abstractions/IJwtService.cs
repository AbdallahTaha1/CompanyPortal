using CompanyPortal.Data.Entities;

namespace CompanyPortal.Services.Abstractions
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
