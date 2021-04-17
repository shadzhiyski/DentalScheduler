using System.Collections.Generic;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;
using Newtonsoft.Json;

namespace DentalSystem.Application.UseCases.Common.Validation
{
    public class ValidationError : IValidationError
    {
        [JsonIgnore]
        public ErrorType Type => ErrorType.Validation;

        public string PropertyName { get; set; }

        public IList<string> Errors { get; set; }
    }
}