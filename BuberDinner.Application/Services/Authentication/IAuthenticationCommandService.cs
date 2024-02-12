using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public interface IAuthenticationCommandService
    {
        Task<Result<AuthenticationResult>> RegisterAsync(string firstName, string lastName, string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
