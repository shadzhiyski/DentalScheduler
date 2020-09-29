using System.Linq;
using FluentValidation;

namespace DentalScheduler.UseCases.Common.Validation
{
    public class ImageValidator : AbstractValidator<byte[]>
    {
        public ImageValidator()
        {
            RuleFor(model => model)
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