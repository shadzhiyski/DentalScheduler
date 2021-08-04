using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading;

namespace DentalSystem.Infrastructure.Identity.Services
{
    public class JwtAuthManager : IJwtAuthManager
    {
        public IConfiguration Config { get; }

        public IReadRepository<Patient> PatientRepository { get; }

        public IReadRepository<DentalWorker> DentalWorkerRepository { get; }

        public IReadRepository<DentalTeamParticipant> DentalTeamParticipantRepository { get; }

        public IReadRepository<DentalTeam> DentalTeamRepository { get; }

        public JwtAuthManager(
            IConfiguration config,
            IReadRepository<Patient> patientRepository,
            IReadRepository<DentalWorker> dentalWorkerRepository,
            IReadRepository<DentalTeamParticipant> dentalTeamParticipantRepository,
            IReadRepository<DentalTeam> dentalTeamRepository)
        {
            Config = config;
            PatientRepository = patientRepository;
            DentalWorkerRepository = dentalWorkerRepository;
            DentalTeamParticipantRepository = dentalTeamParticipantRepository;
            DentalTeamRepository = dentalTeamRepository;
        }

        public async Task<string> GenerateJwtAsync(IUserCredentialsInput userInfo, string roleName, CancellationToken cancellationToken)
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

            if (roleName.Equals(RoleType.Patient.ToString()))
            {
                var patient = await PatientRepository.SingleOrDefaultAsync(p => p.IdentityUser.UserName == userInfo.UserName, cancellationToken);
                var patientClaims = new Claim("PatientReferenceId", patient.ReferenceId.ToString());
                claims.Add(patientClaims);
            }
            else if (roleName.Equals(RoleType.Dentist.ToString()))
            {
                var dentalTeamReferenceId = await DentalWorkerRepository.AsNoTracking()
                    .Where(dw => dw.IdentityUser.UserName == userInfo.UserName)
                    .Join(
                        DentalTeamParticipantRepository.AsNoTracking(),
                        (dw) => dw.Id,
                        (dtp) => dtp.ParticipantId,
                        (dw, dtp) => new
                        {
                            dtp.TeamId
                        }
                    ).Join(
                        DentalTeamRepository.AsNoTracking(),
                        (q) => q.TeamId,
                        (dt) => dt.Id,
                        (q, dt) => new
                        {
                            dt.ReferenceId
                        }
                    ).SingleOrDefaultAsync(cancellationToken);
                var dentalWorkerClaims = new Claim("DentalTeamReferenceId", dentalTeamReferenceId.ReferenceId.ToString());
                claims.Add(dentalWorkerClaims);
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