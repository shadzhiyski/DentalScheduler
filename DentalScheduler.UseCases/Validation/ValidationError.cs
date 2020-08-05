using System.Collections.Generic;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases.Validation;
using Newtonsoft.Json;

namespace DentalScheduler.UseCases.Validation
{
    public class ValidationError : IValidationError
    {
        [JsonIgnore]
        public ErrorType Type => ErrorType.Validation;

        public string PropertyName { get; set; }

        public IEnumerable<string> Messages { get; set; }
    }
}