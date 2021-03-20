using System.Collections.Generic;

namespace DentalSystem.Interfaces.UseCases.Common.Dto.Output
{
    public interface IResult<TValue> where TValue : class
    {
        TValue Value { get; }

        IEnumerable<IError> Errors { get; }

        ResultType Type { get; }
    }
}