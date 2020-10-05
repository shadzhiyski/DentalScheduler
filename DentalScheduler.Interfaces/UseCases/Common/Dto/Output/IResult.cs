using System.Collections.Generic;

namespace DentalScheduler.Interfaces.Dto.Output.Common
{
    public interface IResult<TValue> where TValue : class
    {
        TValue Value { get; }

        IEnumerable<IError> Errors { get; }
    }
}