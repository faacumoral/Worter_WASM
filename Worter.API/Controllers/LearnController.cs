using FMCW.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Worter.API.Controllers.Shared;
using Worter.Common.Constants;
using Worter.DTO.Language;
using Worter.Interfaces;

namespace Worter.API.Controllers
{
    public class LearnController : AppController
    {
        protected readonly ILearnService _learnService;

        public LearnController(
            ILearnService learnService, 
            IConfiguration configuration) : base(configuration)
        {
            _learnService = learnService;
        }

        #region POST

        [HttpPost]
        public ListResult<LearnDTO> SaveResultsGetNew([FromBody]List<AnswerDTO> userAnswers)
          => _learnService.SaveResultsGetNew(userAnswers, UserId);

        #endregion

        #region GET
        [HttpGet]
        public ListResult<LearnDTO> GetInitialLearns([FromQuery] int IdLanguage)
          => _learnService.GetLearns(IdLanguage, UserId, CONSTANTS.WORDS_TO_RETURN * 2 /* first time */);
        #endregion
    }
}