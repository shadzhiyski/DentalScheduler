using System.Collections.Generic;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;
using Newtonsoft.Json;

namespace DentalSystem.UseCases.Common.Validation
{
    public class ValidationError : IValidationError
    {
        [JsonIgnore]
        public ErrorType Type => ErrorType.Validation;

        public string PropertyName { get; set; }

        public IList<string> Errors { get; set; }
    }
}