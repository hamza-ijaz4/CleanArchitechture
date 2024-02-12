using System.Net;

namespace BuberDinner.Application.Common.Errors
{
    public class DuplicateEmailError : FluentResults.IError
    {
        public List<FluentResults.IError> Reasons => new List<FluentResults.IError>();

        public string Message => "Email Already Exists";

        public Dictionary<string, object> Metadata => new Dictionary<string, object>
        {
            { "statusCode", HttpStatusCode.Conflict }
        };
    }
}
