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

namespace DentalScheduler.Web.UI.Components
{
    public partial class EditAppointment
    {
        [Inject]
        DialogService DialogService { get; set; }
        
        [Inject]
        IDentalTeamService DentalTeamService { get; set; }
        
        [Inject]
        ITreatmentService TreatmentService { get; set; }

        public EditContext EditContext { get; set; }

        public const int DefaultDurationInMinutes = 30;

        public int? StandardDurationInMinutes => Treatments?.SingleOrDefault(
                t => t.ReferenceId.Equals(Model.TreatmentReferenceId?.ToString() ?? Guid.Empty.ToString())
            )?.DurationInMinutes;
        
        public int DurationInMinutes { get; set; }

        public string DentalTeamReferenceId
        {
            get => Model.DentalTeamReferenceId?.ToString();
            set => Model.DentalTeamReferenceId = value != default
                ? new Guid(value)
                : default;
        }

        public string TreatmentReferenceId
        {
            get => Model.TreatmentReferenceId?.ToString();
            set => Model.TreatmentReferenceId = value != default
                ? new Guid(value)
                : default;
        }

        public IEnumerable<DentalTeamDropDownViewModel> DentalTeams { get; set; }

        public IEnumerable<TreatmentDropDownViewModel> Treatments { get; set; }

        public TreatmentSessionPeriodWrapperModel PeriodWrapperModel => new TreatmentSessionPeriodWrapperModel(Model);
        
        [Parameter]
        public ITreatmentSessionInput Model { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(Model);
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
            => DurationInMinutes = Treatments.SingleOrDefault(
                    t => t.ReferenceId.Equals(Model.TreatmentReferenceId?.ToString() ?? Guid.Empty.ToString())
                )
                ?.DurationInMinutes ?? DefaultDurationInMinutes;

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