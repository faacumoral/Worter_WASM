using FMCW.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Worter.API.Controllers.Shared;
using Worter.DAO.Models;
using Worter.DTO.Language;
using Worter.Interfaces;

namespace Worter.API.Controllers
{
    public class LanguageController : AppController
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService, 
            IConfiguration configuration) : base(configuration)
        {
            _languageService = languageService;
        }

        [HttpGet]
        [Authorize]
        public ListResult<LanguageDTO> Get()
            => _languageService.GetAll();
        
    }
}