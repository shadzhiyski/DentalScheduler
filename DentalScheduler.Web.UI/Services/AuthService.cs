using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.UseCases.Common.Validation;

namespace DentalScheduler.Web.UI.Services
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

            var accessToken = await response.Content.ReadFromJsonAsync<DTO.Output.AccessTokenOutput>();

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