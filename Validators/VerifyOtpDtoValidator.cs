using CompanyPortal.DTOs.Auth;
using FluentValidation;

namespace CompanyPortal.Validators
{
    public class VerifyOtpDtoValidator : AbstractValidator<VerifyOtpDto>
    {
        public VerifyOtpDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP is required.")
                .Length(6).WithMessage("OTP must be exactly 6 characters long.")
                .Matches(@"^\d+$").WithMessage("OTP must contain only digits.");
        }
    }
}
