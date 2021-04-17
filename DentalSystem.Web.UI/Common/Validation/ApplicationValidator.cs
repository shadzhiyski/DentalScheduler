using System;
using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.UseCases.Common.Validation;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DentalSystem.Web.UI.Common.Validation
{
    public class ApplicationValidator<TModel> : ComponentBase
        where TModel : class
    {
        [CascadingParameter]
        private EditContext EditContext { get; set; }

        [Inject]
        public IApplicationValidator<TModel> Validator { get; set; }

        private ValidationMessageStore ValidationMessageStore { get; set; }

        [Inject]
        private IServiceProvider ServiceProvider { get; set; }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            if (EditContext == null)
            {
                throw new NullReferenceException($"{nameof(Validator)} must be placed within an {nameof(EditForm)}");
            }

            SetupEditContext();
        }

        void SetupEditContext()
        {
            ValidationMessageStore = new ValidationMessageStore(EditContext);
            HookUpEditContextEvents();
        }

        private void HookUpEditContextEvents()
        {
            EditContext.OnValidationRequested += ValidationRequested;
            EditContext.OnFieldChanged += FieldChanged;
        }

        void ValidationRequested(object sender, ValidationRequestedEventArgs args)
        {
            ValidationMessageStore.Clear();

            var model = EditContext.Model as TModel;
            if (model != null)
            {
                IValidationResult result = Validator.Validate(model);
                AddValidationResult(EditContext.Model, result);
            }
        }

        void FieldChanged(object sender, FieldChangedEventArgs args)
        {
            FieldIdentifier fieldIdentifier = args.FieldIdentifier;
            ValidationMessageStore.Clear();

            var model = fieldIdentifier.Model as TModel;
            if (model != null)
            {
                IValidationResult result = Validator.Validate(model);
                AddValidationResult(fieldIdentifier.Model, result);
            }
        }

        void AddValidationResult(object model, IValidationResult validationResult)
        {
            foreach (IValidationError error in validationResult.Errors)
            {
                var fieldIdentifier = new FieldIdentifier(model, error.PropertyName);
                ValidationMessageStore.Add(fieldIdentifier, error.Errors);
            }

            EditContext.NotifyValidationStateChanged();
        }
    }
}