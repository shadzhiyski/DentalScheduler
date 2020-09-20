using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using DentalScheduler.Web.UI.Models;
using Radzen;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Web.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using DentalScheduler.DTO.Input;

namespace DentalScheduler.Web.UI.Components
{
    public partial class EditTreatmentSession
    {
        public const int DefaultDurationInMinutes = 30;
        
        [Parameter]
        public ITreatmentSessionInput Model { get; set; } = new TreatmentSessionInput();

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        DialogService DialogService { get; set; }
        
        [Inject]
        IDentalTeamService DentalTeamService { get; set; }
        
        [Inject]
        ITreatmentService TreatmentService { get; set; }

        public EditContext EditContext { get; set; }

        public bool IsTreatmentDropDownDisabled { get; set; }

        public bool IsDentalTeamDropDownDisabled { get; set; }

        public int? StandardDurationInMinutes => Treatments?.SingleOrDefault(
                t => Model.TreatmentReferenceId?.Equals(t.ReferenceId) ?? false
            )?.DurationInMinutes;
        
        public int DurationInMinutes { get; set; }

        public IEnumerable<DentalTeamDropDownViewModel> DentalTeams { get; set; }

        public IEnumerable<TreatmentDropDownViewModel> Treatments { get; set; }

        public TreatmentSessionPeriodWrapperModel PeriodWrapperModel => new TreatmentSessionPeriodWrapperModel(Model);

        protected async override Task OnInitializedAsync()
        {
            EditContext = new EditContext(Model);

            var user = (await AuthenticationStateTask).User;
            IsTreatmentDropDownDisabled = user.IsInRole("Dentist");
            IsDentalTeamDropDownDisabled = user.IsInRole("Dentist");
        }

        async Task LoadDentalTeams()
        {
            DentalTeams = await DentalTeamService.GetDentalTeamsDropDownListAsync();
        }

        async Task LoadTreatments()
        {
            Treatments = await TreatmentService.GetTreatmentsAsync();
            SetDurationInMinutes();
        }

        void OnTreatmentChange()
        {
            SetDurationInMinutes();
            SetPeriod();
        }

        void OnPeriodChange() 
            => SetPeriod();
        
        void SetDurationInMinutes() 
            => DurationInMinutes = StandardDurationInMinutes ?? DefaultDurationInMinutes;

        void SetPeriod() 
            => Model.End = Model.Start.Value.AddMinutes(DurationInMinutes);

        void Approve()
            => Model.Status = "Accepted";

        void Reject()
            => Model.Status = "Rejected";
        
        private void OnSubmit() 
            => DialogService.Close(Model);
    }
}