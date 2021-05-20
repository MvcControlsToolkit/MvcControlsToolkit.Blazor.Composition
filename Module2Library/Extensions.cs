using Microsoft.Extensions.DependencyInjection;
using MvcControlsToolkit.Blazor.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Library
{
    public static class Extensions
    {
        public static string FilesDomain { get; internal set; }
        private static ModuleExports infos= new ModuleExports
        {
            Components = new ComponentRole[]
            {
                new ComponentRole{Role="role21", Component= typeof(ExampleBuildComponent)}
            },
            Pages = new PageRole[]
            {
                new PageRole
                {
                    URL = "/module2/examplemenupage",
                    Role="MainMenu",
                    Text = () => "Newly Added BPage",
                    
                }
            },
            JavaScript= new List<string> { "_content/Module2Library/exampleJsInterop.js" },
            Css = new List<string> { "_content/Module2Library/Module2Library.bundle.scp.css" }
        };
    public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<BuildServiceExample>();
            return services;
        }
        public static ModuleExports Export(string domain)
        {
            FilesDomain = domain;
            return infos;
        }
    }
}
