using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationCommandService : IAuthenticationCommandService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator,
                                     IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            //validate the user don't exist
            if ((await _userRepository.GetUserByEmailAsync(email)) is not User user)
            {
                throw new Exception("email or password incorrect");
            }

            //validate the password is correct
            if (user.Password != password)
            {
                throw new Exception("email or password incorrect");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }

        public async Task<Result<AuthenticationResult>> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            //Validate the user don't exist
            if ((await _userRepository.GetUserByEmailAsync(email)) is not null)
            {
                return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
            }

            //create a new user & persist to DB
            var user = new User(firstName, lastName, email, password);
            await _userRepository.AddUserAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
