using System.Collections.Generic;
using Worter.DTO.Language;

namespace Worter.Interfaces
{
    public interface ILanguageService 
    {
        List<LanguageDTO> GetAll();
    }
}
