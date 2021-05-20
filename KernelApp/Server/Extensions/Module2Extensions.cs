using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MvcControlsToolkit.Blazor.Extensions;

namespace KernelApp.Extensions
{
    public static  class Module2Extensions
    {
        public static   IServiceCollection 
            AddModule2Server(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddModuleDescription(
                environment,
                "module2",
                 b =>
                {  
                    b.AddMain("_framework/Module2Library.dll");
                },null, "module2"
                );
            return services;
        }
    }
}
