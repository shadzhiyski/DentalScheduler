using System;

namespace DentalSystem.Interfaces.UseCases.Scheduling.Dto.Output
{
    public interface IPatientOutput
    {
        Guid? ReferenceId { get; set; }

        byte[] Avatar { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}