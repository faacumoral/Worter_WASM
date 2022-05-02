using FMCW.Common;

namespace Worter.DTO.Language
{
    public class TranslateDTO : BaseDTO
    {
        private string translateMeaning;
        private string originalMeaning;

        public int IdWord { get; set; }
        public int IdTranslate { get; set; }
        public int IdLanguage { get; set; }

        public string OriginalMeaning 
        {   
            get => originalMeaning?.Trim(); 
            set => originalMeaning = value?.Trim(); 
        }

        public string TranslateMeaning
        {
            get => translateMeaning?.Trim();
            set => translateMeaning = value?.Trim();
        }

    }
}
