using System.Linq;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;
using Microsoft.AspNetCore.Mvc;

namespace DentalSystem.Web.Api.Controllers
{
    /// <summary>
    /// Base Api Controller.
    /// </summary>
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Presents result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
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