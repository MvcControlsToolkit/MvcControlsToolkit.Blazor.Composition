using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MvcControlsToolkit.Blazor.Composition;
using MvcControlsToolkit.Server.Licensing;



namespace KernelApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Module2Controller : ControllerBase
    {
        [HttpPost]
        public  ModuleResponse Post(
            [FromBody] Challenge challenge,
            [FromServices] Func<string, ModuleDynamicReference> namedModules)
        {
            LicenseChallanger.Solve(BlazorCoTServerLicense.Get(), challenge);
            return new ModuleResponse
            {
                ModuleInfo = namedModules("module2"),
                Challenge=challenge
            };
        }
        [HttpGet("/module2/{**path}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Get(string path)
        {
            Response.Headers.Add("content-encoding", "gzip");
            return File(path + ".gz", "application/octet-stream");
        }
    }
}
