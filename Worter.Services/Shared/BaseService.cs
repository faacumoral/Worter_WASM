using Microsoft.Extensions.Configuration;
using Worter.DAO.Models;

namespace Worter.Services.Shared
{
    public abstract class BaseService
    {
        protected readonly WorterContext context;
        protected readonly IConfiguration configuration;

        public BaseService(WorterContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
    }
}
