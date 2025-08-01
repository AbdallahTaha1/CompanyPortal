using CompanyPortal.DTOs.Auth;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Services.Abstractions;

namespace CompanyPortal.Services.Implementaion
{
    public class OtpServcie : IOtpServcie
    {
        private readonly IUnitOfWork _unitOfWork;
        public OtpServcie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> GetOtpAsync(string email)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Email == email);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            return user.OtpCode!;
        }

        public async Task<bool> VerifyOtpAsync(VerifyOtpDto verifyOtpDto)
        {
            var user = _unitOfWork.Users.Find(u => u.Email == verifyOtpDto.Email);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            if (user.OtpCode == verifyOtpDto.Otp)
            {
                user.IsVerified = true;
                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
