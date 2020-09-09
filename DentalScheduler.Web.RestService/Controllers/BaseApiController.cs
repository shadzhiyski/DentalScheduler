using System.Linq;
using DentalScheduler.Interfaces.Models.Output.Common;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.RestService.Controllers
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