using FMCW.Common.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worter.Common;
using Worter.DTO.Language;
using Worter.HTTP;
using Worter.Services.Toast;

namespace Worter.Pages
{
    public class LearnBase : ComponentBase
    {
        [Inject] APIClient APIClient { get; set; }
        [Inject] ToastService toastService { get; set; }

        protected int idLanguage = 0;
        protected bool languageSelected = false;

        protected List<LanguageDTO> languages = new List<LanguageDTO>();

        protected LearnDTO learn = new LearnDTO();
        protected List<AnswerDTO> userAnswers = new List<AnswerDTO>();
        protected List<LearnDTO> learns = new List<LearnDTO>();

        protected async Task LoadLanguages()
        {
            var apiRequest = Request.BuildGet("Language");
            var response = await APIClient.Send<ListResult<LanguageDTO>>(apiRequest);
            if (response.Success)
            {
                languages = response.ResultOk;
            }
            else
            {
                toastService.ShowToast(response.ResultError.FriendlyErrorMessage, ToastLevel.Error);
            }
        }

        protected void ConfirmLanguage()
        {
            if (idLanguage == 0)
            {
                toastService.ShowError("Pick a language to learn");
                return;
            }

            languageSelected = true;
        }

        protected async Task StartLearn()
        {
            var url = $"Learn?IdLanguage={idLanguage}";
            var apiRequest = Request.BuildGet(url);
            var response = await APIClient.Send<ListResult<LearnDTO>>(apiRequest);
            if (response.Success)
            {
                learns = response.ResultOk;
                LoadNewWord();
            }
            else
            {
                toastService.ShowError(response.ResultError.FriendlyErrorMessage);
            }
        }

        protected void LoadNewWord()
        {
            learn = learns.GetRandom();
            learn.UserAnswers = new List<AnswerDTO>();
            // add all possible answers
            for (int i = 0; i < learn.Translations.Count; i++)
            {
                learn.UserAnswers.Add(new AnswerDTO
                {
                    IsCorrect = false,
                    UserAnswer = "",
                    IdTranslate = 0
                });
            }
        }

        protected void CheckAnswer(AnswerDTO answer)
        {
            // check if its correct
            var correctAnswer = learn.Translations.FirstOrDefault(t => t.Answer == answer.UserAnswer);
            // and check if is not already answered
            var isOnlyOneTime = learn.UserAnswers.Count(a => answer.UserAnswer == a.UserAnswer) == 1;
            if (correctAnswer != null && isOnlyOneTime)
            {
                answer.IdTranslate = correctAnswer.IdTranslate;
                answer.IsCorrect = true;
            }
        }

        #region Next and dont know buttons
        protected async Task DontKnow()
        {
            // check which answers are correct and which ones dont
            var answers = learn.Translations.Select(t =>
                new AnswerDTO
                {
                    IdTranslate = t.IdTranslate,
                    // check if it is answered
                    IsCorrect = learn.UserAnswers.Any(ua => ua.IdTranslate == t.IdTranslate)
                }).ToList();
            
            await ChangeWord(answers);
        }

        protected async Task Next()
        {
            // all corrects and have id translate setted
            await ChangeWord(learn.UserAnswers);
        }

        protected async Task ChangeWord(List<AnswerDTO> answers)
        {
            userAnswers.AddRange(answers);
            LoadNewWord();
            await CheckIfSendAnswers();
        }
        #endregion

        protected async Task CheckIfSendAnswers()
        {
            if (learns.Count <= Common.Constants.CONSTANTS.WORDS_TO_RETURN) 
            {
                var url = "Learn";
                var apiRequest = Request.BuildPost(url, userAnswers);
                var response = await APIClient.Send<ListResult<LearnDTO>>(apiRequest);
                if (response.Success)
                {
                    learns.AddRange(response.ResultOk);
                }
                else
                {
                    toastService.ShowError(response.ResultError.FriendlyErrorMessage);
                }                
                userAnswers = new List<AnswerDTO>();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadLanguages();
        }
    }
}
