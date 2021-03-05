using System.Collections.Generic;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.UseCases.Common.Dto.Output
{
    public class Result<TValue> : IResult<TValue> where TValue : class
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

        public TValue Value { get; }

        public IEnumerable<IError> Errors { get; }

        public ResultType Type { get; }
    }
}