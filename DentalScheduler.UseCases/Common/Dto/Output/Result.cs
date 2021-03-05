using System.Collections.Generic;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.UseCases.Common.Dto.Output
{
    public class Result<TValue> : IResult<TValue> where TValue : class
    {
        public Result(
            TValue value,
            ResultStatus status = ResultStatus.Success)
            : this(value, new List<IError>(), status)
        { }

        public Result(
            IEnumerable<IError> errors,
            ResultStatus status = ResultStatus.Invalid)
            : this(default(TValue), errors, status)
        { }

        private Result(
            TValue value,
            IEnumerable<IError> errors,
            ResultStatus status)
        {
            Value = value;
            Errors = errors;
            Status = status;
        }

        public TValue Value { get; }

        public IEnumerable<IError> Errors { get; }

        public ResultStatus Status { get; }
    }
}