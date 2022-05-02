using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Worter.API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppController : ControllerBase
    {
        protected readonly IConfiguration configuration;

        protected int UserId { get => int.Parse(User.Claims.First(c => c.Type == "UserId").Value); }

        public AppController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

    }
}