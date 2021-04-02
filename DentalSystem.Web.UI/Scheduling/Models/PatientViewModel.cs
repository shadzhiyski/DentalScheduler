using System;

namespace DentalSystem.Web.UI.Models
{
    public class PatientViewModel
    {
        public Guid ReferenceId { get; set; }

        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}