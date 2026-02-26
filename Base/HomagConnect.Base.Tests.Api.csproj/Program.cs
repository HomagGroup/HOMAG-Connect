using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomagConnect.Base.Tests.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddControllers().AddNewtonsoftJson(o =>
            {
#if DEBUG
                o.SerializerSettings.Formatting = Formatting.Indented;
#endif
                o.SerializerSettings.ContractResolver = new CamelCaseExceptDictionaryKeysResolver();
                o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                o.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            });


            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthorization();


            // FYI: We cannot change the serializer when we use minimal APIs
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis
            var getPath = app.Configuration.GetValue<string>("getPath");
            if (getPath != null)
            {
                app.MapControllerRoute(name: "default", getPath,
                    defaults: new { controller = "Root", action = "Get" }
                    );
            }

            var postPath = app.Configuration.GetValue<string>("postPath");
            if (postPath != null)
            {
                app.MapControllerRoute(name: "default", postPath,
                    defaults: new { controller = "Root", action = "Post" }
                    );
            }

            app.Run();
        }
    }
}
