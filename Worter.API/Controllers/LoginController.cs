using FMCW.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Worter.DTO.Login;
using Microsoft.Extensions.Configuration;
using Worter.Common.Constants;
using Worter.API.Shared;
using Worter.Interfaces;

namespace Worter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IStudentService _studentService;

        protected readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration,
            IStudentService studentService)
        {
            _studentService = studentService;
            _configuration = configuration;
        }

        [Route("Student")]
        [HttpPost]
        public StringResult LoginStudent([FromBody] StudentLoginRequest student)
        {
            if (string.IsNullOrEmpty(student.Username) || string.IsNullOrEmpty(student.Password))
            {
                return StringResult.Error("Username and password cannot be blank");
            }

            var loginResult = _studentService.LoginStudent(student.Username, student.Password);
            if (loginResult.Success)
            {
                var secretKey = _configuration[CONSTANTS.Keys.JWT_SECRETKEY];
                var issuer = _configuration[CONSTANTS.Keys.JWT_ISSUER];
                var audience = _configuration[CONSTANTS.Keys.JWT_AUDIENCE];
                var token =JwtHandler.GenerateAPIToken(loginResult.ResultOk.ToString(), secretKey, issuer, audience);
                return StringResult.Ok(token);
            }
            else
            {
                var result = StringResult.Error();
                result.ResultError = loginResult.ResultError;
                return result;
            }
        }
    }
}