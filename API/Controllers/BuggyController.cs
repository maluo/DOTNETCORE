using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : BaseApiController
    {
        [HttpGet("unauthorized")]
        public ActionResult<string> GetUnauthorized()
        {
            return Unauthorized("You are not authorized to access this resource.");
        }

        [HttpGet("badrequest")]
        public ActionResult<string> GetBadRequest()
        {
            return Unauthorized("Not a good request.");
        }

        [HttpGet("notfound")]
        public ActionResult<string> GetNotFound()
        {
            return NotFound("Resource not found.");
        }

        [HttpGet("internalerror")]
        public ActionResult<string> GetInternalError()
        {
            throw new Exception("This is an internal server error.");
        }

        [HttpPost("validationerror")]
        public ActionResult<string> GetValidationError(CreateProductDto product)
        {
            return Ok();//why it would triggers the middleware here? Becuause of the CreateProductDto.
        }
    }
}