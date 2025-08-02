using CompanyPortal.Data.Entities;
using CompanyPortal.DTOs.Auth;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Services.Abstractions;
using CompanyPortal.Shared;
using Microsoft.AspNetCore.Identity;

namespace CompanyPortal.Services.Implementaion
{
    public class PasswordService : IPasswordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher<User> _passwordHasher;
        public PasswordService(IUnitOfWork unitOfWork, PasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result> SetPasswordAsync(SetPasswordDto dto)
        {
            var user = _unitOfWork.Users.Find(u => u.Email == dto.Email);
            if (user == null)
            {
                return Result.Fail("User not found");
            }

            if (!user.IsVerified)
            {
                return Result.Fail("User is not verified");
            }

            // hash the password
            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);

            // set the password
            user.PasswordHash = hashedPassword;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok("Password set successfully");
        }

        public bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
