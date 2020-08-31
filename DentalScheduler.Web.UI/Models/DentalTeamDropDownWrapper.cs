using System;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.Web.UI.Models
{
    public class DentalTeamDropDownWrapper
    {
        public DentalTeamDropDownWrapper(IDentalTeamOutput dentalTeam)
        {
            DentalTeam = dentalTeam;
        }

        public string Value 
        { 
            get => DentalTeam.ReferenceId.ToString();
            set => DentalTeam.ReferenceId = new Guid(value);
        }
        
        public string Text 
        { 
            get => DentalTeam.Name; 
            set => DentalTeam.Name = value;
        }

        public IDentalTeamOutput DentalTeam { get; set; }
    }
}