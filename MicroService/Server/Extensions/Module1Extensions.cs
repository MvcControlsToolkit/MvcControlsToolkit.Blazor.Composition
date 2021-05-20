using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MvcControlsToolkit.Blazor.Extensions;

namespace MicroService.Server.Extensions
{
    public static  class Module1Extensions
    {
        public static   IServiceCollection 
            AddModule1Server(this IServiceCollection services, 
            IWebHostEnvironment environment)
        {
            services.AddModuleDescription(
                environment,
                "module1",
                 b =>
                {  
                    b.AddMain("_framework/Module1Library.dll");
                },
                null, "module1");
            return services;
        }
    }
}
