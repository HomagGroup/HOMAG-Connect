using System;
using System.Collections;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace HomagConnect.Base.Extensions
{
    /// <summary>
    /// Trace extensions.
    /// </summary>
    public static class TraceExtensions
    {
        private static readonly JsonSerializerSettings _JsonSerializerSettings = new()
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        /// <summary>
        /// Trace an enumerable.
        /// </summary>
        public static void Trace(this IEnumerable enumerable, [CallerMemberName] string description = "")
        {
            Console.WriteLine(description);

            Console.WriteLine(JsonConvert.SerializeObject(enumerable, _JsonSerializerSettings));

            Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Trace an object.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="description"></param>
#pragma warning disable S4225 // Extension methods should not extend "object"
        public static void Trace(this object o, [CallerMemberName] string description = "")
#pragma warning restore S4225 // Extension methods should not extend "object"
        {
            Console.WriteLine(description);

            Console.WriteLine(JsonConvert.SerializeObject(o, _JsonSerializerSettings));

            Console.WriteLine(string.Empty);
        }
    }
}