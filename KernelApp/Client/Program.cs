using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MvcControlsToolkit.Blazor.Extensions;
using MvcControlsToolkit.Blazor.License;

namespace KernelApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.AddComposition();
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#if COMPOSITION

            builder.Services.AddDynamicModules(
            BlazorCoTClientLicense.Get(),
            "modulesDownloader",
            b =>
            {
                b.AddModule("http://localhost:5010/module1", "module1", "role11");
                //b.AddModule("/module2", "module2", "role21");
            },
            MenuService.Instance.Add);
await builder.Services.AddAssembly(
    typeof(Module2Library.Extensions).Assembly, 
    MenuService.Instance.Add);
            builder.Services.AddHttpClient("modulesDownloader", 
                client => client.BaseAddress =
                new Uri(builder.HostEnvironment.BaseAddress) );
#endif
            
            builder.Services.AddScoped(x => MenuService.Instance);
            var host = builder.Build();
#if COMPOSITION
            //await host.Services.LoadModule("http://localhost:5010/module1", 
            //    MenuService.Instance.Add);
            await host.Services.InitializeBuildStaticModules();
#endif
            await host.RunAsync();

        }
    }
}
