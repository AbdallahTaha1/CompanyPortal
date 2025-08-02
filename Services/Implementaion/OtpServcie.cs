using CompanyPortal.DTOs.Auth;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Services.Abstractions;
using CompanyPortal.Shared;

namespace CompanyPortal.Services.Implementaion
{
    public class OtpServcie : IOtpServcie
    {
        private readonly IUnitOfWork _unitOfWork;
        public OtpServcie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> GetOtpAsync(string email)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Email == email);
            if (user == null)
            {
                return Result<string>.Fail("User not found");
            }

            return Result<string>.Ok(user.OtpCode!);
        }

        public async Task<Result> VerifyOtpAsync(VerifyOtpDto verifyOtpDto)
        {
            var user = _unitOfWork.Users.Find(u => u.Email == verifyOtpDto.Email);
            if (user == null)
            {
                return Result.Fail("User not found");
            }

            if (user.OtpExpiresAt == null || DateTime.UtcNow > user.OtpExpiresAt)
            {
                return Result.Fail("OTP has expired. Please request a new OTP.");
            }

            if (user.OtpCode == verifyOtpDto.Otp)
            {
                user.IsVerified = true;
                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok("User is verified");
            }
            else
            {
                return Result.Fail("The OTP entered is not correct");
            }
        }
    }
}
