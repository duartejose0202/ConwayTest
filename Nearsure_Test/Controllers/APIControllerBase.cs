using Microsoft.AspNetCore.Mvc;
using ConwayGame.Models;

namespace ConwayGame.Controllers
{
    public class APIControllerBase : ControllerBase
    {
        protected ObjectResult JsonResult(string message, object? data = null, int statusCode = StatusCodes.Status200OK)
        {
            return StatusCode(statusCode, new APIResponse
            {
                Success = statusCode == StatusCodes.Status200OK,
                Message = message,
                Data = data
            });
        }
    }
}
