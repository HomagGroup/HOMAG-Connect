using System.Collections;
using System.Runtime.CompilerServices;
using HomagConnect.Base;
using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Samples.Helper
{
    internal static class DebugExtensions
    {
        internal static void Trace(this IEnumerable enumerable, [CallerMemberName] string description = "")
        {
            Console.WriteLine(description);

            Console.WriteLine(JsonConvert.SerializeObject(enumerable, SerializerSettings.Default));

            Console.WriteLine(string.Empty);
        }
    }
}