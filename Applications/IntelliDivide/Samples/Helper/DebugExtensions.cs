using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.IntelliDivide.Samples.Helper
{
    internal static class DebugExtensions
    {
        internal static void Trace(this IEnumerable enumerable, [CallerMemberName] string description = "")
        {
            Debug.WriteLine(description);

            Debug.WriteLine(JsonConvert.SerializeObject(enumerable, Formatting.Indented));

            foreach (var o in enumerable)
            {
                Trace(o);
            }
            Debug.WriteLine(string.Empty);
        }

        internal static void Trace(this object o)
        {
            Debug.WriteLine(o);

            foreach (var p in o.GetType().GetProperties())
            {
                var name = p.Name;
                var value = p.GetValue(o);

                if (value != null)
                {
                    Debug.WriteLine($"\t{name}:\t{value}");
                }
            }

            Debug.WriteLine("");
        }
    }
}