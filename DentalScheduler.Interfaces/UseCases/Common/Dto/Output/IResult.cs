using System.Collections.Generic;

namespace DentalScheduler.Interfaces.UseCases.Common.Dto.Output
{
    public interface IResult<TValue> where TValue : class
    {
        TValue Value { get; }

        IEnumerable<IError> Errors { get; }

        ResultStatus Status { get; }
    }
}