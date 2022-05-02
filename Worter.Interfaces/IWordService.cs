using FMCW.Common.Results;
using Worter.DTO.Language;

namespace Worter.Interfaces
{
    public interface IWordService
    {
        IntResult Add(TranslateDTO wordDto, int userId);
        BoolResult DeleteTranslate(int idTranslate);
        ListResult<TranslateDTO> Get(WordFilterDTO filters, int iduser);
    }
}
