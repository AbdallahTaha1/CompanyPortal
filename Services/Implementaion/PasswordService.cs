using CompanyPortal.DTOs.Auth;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace CompanyPortal.Services.Implementaion
{
    public class PasswordService : IPasswordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher<object> _passwordHasher;
        public PasswordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task SetPasswordAsync(SetPasswordDto dto)
        {
            var user = _unitOfWork.Users.Find(u => u.Email == dto.Email);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            if (!user.IsVerified)
            {
                throw new InvalidOperationException("User is not verified");
            }

            // hash the password
            var hashedPassword = _passwordHasher.HashPassword(null, dto.Password);

            // set the password
            user.PasswordHash = hashedPassword;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
