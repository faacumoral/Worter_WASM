using Blazored.SessionStorage;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Worter
{
    public  static class Extensions
    {

        public static async Task SetStringAsync(this ISessionStorageService sessionStorage, string key, string value)
        {
            await sessionStorage.SetItemAsync(key, value);
        }

        public static async Task<string> GetStringAsync(this ISessionStorageService sessionStorage, string key)
        {
            return await sessionStorage.GetItemAsync<string>(key);
        }

        public static async Task SetJwtToken(this ISessionStorageService sessionStorage, string token)
        {
            await sessionStorage.SetStringAsync(Constants.JWT_TOKEN, token);
        }

        public static async Task<string> GetJwtToken(this ISessionStorageService sessionStorage)
        {
            return await sessionStorage.GetStringAsync(Constants.JWT_TOKEN);
        }

        public static async Task<bool> ExistsJWTAsync(this ISessionStorageService sessionStorage)
        {
            var jwt = await sessionStorage.GetStringAsync(Constants.JWT_TOKEN);
            return !string.IsNullOrEmpty(jwt);
        }

        public static void GoLogin(this NavigationManager navigator)
        { 
            navigator.NavigateTo(Constants.Views.STUDENTS_LOGIN);
        }
    }
}
