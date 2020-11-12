using System;
using System.IO;
using System.Threading.Tasks;
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

        public static async Task<byte[]> ToArrayAsync(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);

                return await Task.FromResult(memoryStream.ToArray());
            }
        }

        public static byte[] ToArray(this IFormFile formFile)
            => formFile.OpenReadStream().ToArray();

        public static string ToBase64String(this byte[] content)
            => Convert.ToBase64String(content);

        public static async Task<MemoryStream> ToMemoryStreamAsync(this Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return await Task.FromResult(memoryStream);
        }
    }
}