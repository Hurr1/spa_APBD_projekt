using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Blazor;

using APDBprojekt.Client.Services;
namespace APDBprojekt.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("APDBprojekt.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("APDBprojekt.ServerAPI"));
            
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddScoped<INewsService, NewsService>();

            builder.Services.AddApiAuthorization();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjU3MjA5QDMyMzAyZTMxMmUzMEFvN29JajUxdFpqbHIyODByTVVRUUhITFhpSC81d09NOVpGM0FlT29rNDA9");
            builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
            await builder.Build().RunAsync();
        }
    }
}
