using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<IError> errors)
        {
            var firstError = errors[0];
            var statusCode = firstError.Metadata["statusCode"];
            return Problem(statusCode: (int)statusCode, title: firstError.Message);
        }
    }
}
