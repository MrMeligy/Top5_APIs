using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Top5.Api.Helper;

namespace Top5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string message = "Success")
        {
            return Ok(new ApiResponse<T>(message, data));
        }

        protected IActionResult Created<T>(T data, string message = "Created")
        {
            return StatusCode(201, new ApiResponse<T>(message, data));
        }

        protected IActionResult Failed(string message, int statusCode)
        {
            return StatusCode(statusCode,new ApiResponse<object>(message, null));
        }
    }
}
