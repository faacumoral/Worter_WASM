using System;
using System.Net.Http;
using System.Threading.Tasks;
using FMCW.Common.Results;
using System.Net.Http.Json;
using Blazored.SessionStorage;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;

namespace Worter.UI.HTTP
{
    public class APIClient
    {
        private readonly HttpClient client;

        private readonly ISessionStorageService sessionStorage;

        private readonly NavigationManager navigationManager;

        public APIClient(
            HttpClient client, 
            ISessionStorageService sessionStorage,
            NavigationManager navigationManager)
        {
            this.client = client;
            this.sessionStorage = sessionStorage;
            this.navigationManager = navigationManager;
        }

        public async Task<Tresult> Send<Tresult>(Request request)
            where Tresult : IBaseErrorResult, new()
        {
            var jwtToken = await sessionStorage.GetStringAsync(Constants.JWT_TOKEN);

            // TODO add expiration validation too
            if (string.IsNullOrEmpty(jwtToken) && request.CheckAuth)
            {
                // TODO save state
                navigationManager.GoLogin();
                return new Tresult()
                {
                    ResultError = ErrorResult.Build("JWT cannot be blank"),
                    Success = false,
                    ResultOperation = ResultOperation.Unauthorized,
                };
            }

            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod(request.Method),
                RequestUri = new Uri(client.BaseAddress.OriginalString + request.Url)
            };

            if (request.Parameter != null)
            {
                requestMessage.Content = JsonContent.Create(request.Parameter, request.Parameter.GetType());
            }

            if (request.CheckAuth)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            var response = await client.SendAsync(requestMessage);
            // var responseStatusCode = response.StatusCode; 
            return await response.Content.ReadFromJsonAsync<Tresult>();
        }
    }
}
