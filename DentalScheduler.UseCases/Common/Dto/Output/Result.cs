using System.Collections.Generic;
using DentalScheduler.Interfaces.Dto.Output.Common;

namespace DentalScheduler.Dto.Output.Common
{
    public class Result<TValue> : IResult<TValue> where TValue : class
    {
        public Result(TValue value)
            : this(value, new List<IError>())
        { }

        public Result(IEnumerable<IError> errors)
            : this(default(TValue), errors)
        { }

        private Result(TValue value, IEnumerable<IError> errors) 
        {
            Value = value;
            Errors = errors;
        }

        public TValue Value { get; }

        public IEnumerable<IError> Errors { get; }
    }
}