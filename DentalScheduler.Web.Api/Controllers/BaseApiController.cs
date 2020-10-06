using System.Linq;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.Api.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult PresentResult<T>(IResult<T> result) where T : class
            =>
            result.Status switch
            {
                (ResultStatus.Success) => Ok(result.Value),
                (ResultStatus.Invalid) => BadRequest(result.Errors),
                (ResultStatus.NotFound) => NotFound(result.Errors)
            };
    }
}