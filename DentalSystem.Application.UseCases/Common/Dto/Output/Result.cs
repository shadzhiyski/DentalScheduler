using System.Collections.Generic;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Application.UseCases.Common.Dto.Output
{
    public record Result<TValue> : IResult<TValue> where TValue : class
    {
        public Result(
            TValue value,
            ResultType type = ResultType.Succeeded)
            : this(value, new List<IError>(), type)
        { }

        public Result(
            IEnumerable<IError> errors,
            ResultType type = ResultType.Failed)
            : this(default(TValue), errors, type)
        { }

        private Result(
            TValue value,
            IEnumerable<IError> errors,
            ResultType type)
        {
            Value = value;
            Errors = errors;
            Type = type;
        }

        public TValue Value { get; init; }

        public IEnumerable<IError> Errors { get; init; }

        public ResultType Type { get; init; }
    }
}