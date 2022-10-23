using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Worter.UI;
using Worter.UI.HTTP;
using Worter.UI.Services.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ToastService>();
builder.Services.AddHttpClient<APIClient>(
    client => client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddBlazoredSessionStorage();


await builder.Build().RunAsync();