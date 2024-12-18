﻿using System.Collections;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Samples.Helper
{
    internal static class DebugExtensions
    {
        private static readonly JsonSerializerSettings _JsonSerializerSettings = new ()
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
    }
}