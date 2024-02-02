using System.Collections;
using System.Diagnostics;
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
            Debug.WriteLine(description);

            Debug.WriteLine(JsonConvert.SerializeObject(enumerable, _JsonSerializerSettings));

            Debug.WriteLine(string.Empty);
        }

        internal static void Trace(this object o, [CallerMemberName] string description = "")
        {
            Debug.WriteLine(description);

            Debug.WriteLine(JsonConvert.SerializeObject(o, _JsonSerializerSettings));

            Debug.WriteLine(string.Empty);
        }
    }
}