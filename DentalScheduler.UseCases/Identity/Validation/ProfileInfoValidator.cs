using System.Linq;
using DentalScheduler.Interfaces.Models.Input;
using FluentValidation;

namespace DentalScheduler.UseCases.Identity.Validation
{
    public class ProfileInfoValidator : AbstractValidator<IProfileInfoInput>
    {
        public ProfileInfoValidator()
        {
            RuleFor(model => model.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
            
            RuleFor(model => model.Avatar)
                .Must(IsImage)
                .WithMessage("Invalid image format.");
        }

        private static bool IsImage(byte[] content)
            => content == null || content.Length == 0
                ? true
                : !string.Empty.Equals(GetImageType(content));

        private static string GetImageType(byte[] content)
        {
            string headerCode = GetHeaderInfo(content).ToUpper();
            
            if (headerCode.StartsWith("FFD8FFE0"))
            {
                return "JPG";
            }
            else if (headerCode.StartsWith("49492A"))
            {
                return "TIFF";
            }
            else if (headerCode.StartsWith("424D"))
            {
                return "BMP";
            }
            else if (headerCode.StartsWith("474946"))
            {
                return "GIF";
            }
            else if (headerCode.StartsWith("89504E470D0A1A0A"))
            {
                return "PNG";
            }
            else
            {
                return string.Empty;
            }
        }

        private static string GetHeaderInfo(byte[] content)
            => string.Join(
                    string.Empty,
                    content[0 .. 8].Select(b => b.ToString("X2"))
                );
    }
}