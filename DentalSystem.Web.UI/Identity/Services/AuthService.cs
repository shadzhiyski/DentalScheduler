using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Common.Dto.Output;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.UseCases.Common.Validation;

namespace DentalSystem.Web.UI.Identity.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        public async Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput input)
        {
            var response = await HttpClient.PostAsJsonAsync<IUserCredentialsInput>("api/Auth/login", input);

            var responseStatusCode = response.StatusCode;
            if (responseStatusCode == HttpStatusCode.BadRequest)
            {
                var errors = await response.Content.ReadFromJsonAsync<IEnumerable<ValidationError>>();

                return new Result<IAccessTokenOutput>(errors);
            }

            var accessToken = await response.Content.ReadFromJsonAsync<AccessTokenOutput>();

            return new Result<IAccessTokenOutput>(accessToken);
        }

        public Task<IResult<IAccessTokenOutput>> RefreshTokenAsync(IUserCredentialsInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task<IResult<IAccessTokenOutput>> RegisterUserAsync(IUserCredentialsInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}