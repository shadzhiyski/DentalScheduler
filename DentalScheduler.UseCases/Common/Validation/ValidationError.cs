using System.Collections.Generic;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using Newtonsoft.Json;

namespace DentalScheduler.UseCases.Common.Validation
{
    public class ValidationError : IValidationError
    {
        [JsonIgnore]
        public ErrorType Type => ErrorType.Validation;

        public string PropertyName { get; set; }

        public IList<string> Errors { get; set; }
    }
}