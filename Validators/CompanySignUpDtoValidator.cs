using CompanyPortal.DTOs.Auth;
using FluentValidation;

namespace CompanyPortal.Validators
{
    public class CompanySignUpDtoValidator : AbstractValidator<CompanySignUpDto>
    {
        public CompanySignUpDtoValidator()
        {
            RuleFor(x => x.ArabicName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(200).WithMessage("Company Arabic name cannot exceed 200 characters.");

            RuleFor(x => x.EnglishName)
                .NotEmpty().WithMessage("Company English Name is required.")
                .MaximumLength(100).WithMessage("Company English name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.")
                .Matches(@"^\+?[0-9\s\-()]+$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.WebsiteUrl)
                .MaximumLength(300).WithMessage("Website URL cannot exceed 300 characters.");

            RuleFor(x => x.Logo)
               .Must(IsValidImageFile!)
               .WithMessage("Logo must be a valid image file (JPEG or PNG).")
               .Must(IsValidImageSize!)
               .WithMessage("Logo file size must not exceed 1 MB.");
        }
        private bool IsValidImageFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return true;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return allowedExtensions.Contains(extension);
        }
        private bool IsValidImageSize(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return true;

            const int maxFileSizeInBytes = 1 * 1024 * 1024; // 1 MB
            return file.Length <= maxFileSizeInBytes;
        }

    }

}