using System.Collections.Generic;

namespace Worter.DTO.Language
{
    public class LearnDTO
    {
        public string Word { get; set; }

        public List<CorrectAnswerDTO> Translations { get; set; }
        public List<AnswerDTO> UserAnswers { get; set; }
    }
}
