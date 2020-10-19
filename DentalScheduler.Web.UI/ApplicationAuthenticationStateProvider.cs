using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System;

namespace DentalScheduler.Web.UI
{
    public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ApplicationAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public ILocalStorageService LocalStorage { get; }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = await GetClaimsPrincipalAsync();
            return new AuthenticationState(claimsPrincipal);
        }

        public async Task MarkUserAsLoggedInAsync(
            IUserCredentialsInput credentials,
            string accessToken)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var roleName = jwtSecurityToken.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))
                ?.Value;

            await LocalStorage.SetItemAsync(Identity.LocalStorageKeys.User.UserName, credentials.UserName);
            await LocalStorage.SetItemAsync(Identity.LocalStorageKeys.User.RoleName, roleName);
            await LocalStorage.SetItemAsync(Identity.LocalStorageKeys.Auth.AccessToken, accessToken);

            var patienReferenceIdAsString = jwtSecurityToken.Claims
                .FirstOrDefault(c => c.Type.Equals("PatientReferenceId"))
                ?.Value;
            var patienReferenceId = patienReferenceIdAsString != null
                ? new Guid(patienReferenceIdAsString)
                : Guid.Empty;
            await LocalStorage.SetItemAsync(Scheduling.LocalStorageKeys.Patient.ReferenceId, patienReferenceId);

            var claimsPrincipal = await GetClaimsPrincipalAsync();

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await LocalStorage.RemoveItemAsync(Identity.LocalStorageKeys.User.UserName);
            await LocalStorage.RemoveItemAsync(Identity.LocalStorageKeys.User.RoleName);
            await LocalStorage.RemoveItemAsync(Identity.LocalStorageKeys.Auth.AccessToken);
            await LocalStorage.RemoveItemAsync(Scheduling.LocalStorageKeys.Patient.ReferenceId);

            var claimsPrincipal = await GetClaimsPrincipalAsync();

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private async Task<ClaimsPrincipal> GetClaimsPrincipalAsync()
        {
            var claims = new List<Claim>();

            if (await LocalStorage.ContainKeyAsync(Identity.LocalStorageKeys.User.UserName))
            {
                var userName = await LocalStorage
                    .GetItemAsync<string>(Identity.LocalStorageKeys.User.UserName);
                claims.Add(new Claim(ClaimTypes.Name, userName));
            }

            if (await LocalStorage.ContainKeyAsync(Identity.LocalStorageKeys.User.RoleName))
            {
                var roleName = await LocalStorage
                    .GetItemAsync<string>(Identity.LocalStorageKeys.User.RoleName);
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            var identity =  claims.Count > 0
                ? new ClaimsIdentity(claims, "apiauth_type")
                : new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }
    }
}