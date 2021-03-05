using System.Linq;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.Api.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult PresentResult<T>(IResult<T> result) where T : class
            => result.Type switch
            {
                ResultType.Succeeded => Ok(result.Value),
                ResultType.Created => Created(string.Empty, result.Value),
                ResultType.Updated => NoContent(),
                ResultType.Deleted => NoContent(),
                ResultType.Failed => BadRequest(result.Errors),
                ResultType.NotFound => NotFound(result.Errors)
            };
    }
}