using CompanyPortal.DTOs.Auth;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Services.Abstractions;

namespace CompanyPortal.Services.Implementaion
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public LoginService(IUnitOfWork unitOfWork, IPasswordService passwordService, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _jwtService = jwtService;
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
            if (user.PasswordHash is null || _passwordService.VerifyPassword(user.PasswordHash, loginDto.Password))
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
    }
}
