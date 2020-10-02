using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DentalScheduler.Common.Helpers.Extensions
{
    public static class IOExtensions
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

        public static string ToBase64String(this byte[] content)
            => Convert.ToBase64String(content);
    }
}