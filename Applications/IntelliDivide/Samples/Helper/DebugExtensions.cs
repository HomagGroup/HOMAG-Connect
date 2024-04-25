using System.Collections;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Samples.Helper
{
    internal static class DebugExtensions
    {
        private static readonly JsonSerializerSettings _JsonSerializerSettings = new()
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        internal static void Trace(this IEnumerable enumerable, [CallerMemberName] string description = "")
        {
            Console.WriteLine(description);

            Console.WriteLine(JsonConvert.SerializeObject(enumerable, _JsonSerializerSettings));

            Console.WriteLine(string.Empty);
        }

#pragma warning disable S4225 // Extension methods should not extend "object"
        internal static void Trace(this object o, [CallerMemberName] string description = "")
#pragma warning restore S4225 // Extension methods should not extend "object"
        {
            Console.WriteLine(description);

            Console.WriteLine(JsonConvert.SerializeObject(o, _JsonSerializerSettings));

            Console.WriteLine(string.Empty);
        }
    }
}