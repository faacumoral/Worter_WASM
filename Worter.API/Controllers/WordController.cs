using FMCW.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Worter.API.Controllers.Shared;
using Worter.DTO.Language;
using Worter.Interfaces;

namespace Worter.API.Controllers
{
    public class WordController : AppController
    {
        private readonly IWordService _wordService;

        public WordController(IConfiguration configuration,
            IWordService wordService) 
            : base(configuration)
        {
            _wordService = wordService;
        }

        #region POST
        [HttpPost]
        public IntResult PostTranslate([FromBody]TranslateDTO word)
            => _wordService.Add(word, UserId);

        [HttpDelete]
        public BoolResult DeleteTranslate([FromRoute]int IdTranslate)
           => _wordService.DeleteTranslate(IdTranslate);

        #endregion

        #region GET
        [HttpGet]
        public ListResult<TranslateDTO> Get([FromQuery]WordFilterDTO filters)
            => _wordService.Get(filters, UserId);
        #endregion
    }
}