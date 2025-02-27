using HomagConnect.Base.Tests.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HomagConnect.Base.Tests
{
    /// <summary>
    /// Test API application
    /// </summary>
    public class TestApplication : WebApplicationFactory<Program>
    {
        public TestApplication(Func<HttpContext, Task<IActionResult>> func, 
            string? getPath = null, string? postPath = null)
        {
            resultService = new ResultService(func);
            keyValuePairs = new Dictionary<string, string?> {
                {"getPath", getPath},
                {"postPath", postPath},
            };
        }

        public static async Task<T?> GetBodyAsAsync<T>(HttpContext ctx)
        {
            using var reader = new StreamReader(ctx.Request.Body);
            var postData = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(postData, SerializerSettings.Default);
        }

        private ResultService resultService;
        private Dictionary<string, string?> keyValuePairs;
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(o =>
            {
                o.AddInMemoryCollection(keyValuePairs);
            });
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(IResultService));
                services.TryAddScoped<IResultService>(p => resultService);
            });

            return base.CreateHost(builder);
        }
        class ResultService : IResultService
        {
            public ResultService(Func<HttpContext, Task<IActionResult>> func)
            {
                _func = func;
            }

            Func<HttpContext, Task<IActionResult>> _func;

            public Task<IActionResult> GetResult(HttpContext ctx)
            {
                return _func(ctx);
            }
        }
    }
}
