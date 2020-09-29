using System.IO;
using Microsoft.AspNetCore.Http;

namespace DentalScheduler.Shared.Helpers.Extensions
{
    public static class HelpersExtensions
    {
        public static byte[] ToArray(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }

        public static byte[] ToArray(this IFormFile formFile)
            => formFile.OpenReadStream().ToArray();
    }
}