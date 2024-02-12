using BuberDinner.Api.Filters;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{

    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationCommandService _authenticationService;

        public AuthenticationController(IAuthenticationCommandService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var registerResult = await _authenticationService.RegisterAsync(request.FirstName, request.LastName, request.Email, request.Password);

            if (registerResult.IsSuccess) return Ok(MapAuthResult(registerResult.Value));

            var firstError = registerResult.Errors.FirstOrDefault();
            if (firstError is DuplicateEmailError) return Problem(registerResult.Errors);

            return Problem();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResult = await _authenticationService.LoginAsync(request.Email, request.Password);
            var response = new AuthenticationResponse(
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.Token);

            return Ok(response);
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.Token);
        }


    }
}
