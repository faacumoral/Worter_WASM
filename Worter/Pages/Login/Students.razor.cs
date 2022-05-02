using Blazored.SessionStorage;
using FMCW.Common.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;
using Worter.DTO.Login;
using Worter.HTTP;

namespace Worter.Pages.Login
{
    public class StudentsBase : ComponentBase
    {
        [Inject] APIClient APIClient { get; set; }
        [Inject] ISessionStorageService SessionStorage { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected StringResult loginResponse;
        protected StudentLoginRequest credentials = new StudentLoginRequest();
        protected bool requestSent = false;

        protected async Task DoLogin()
        {
            loginResponse = null;
            var apiRequest = Request.BuildPost("Login/Student", credentials, false);
            requestSent = true;
            loginResponse = await APIClient.Send<StringResult>(apiRequest);
            requestSent = false;
            if (loginResponse.Success)
            {
                await SessionStorage.SetJwtToken(loginResponse.ResultOk);
                NavigationManager.NavigateTo(Constants.Views.INDEX);
            }
        }
    }
}
