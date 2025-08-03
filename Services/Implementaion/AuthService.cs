using CompanyPortal.Consts;
using CompanyPortal.Data.Entities;
using CompanyPortal.DTOs.Auth;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Services.Abstractions;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Implementaion
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IImageService _imageService;
        public AuthService(IUnitOfWork unitOfWork, IPasswordService passwordService, IJwtService jwtService, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _imageService = imageService;
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
            string? logoUrl = null;
            if (companySignUpDto.Logo != null)
            {
                // Save the logo image and get the URL
                var Url = await _imageService.SaveImageAsync(companySignUpDto.Logo, "Uploads/logos");
                if (string.IsNullOrEmpty(Url))
                {
                    return Result.Fail("Failed to upload logo image.");
                }
                logoUrl = Url;
            }
            var newCompany = new Company
            {
                Id = Guid.NewGuid(),
                ArabicName = companySignUpDto.ArabicName,
                EnglishName = companySignUpDto.EnglishName,
                PhoneNumber = companySignUpDto.PhoneNumber,
                LogoUrl = logoUrl,
                WebsiteUrl = companySignUpDto.WebsiteUrl,
                UserId = newUser.Id
            };

            await _unitOfWork.Companies.AddAsync(newCompany);

            // 4. Save changes
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok("Company registered successfully. An OTP has been sent to your email for verification.");

        }

        public async Task<AuthResultDto> Login(LoginDto loginDto)
        {
            // 1. chekc if the email is registered
            var user = await _unitOfWork.Users.FindAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return new AuthResultDto
                {
                    IsAuthenticated = false,
                    Message = "Email is not registered."
                };
            }
            // 2. check if the user is verified
            if (!user.IsVerified)
            {
                return new AuthResultDto
                {
                    IsAuthenticated = false,
                    Message = "User is not verified. Please verify your account."
                };
            }
            // 3. check if the password is correct
            if (string.IsNullOrEmpty(user.PasswordHash) || !_passwordService.VerifyPassword(user.PasswordHash, loginDto.Password))
            {
                return new AuthResultDto
                {
                    IsAuthenticated = false,
                    Message = "Invalid password."
                };
            }

            // 4. generate a JWT token
            var token = _jwtService.GenerateToken(user);

            // 5. return the token and user info
            return new AuthResultDto
            {
                IsAuthenticated = true,
                JWTToken = token,
                UserId = user.Id.ToString(),
                Email = user.Email,
                Role = user.Role.ToString(),
            };

        }

        private string GenerateOtp()
        {
            // Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();
            return otp;
        }
    }
}
