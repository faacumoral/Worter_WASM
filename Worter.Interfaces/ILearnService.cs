using FMCW.Common.Results;
using System.Collections.Generic;
using Worter.Common.Constants;
using Worter.DTO.Language;

namespace Worter.Interfaces
{
    public interface ILearnService 
    {
        ListResult<LearnDTO> GetLearns(int idLanguage, int userId, int wordsToReturn = CONSTANTS.WORDS_TO_RETURN);

        ListResult<LearnDTO> SaveResultsGetNew(List<AnswerDTO> userAnswers, int userId);
    }
}
