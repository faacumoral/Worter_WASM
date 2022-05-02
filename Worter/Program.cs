using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Blazored.SessionStorage;
using Worter.HTTP;
using Worter.Services;
using Worter.Services.Toast;

namespace Worter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<ToastService>();

            builder.Services.AddHttpClient<APIClient>(
                client => client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));

            builder.Services.AddBlazoredSessionStorage();

            // builder.Services.AddSingleton<Models.StateContainer>();

            await builder.Build().RunAsync();
        }
    }
}
