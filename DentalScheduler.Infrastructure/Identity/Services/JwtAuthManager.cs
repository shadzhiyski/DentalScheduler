using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DentalScheduler.Infrastructure.Identity.Services
{
    public class JwtAuthManager : IJwtAuthManager
    {
        public IConfiguration Config { get; }

        public IGenericRepository<Patient> PatientRepository { get; }

        public JwtAuthManager(IConfiguration config, IGenericRepository<Patient> patientRepository)
        {
            Config = config;
            PatientRepository = patientRepository;
        }
        
        public string GenerateJwt(IUserCredentialsInput userInfo, string roleName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            int expiryInMinutes = Convert.ToInt32(Config["Jwt:ExpiryInMinutes"]);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.UserName.ToString()),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var patient = PatientRepository.SingleOrDefault(p => p.IdentityUser.UserName == userInfo.UserName);
            if (patient != null)
            {
                var patientClaims = new Claim("PatientReferenceId", patient.ReferenceId.ToString());
                claims.Add(patientClaims);
            }

            var token = new JwtSecurityToken(
                issuer: Config["Jwt:Issuer"],
                audience: Config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}