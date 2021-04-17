using System.Collections.Generic;

namespace DentalSystem.Boundaries.UseCases.Common.Dto.Output
{
    public interface IResult<TValue> where TValue : class
    {
        TValue Value { get; }

        IEnumerable<IError> Errors { get; }

        ResultType Type { get; }
    }
}