using FMCW.Common.Results;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worter.DTO.Language;
using Worter.HTTP;
using Worter.Services.Toast;

namespace Worter.Pages
{
    public class VocabularyBase : ComponentBase
    {
        [Inject] APIClient APIClient { get; set; }
        [Inject] ToastService toastService { get; set; }

        protected TranslateDTO newTranslate = new TranslateDTO();
        protected List<LanguageDTO> languages = new List<LanguageDTO>();
        protected WordFilterDTO filters = new WordFilterDTO();
        protected List<TranslateDTO> translates = null;
        protected bool requestSent = false;
        protected bool requestGetSent = false;

        protected async void AddWord()
        {
            if (string.IsNullOrEmpty(newTranslate.OriginalMeaning))
            {
                toastService.ShowError("Complete original meaning field");
                return;
            }

            if (string.IsNullOrEmpty(newTranslate.TranslateMeaning))
            {
                toastService.ShowError("Complete translate meaning field");
                return;
            }

            if (newTranslate.IdLanguage == 0)
            {
                toastService.ShowError("Pick a language");
                return;
            }

            var apiRequest = Request.BuildPost("Word", newTranslate);
            requestSent = true;
            var addRequest = await APIClient.Send<IntResult>(apiRequest);
            requestSent = false;

            if (addRequest.Success)
            {
                if (addRequest.ResultOperation == ResultOperation.RegisterAlreadyAdd)
                {
                    toastService.ShowWarning("Original and translate meaning already exists");
                }
                else
                {
                    toastService.ShowSucces("New word added correctly");
                }

                newTranslate.OriginalMeaning = "";
                newTranslate.TranslateMeaning = "";
                StateHasChanged();
            }
            else
            {
                toastService.ShowError("An error has ocurred");
            }
        }
        protected async Task LoadLanguages()
        {
            var apiRequest = Request.BuildGet("Language");
            var response = await APIClient.Send<ListResult<Worter.DTO.Language.LanguageDTO>>(apiRequest);
            if (response.Success)
            {
                languages = response.ResultOk;
            }
            else
            {
                toastService.ShowToast(response.ResultError.FriendlyErrorMessage, ToastLevel.Error);
            }
        }

        protected async Task DeleteTranslate(TranslateDTO translate)
        {
            var apiRequest = Request.BuildDelete($"Word/{translate.IdTranslate}");
            requestGetSent = true;
            var deleteRequest = await APIClient.Send<BoolResult>(apiRequest);
            requestGetSent = false;
            Console.WriteLine(deleteRequest.Success);
            Console.WriteLine(deleteRequest.ResultOk);
            if (deleteRequest.Success && deleteRequest.ResultOk)
            {
                translates.Remove(translate);
            }
            else
            {
                toastService.ShowError(deleteRequest.ResultError?.FriendlyErrorMessage);
            }
        }

        protected async Task GetWords()
        {
            if (filters.IdLanguage == 0)
            {
                toastService.ShowError("Pick a language");
                return;
            }

            if (string.IsNullOrEmpty(filters.Filter))
            {
                toastService.ShowError("Type something!");
                return;
            }

            var url = $"Word?IdLanguage={filters.IdLanguage}&Filter={filters.Filter}";
            var request = Request.BuildGet(url);
            requestGetSent = true;
            var response = await APIClient.Send<ListResult<TranslateDTO>>(request);
            requestGetSent = false;
            if (response.Success)
            {
                translates = response.ResultOk;
            }
            else
            {
                toastService.ShowError(response.ResultError.FriendlyErrorMessage);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadLanguages();
        }
    }
}
