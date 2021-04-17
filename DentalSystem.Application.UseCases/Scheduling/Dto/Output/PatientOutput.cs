using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Output
{
    public class PatientOutput : IPatientOutput
    {
        public Guid? ReferenceId { get; set; }

        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}