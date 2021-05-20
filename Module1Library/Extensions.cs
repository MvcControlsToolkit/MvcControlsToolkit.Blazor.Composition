using Microsoft.Extensions.DependencyInjection;
using MvcControlsToolkit.Blazor.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1Library
{
    public static class Extensions
    {
        public static string FilesDomain { get; internal set; }
        private static ModuleExports infos= new ModuleExports
        {
            Components = new ComponentRole[]
            {
                new ComponentRole{Role="role11", Component= typeof(ExampleComponent)}
            },
            Pages = new PageRole[]
            {
                new PageRole
                {
                    URL = "/module1/examplemenupage",
                    Role="MainMenu",
                    Text = () => "Newly Added Page",
                    
                }
            },
            JavaScript= new List<string> { "_content/Module1Library/JavaScript1.js?v=1" },
            Css = new List<string> { "_content/Module1Library/Module1Library.bundle.scp.css" }
        };
    public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ServiceExample>();
            return services;
        }
        public static ModuleExports Export(string domain)
        {
            FilesDomain = domain;
            return infos;
        }
    }
}
