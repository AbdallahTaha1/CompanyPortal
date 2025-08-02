using CompanyPortal.Consts;
using CompanyPortal.Data.Entities;
using CompanyPortal.DTOs.Auth;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Services.Abstractions;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Implementaion
{
    public class CompanySignUpService : ICompanySignUpService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanySignUpService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> SignUpAsync(CompanySignUpDto companySignUpDto)
        {
            // 1. Validate email not already registered
            var existingUser = await _unitOfWork.Users.FindAsync(c => c.Email == companySignUpDto.Email);
            if (existingUser is not null)
                return Result.Fail("Email is already registered.");

            // 2. Create new user
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = companySignUpDto.Email,
                Role = UserRole.Company,
                OtpCode = GenerateOtp(),
                OtpExpiresAt = DateTime.UtcNow.AddDays(1),
                IsVerified = false
            };

            await _unitOfWork.Users.AddAsync(newUser);

            // 3. Create new company
            var newCompany = new Company
            {
                Id = Guid.NewGuid(),
                ArabicName = companySignUpDto.ArabicName,
                EnglishName = companySignUpDto.EnglishName,
                PhoneNumber = companySignUpDto.PhoneNumber,
                LogoUrl = companySignUpDto.LogoUrl,
                WebsiteUrl = companySignUpDto.WebsiteUrl,
                UserId = newUser.Id
            };

            await _unitOfWork.Companies.AddAsync(newCompany);

            // 4. Save changes
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok("Company registered successfully. An OTP has been sent to your email for verification.");

        }

        private string GenerateOtp()
        {
            // Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();
            return otp;
        }
    }
}
