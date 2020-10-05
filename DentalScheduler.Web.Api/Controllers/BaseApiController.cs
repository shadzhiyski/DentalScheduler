using System.Linq;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.Api.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult PresentResult<T>(IResult<T> result) where T : class
        {
            if (result.Value != default(T))
            {
                return Ok(result.Value);
            }

            if (result.Errors.Count() == 1
                && result.Errors.First().Type == ErrorType.NotFound)
            {
                return NotFound(result.Errors.First());
            }
            
            return BadRequest(result.Errors);
        }
    }
}