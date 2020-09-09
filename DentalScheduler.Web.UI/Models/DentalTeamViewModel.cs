using System;

namespace DentalScheduler.Web.UI.Models
{
    public class DentalTeamViewModel
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }

        public RoomViewModel Room { get; set; }
    }
}